using System;
using System.IO;

namespace Renamer
{
   public class DirectoryRenamer
   {
      private const string ForbiddenCharacters = "<>:\"/|?*\\";
      public string CleanEpisodeName(string fileName)
      {
         var result = fileName;
         foreach (var forbiddenCharacter in ForbiddenCharacters)
         {
            result = result.Replace(forbiddenCharacter, '-');
         }
         return result;
      }

      public void RenameIt(ShowDirectory showDirectory, Show show)
      {
         var files = Directory.EnumerateFiles(showDirectory.Location);
         foreach (var file in files)
         {
            var extractor = new InfoExtractor(file);
            if (extractor.IsIdeal)
               continue;

            if (!extractor.Success)
               continue;

            var episodeName = show.GetEpisodeName(extractor.SeasonNumber, extractor.EpisodeNumber);
            if (episodeName == null) 
               continue;

            var extension = Path.GetExtension(file);
            var newFileName = string.Format("{0:00}x{1:00} - {2}{3}", extractor.SeasonNumber, extractor.EpisodeNumber,
               CleanEpisodeName(episodeName), extension);
            newFileName = Path.Combine(showDirectory.Location, newFileName);
            if (!File.Exists(newFileName))
            {
               File.Move(file, Path.Combine(showDirectory.Location, newFileName));
               Console.WriteLine(file + " => " + newFileName);
            }
         }
      }
   }
}