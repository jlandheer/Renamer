using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Renamer
{
   public class SearchResult
   {
      public string Id { get; set; }
      public string ShowName { get; set; }
      public int Started { get; set; }
      public int? Ended { get; set; }
      public int Seasons { get; set; }
      public bool IsEnded
      {
         get { return Ended.HasValue; }
      }
   }

   public class TvRageClient : ITvShowApiClient
   {
      private const string SearchUrl = "http://services.tvrage.com/feeds/search.php?show={0}";
      private const string ShowUrl = "http://services.tvrage.com/feeds/showinfo.php?sid={0}";
      private const string EpisodeUrl = "http://services.tvrage.com/feeds/episode_list.php?sid={0}";

      public async Task<IEnumerable<SearchResult>> SearchAsync(string term)
      {
         var client = new HttpClient();
         var resultStream = await client.GetStreamAsync(String.Format(SearchUrl, term));
         return CreateShowIdentifiersFromXmlStream(resultStream);
      }

      public async Task<Show> ShowInfoAsync(SearchResult searchResult)
      {
         var showClient = new HttpClient();
         var episodeClient = new HttpClient();

         var showStream = await showClient.GetStreamAsync(String.Format(ShowUrl, searchResult.Id));
         var show = Show.FromStream(showStream);
         var episodeStream = await episodeClient.GetStreamAsync(String.Format(EpisodeUrl, searchResult.Id));
         show.FillEpisodes(episodeStream);

         return show;
      }

      public IEnumerable<SearchResult> CreateShowIdentifiersFromXmlStream(Stream stream)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(stream);
         var shows = xmlDoc.GetElementsByTagName("show");
         for (var i = 0; i < shows.Count; i++)
         {
            var show = shows[i];
            string endedText = show.SelectSingleNode("ended").InnerText;
            yield return new SearchResult
            {
               // ReSharper disable PossibleNullReferenceException
               Id = show.SelectSingleNode("showid").InnerText,
               ShowName = show.SelectSingleNode("name").InnerText,
               Seasons = int.Parse(show.SelectSingleNode("seasons").InnerText),
               Started = int.Parse(show.SelectSingleNode("started").InnerText),
               Ended = string.IsNullOrWhiteSpace(endedText) ? (int?)null : int.Parse(endedText)
               // ReSharper restore PossibleNullReferenceException
            };
         }
      }
   }
}