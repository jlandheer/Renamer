using System.Collections.Generic;
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
using AutoMapper;

namespace Renamer.UI
{
   /// <summary>
   /// Interaction logic for SearchForm.xaml
   /// </summary>
   public partial class SearchForm : UserControl
   {
      private ITvShowApiClient _client;
      private SearchFormViewModel _viewModel;

      public SearchForm()
      {
         InitializeComponent();
         _viewModel = new SearchFormViewModel();
         DataContext = _viewModel;
      }

      public void SetApi(ITvShowApiClient client)
      {
         _client = client;
      }

      public void SetShow(ShowItem show)
      {
         SearchTerm.Text = show.ShowNameOnDisk;
      }

      private async void DoSearch_Click(object sender, RoutedEventArgs e)
      {
         SearchTerm.IsEnabled = false;
         DoSearch.IsEnabled = false;
         var result = await _client.SearchAsync(SearchTerm.Text);
         SearchTerm.IsEnabled = true;
         DoSearch.IsEnabled = true;
         foreach (var searchResult in result)
         {
            _viewModel.SearchResults.Add(Mapper.Map<SearchResultItem>(searchResult));
         }
      }
   }
}
