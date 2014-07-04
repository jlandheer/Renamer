using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Primitives;
using Renamer.UI.Annotations;

namespace Renamer.UI
{
   public class ShowItem : INotifyPropertyChanged
   {
      private string _showNameOnDisk;
      private string _location;
      private string _showName;
      private Status _status;

      public event PropertyChangedEventHandler PropertyChanged;

      public ShowItem()
      {
         Episodes = new ObservableCollection<EpisodeItem>();
         Status = Status.Idle;
      }

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         var handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }

      public Status Status
      {
         get { return _status; }
         set
         {
            if (Equals(value, _status)) return;
            _status = value;
            OnPropertyChanged();
         }
      }

      public string ShowName
      {
         get { return _showName; }
         set
         {
            if (value == _showName) return;
            _showName = value;
            OnPropertyChanged();
         }
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

   public enum Status
   {
      Idle,
      Checking,
      Found,
      NotFound
   }
}