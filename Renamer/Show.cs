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
      public IList<Episode> Episodes { get; set; }
      public object IsEnded { get; private set; }

      public Show()
      {
         Episodes = new List<Episode>();
      }

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

         var episodes = new List<Episode>();
         var currentSeasons = xmlDoc.GetElementsByTagName("Season");
         for (var i = 0; i < currentSeasons.Count; i++)
         {
            var currentSeason = currentSeasons[i];
            var seasonNumber = int.Parse(currentSeason.Attributes["no"].InnerText);
            var currentEpisodes = currentSeason.SelectNodes("episode");
            for (var j = 0; j < currentEpisodes.Count; j++)
            {
               var currentEpisode = currentEpisodes[j];
               episodes.Add(new Episode
               {
                  Season = seasonNumber,
                  Number = int.Parse(currentEpisode.SelectSingleNode("seasonnum").InnerText),
                  Name = currentEpisode.SelectSingleNode("title").InnerText
               });
            }
         }
         Episodes = episodes;
      }

      public string GetEpisodeName(int seasonNumber, int episodeNumber)
      {
         var episode = Episodes.SingleOrDefault(i => i.Season == seasonNumber && i.Number == episodeNumber);
         if (episode == null)
         {
            return null;
         }

         return episode.Name;
      }
   }
}