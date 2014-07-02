using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Renamer.UI.Annotations;

namespace Renamer.UI
{
   public class ShowItem : INotifyPropertyChanged
   {
      private string _showNameOnDisk;
      private string _location;

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         var handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }

      public string ShowNameOnDisk
      {
         get { return _showNameOnDisk; }
         set
         {
            if (value == _showNameOnDisk) return;
            _showNameOnDisk = value;
            OnPropertyChanged();
         }
      }

      public string Location
      {
         get { return _location; }
         set
         {
            if (value == _location) return;
            _location = value;
            OnPropertyChanged();
         }
      }

      public ObservableCollection<EpisodeItem> Episodes { get; set; }
   }
}