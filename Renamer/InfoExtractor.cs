using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Renamer
{
   public class InfoExtractor
   {
      private readonly IEnumerable<Regex> _patterns = new List<Regex> {
         new Regex(@"S(\d+)E(\d+)", RegexOptions.IgnoreCase),
         new Regex(@"(\d+)x(\d+)", RegexOptions.IgnoreCase)
      };

      private readonly Regex _idealPattern = new Regex(@"^(\d+)x(\d+) - (.+)$", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);

      public string EpisodeName { get; set; }
      public int SeasonNumber { get; private set; }
      public int EpisodeNumber { get; private set; }
      public bool Success { get; private set; }
      public bool IsIdeal { get; private set; }

      public InfoExtractor(string fileName)
      {
         fileName = Path.GetFileNameWithoutExtension(fileName);
         Success = false;
         IsIdeal = DoMatch(fileName, _idealPattern);
         if (IsIdeal)
            return;

         foreach (var regex in _patterns)
         {
            if (DoMatch(fileName, regex))
               return;
         }
      }

      private bool DoMatch(string fileName, Regex regex)
      {
         var match = regex.Match(fileName);
         if (match.Success)
         {
            SeasonNumber = int.Parse(match.Groups[1].Value);
            EpisodeNumber = int.Parse(match.Groups[2].Value);
            if (match.Groups.Count > 3)
            {
               EpisodeName = match.Groups[3].Value;
            }

            Success = true;
            return true;
         }
         return false;
      }
   }
}