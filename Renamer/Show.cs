using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Renamer
{
   public class Show
   {
      public string Id { get; set; }
      public string Name { get; set; }
      public int YearStarted { get; set; }
      public string Path { get; set; }
      public int NumberOfSeasons { get; set; }
      public IList<Season> Seasons { get; set; }
      public object IsEnded { get; private set; }

      public static Show FromStream(Stream stream)
      {
         var show = new Show();
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(stream);

         var idNode = xmlDoc.DocumentElement.SelectSingleNode("showid");
         var nameNode = xmlDoc.DocumentElement.SelectSingleNode("showname");
         var startedNode = xmlDoc.DocumentElement.SelectSingleNode("started");
         var seasonsNode = xmlDoc.DocumentElement.SelectSingleNode("seasons");
         var statusNode = xmlDoc.DocumentElement.SelectSingleNode("status");

         show.Name = nameNode.InnerText;
         show.Id = idNode.InnerText;
         show.YearStarted = int.Parse(startedNode.InnerText);
         show.NumberOfSeasons = int.Parse(seasonsNode.InnerText);
         show.IsEnded = statusNode.InnerText.IndexOf("ended", StringComparison.OrdinalIgnoreCase) >= 0;

         return show;
      }

      public void FillEpisodes(Stream stream)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(stream);

         var seasons = new List<Season>();
         var currentSeasons = xmlDoc.GetElementsByTagName("Season");
         for (var i = 0; i < currentSeasons.Count; i++)
         {
            var currentSeason = currentSeasons[i];
            var season = new Season();
            season.Number = int.Parse(currentSeason.Attributes["no"].InnerText);
            var episodes = new List<Episode>();
            var currentEpisodes = currentSeason.SelectNodes("episode");
            for (var j = 0; j < currentEpisodes.Count; j++)
            {
               var currentEpisode = currentEpisodes[j];
               episodes.Add(new Episode
               {
                  Number = int.Parse(currentEpisode.SelectSingleNode("seasonnum").InnerText),
                  Name = currentEpisode.SelectSingleNode("title").InnerText
               });
            }
            season.Episodes = episodes;
            seasons.Add(season);
         }
         Seasons = seasons;
      }

      public string GetEpisodeName(int seasonNumber, int episodeNumber)
      {
         var season = Seasons.SingleOrDefault(i => i.Number == seasonNumber);
         if (season == null)
         {
            return null;
         }

         var episode = season.Episodes.SingleOrDefault(i => i.Number == episodeNumber);
         if (episode == null)
         {
            return null;
         }

         return episode.Name;
      }
   }
}