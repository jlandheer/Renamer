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
namespace ParaPlan.Converters
{
}
namespace Renamer.UI
{
   public partial class MainWindow : Window
   {
      private readonly ViewModel _viewModel;
      public const string SeriesLocation = @"D:\Series";

      public MainWindow()
      {
         InitializeComponent();

         InitializeMapper();
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
               Thread.Sleep(rand.Next(1000, 3000));
               if (show != null)
               {
                  showItem.ShowName = show.Name;
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
         this.DataContext = _viewModel;
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         _viewModel.Shows[0].Location = "Test";
      }
   }
}
