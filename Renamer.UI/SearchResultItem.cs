using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Renamer.UI.Annotations;

namespace Renamer.UI
{
   public class SearchResultItem : INotifyPropertyChanged, IComparable<SearchResultItem>
   {
      private string _id;
      private string _showName;
      private int _started;
      private int? _ended;
      private int _seasons;

      public string Id
      {
         get { return _id; }
         set
         {
            if (value == _id) return;
            _id = value;
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

      public int Started
      {
         get { return _started; }
         set
         {
            if (value == _started) return;
            _started = value;
            OnPropertyChanged();
         }
      }

      public int? Ended
      {
         get { return _ended; }
         set
         {
            if (value == _ended) return;
            _ended = value;
            OnPropertyChanged();
         }
      }

      public int Seasons
      {
         get { return _seasons; }
         set
         {
            if (value == _seasons) return;
            _seasons = value;
            OnPropertyChanged();
         }
      }

      public int CompareTo(SearchResultItem other)
      {
         return String.Compare(ShowName, other.ShowName, System.StringComparison.Ordinal);
      }

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         var handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }

   }
}