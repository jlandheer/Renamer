using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Renamer
{
   public class ShowCache
   {
      private const string CacheFileName = "_cachefile.json";

      public async Task<Show> GetShowAsync(ShowDirectory showDirectory)
      {
         var filePath = Path.Combine(showDirectory.Location, CacheFileName);

         if (File.Exists(filePath))
         {
            using (var fs = File.OpenText(filePath))
            {
               var content = await fs.ReadToEndAsync();
               var show = JsonConvert.DeserializeObject<Show>(content);
               return show;
            }
         }
         return null;
      }

      public async Task SaveShowAsync(Show show, ShowDirectory showDirectory)
      {
         var filePath = Path.Combine(showDirectory.Location, CacheFileName);
         using (var fs = File.CreateText(filePath))
         {
            var jsonSerializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            await fs.WriteAsync(JsonConvert.SerializeObject(show, jsonSerializerSettings));
         }
      }
   }
}