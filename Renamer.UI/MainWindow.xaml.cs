using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Renamer.UI.Annotations;

namespace Renamer.UI
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
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

   public class EpisodeItem : INotifyPropertyChanged
   {
      private string _name;
      private int _episode;
      private int _season;
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

      public int Episode
      {
         get { return _episode; }
         set
         {
            if (value == _episode) return;
            _episode = value;
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
   }

   public partial class MainWindow : Window
   {
      private ObservableCollection<ShowItem> _shows;

      public MainWindow()
      {
         InitializeComponent();
         _shows = new ObservableCollection<ShowItem>();
         _shows.Add(new ShowItem { ShowNameOnDisk = "Breaking Bad", Location = @"S:\HD\Breaking Bad" });
         _shows.Add(new ShowItem { ShowNameOnDisk = "Community", Location = @"S:\HD\Community (2007)" });
         _shows.Add(new ShowItem { ShowNameOnDisk = "Homeland", Location = @"S:\HD\Homeland" });
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         showDirectoryListView.ItemsSource = _shows;
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         _shows[0].Location = "Test";
      }
   }
}
