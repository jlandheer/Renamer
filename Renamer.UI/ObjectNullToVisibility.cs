using System;
using System.Windows;
using System.Windows.Data;

namespace Renamer.UI
{
   public class ObjectNullToVisibility : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         return value == null ? Visibility.Collapsed : Visibility.Visible;
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         return value;
      }
   }
}