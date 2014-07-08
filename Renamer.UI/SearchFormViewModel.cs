namespace Renamer.UI
{
   public class SearchFormViewModel
   {
      public ObservableSortedList<SearchResultItem> SearchResults { get; set; }

      public SearchFormViewModel()
      {
         SearchResults = new ObservableSortedList<SearchResultItem>();
      }
   }
}