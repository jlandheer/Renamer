using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renamer
{
   public interface ITvShowApiClient
   {
      Task<IEnumerable<SearchResult>> SearchAsync(string term);
      Task<Show> ShowInfoAsync(SearchResult searchResult);
   }
}