// ReSharper disable InconsistentNaming
using System.Collections.Generic;

namespace Renamer.UI
{
   public static class IEnumerableExtensions
   {
      public static void AddRange<T>(this IList<T> items, IEnumerable<T> itemsToAdd)
      {
         foreach (var item in itemsToAdd)
         {
            items.Add(item);
         }
      }
   }
}