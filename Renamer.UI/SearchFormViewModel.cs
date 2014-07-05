namespace Renamer.UI
{
   public class SearchFormViewModel
   {
      public ComparingObservableCollection<SearchResultItem> SearchResults { get; set; }

      public SearchFormViewModel()
      {
         SearchResults = new ComparingObservableCollection<SearchResultItem>();
      }
   }
}