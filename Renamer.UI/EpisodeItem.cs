using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Renamer.UI.Annotations;

namespace Renamer.UI
{
   public class EpisodeItem : INotifyPropertyChanged, IComparable<EpisodeItem>
   {
      private int _season;
      private int _number;
      private string _name;

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         var handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }

      public int Season
      {
         get { return _season; }
         set
         {
            if (value == _season) return;
            _season = value;
            OnPropertyChanged();
         }
      }

      public int Number
      {
         get { return _number; }
         set
         {
            if (value == _number) return;
            _number = value;
            OnPropertyChanged();
         }
      }

      public string Name
      {
         get { return _name; }
         set
         {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
         }
      }

      public int CompareTo(EpisodeItem other)
      {
         return (1000*Season + Number).CompareTo(1000*other.Season + other.Number);
      }
   }
}