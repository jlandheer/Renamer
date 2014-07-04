using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Amib.Threading;
using AutoMapper;

namespace Renamer.UI
{
   public partial class MainWindow : Window
   {
      private readonly ViewModel _viewModel;
      public const string SeriesLocation = @"S:\Hd";

      public MainWindow()
      {
         InitializeComponent();

         InitializeMapper();
         var s = new Show() { Episodes = new List<Episode> { new Episode { Season = 1, Number = 1, Name = "Ep1" } } };
         var i2 = s.Episodes.Select(i => Mapper.Map<EpisodeItem>(i));

         var pool = new SmartThreadPool();

         _viewModel = new ViewModel();
         var lister = new ShowLister(SeriesLocation);

         var cache = new ShowCache();
         var rand = new Random();
         foreach (var showDirectory in lister.Shows)
         {
            var currentShowDirectory = showDirectory;
            var showItem = Mapper.Map<ShowItem>(currentShowDirectory);
            _viewModel.Shows.Add(showItem);
            pool.QueueWorkItem(async () =>
            {
               showItem.Status = Status.Checking;
               var show = await cache.GetShowAsync(currentShowDirectory);
               //Thread.Sleep(rand.Next(1000, 3000));
               if (show != null)
               {
                  showItem.ShowName = show.Name;
                  var itemsToAdd = show.Episodes.Select(i => Mapper.Map<EpisodeItem>(i)).ToList();
                  showItem.Episodes.AddRange(itemsToAdd);
                  showItem.Status = Status.Found;
               }
               else
               {
                  showItem.ShowName = "Niet gevonden";
                  showItem.Status = Status.NotFound;
               }
            });
         }
      }

      public static void InitializeMapper()
      {
         Mapper.Initialize(config => config.AddProfile<RenamerProfile>());
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         DataContext = _viewModel;
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         _viewModel.Shows[0].Location = "Test";
      }

      private void showDirectoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         var value = ((sender as ListView).SelectedItem) as ShowItem;
         switch (value.Status)
         {
            case Status.Idle:
               SearchForm.Visibility = Visibility.Collapsed;
               EpisodeList.Visibility = Visibility.Collapsed;
               break;
            case Status.Checking:
               SearchForm.Visibility = Visibility.Collapsed;
               EpisodeList.Visibility = Visibility.Collapsed;
               break;
            case Status.Found:
               EpisodeList.DataContext = value;
               SearchForm.Visibility = Visibility.Collapsed;
               EpisodeList.Visibility = Visibility.Visible;
               break;
            case Status.NotFound:
               SearchForm.SetShow(value);
               SearchForm.Visibility = Visibility.Visible;
               EpisodeList.Visibility = Visibility.Collapsed;
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }
      }
   }
}
