using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;

namespace Renamer.UI
{
   public partial class MainWindow : Window
   {
      private readonly ViewModel _viewModel;
      private Random _rand = new Random();
      public const string SeriesLocation = @"S:\HD";

      public MainWindow()
      {
         InitializeComponent();

         InitializeMapper();
         SearchForm.SetApi(new TvRageClient());
         SearchForm.Visibility = Visibility.Collapsed;
         EpisodeList.Visibility = Visibility.Collapsed;

         _viewModel = new ViewModel();
      }

      public static void InitializeMapper()
      {
         Mapper.Initialize(config => config.AddProfile<RenamerProfile>());
      }

      private async void Window_Loaded(object sender, RoutedEventArgs e)
      {
         DataContext = _viewModel;
         await FillShowList();
      }

      private void showDirectoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         var value = ((sender as ListView).SelectedItem) as ShowItem;
         if (value == null)
         {
            SearchForm.Visibility = Visibility.Collapsed;
            EpisodeList.Visibility = Visibility.Collapsed;
            return;
         }

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

      private async void Button_Click(object sender, RoutedEventArgs e)
      {
         await FillShowList();
      }

      private async Task FillShowList()
      {
         _viewModel.SelectedShow = null;
         _viewModel.Shows.Clear();
         var lister = new ShowLister(SeriesLocation);

         var cache = new ShowCache();
         foreach (var showDirectory in lister.Shows)
         {
            var currentShowDirectory = showDirectory;
            var showItem = Mapper.Map<ShowItem>(currentShowDirectory);
            _viewModel.Shows.Add(showItem);
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
         }
      }
   }
}
