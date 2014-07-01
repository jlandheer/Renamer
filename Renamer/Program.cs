using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Renamer
{
   internal class Program
   {
      private const string SeriesLocation = @"S:\HD";
      private static void Main(string[] args)
      {
         //DirectoriesScanner();
         Renaming();
         Console.WriteLine("Done");
         Console.ReadKey();
      }

      private static void Renaming()
      {
         var lister = new ShowLister(SeriesLocation);
         foreach (var showDirectory in lister.Shows)
         {
            var cache = new ShowCache();
            var show = cache.GetShowAsync(showDirectory).Result;
            if (show != null)
            {
               var ren = new DirectoryRenamer();
               ren.RenameIt(showDirectory, show);
            }
         }
      }

      private static void DirectoriesScanner()
      {
         var lister = new ShowLister(SeriesLocation);
         var client = new TvRageClient();
         foreach (var showDirectory in lister.Shows.OrderBy(i => i.ShowNameOnDisk))
         {
            Console.WriteLine("{0} : {1}", showDirectory.ShowNameOnDisk, showDirectory.Location);
            var cache = new ShowCache();
            var show = cache.GetShowAsync(showDirectory).Result;
            if (show == null)
            {
               var searchTerm = showDirectory.ShowNameOnDisk;
               Console.WriteLine("Search: {0} [Enter=Y]", searchTerm);
               var newSearch = Console.ReadLine();
               if (!string.IsNullOrWhiteSpace(newSearch))
               {
                  searchTerm = newSearch;
               }
               var results = client.SearchAsync(searchTerm).Result.ToList();
               var index = 1;
               foreach (var searchResult in results)
               {
                  Console.WriteLine("{0:00}: {1} ({2}, {3} seasons)", index++, searchResult.ShowName, searchResult.Started, searchResult.Seasons);
               }
               Console.WriteLine("Kies: [ENTER=1, s=skip]");
               var chosen = Console.ReadLine();
               chosen = string.IsNullOrWhiteSpace(chosen) ? "1" : chosen;
               if(chosen.Equals("s", StringComparison.OrdinalIgnoreCase))
               {
                  continue;
               }

               int chosenIndex;
               if (!string.IsNullOrWhiteSpace(chosen) && int.TryParse(chosen, out chosenIndex))
               {
                  show = client.ShowInfoAsync(results[chosenIndex - 1]).Result;
                  cache.SaveShowAsync(show, showDirectory);
               }
            }
         }
      }
   }
}
