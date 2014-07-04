using System;
using System.Windows;
using System.Windows.Data;

namespace Renamer.UI
{
   public class StatusToVisibility : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         switch ((Status)value)
         {
            case Status.Found:
               return Visibility.Visible;
            case Status.NotFound:
               return Visibility.Visible;
            default:
               return Visibility.Collapsed;
         }
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         return value;
      }
   }
}