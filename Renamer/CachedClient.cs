using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renamer
{
   public class CachedClient : ITvShowApiClient
   {
      private readonly ITvShowApiClient _client;

      public CachedClient(ITvShowApiClient client)
      {
         _client = client;
      }


      public async Task<IEnumerable<SearchResult>> SearchAsync(string term)
      {
         return await _client.SearchAsync(term);
      }

      public async Task<Show> ShowInfoAsync(SearchResult searchResult)
      {
         var showInfo = await _client.ShowInfoAsync(searchResult);

         return showInfo;
      }
   }
}