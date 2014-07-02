using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace Renamer.UI
{
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
