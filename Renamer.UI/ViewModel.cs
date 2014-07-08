namespace Renamer.UI
{
   public class ViewModel
   {
      public ObservableSortedList<ShowItem> Shows { get; set; }
      public ShowItem SelectedShow { get; set; }

      public ViewModel()
      {
         Shows = new ObservableSortedList<ShowItem>();
      }
   }
}