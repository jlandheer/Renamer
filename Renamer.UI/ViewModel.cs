using System.Collections.ObjectModel;

namespace Renamer.UI
{
   public class ViewModel
   {
      public ObservableCollection<ShowItem> Shows { get; set; }
      public ShowItem SelectedShow { get; set; }

      public ViewModel()
      {
         Shows = new ObservableCollection<ShowItem>();
      }
   }
}