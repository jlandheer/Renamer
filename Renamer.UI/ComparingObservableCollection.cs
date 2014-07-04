using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Renamer.UI
{
   public class ComparingObservableCollection<T> : ObservableCollection<T>
      where T : IComparable<T>
   {
      public ComparingObservableCollection()
      {
      }

      public ComparingObservableCollection(IEnumerable<T> items)
         : base(items.OrderBy(i => i))
      {
      }

      protected override void InsertItem(int index, T item)
      {
         int i = 0;
         bool found = false;
         for (i = 0; i < Items.Count; i++)
         {
            if (item.CompareTo(Items[i]) < 0)
            {
               found = true;
               break;
            }
         }

         if (!found) i = Count;

         base.InsertItem(i, item);
      }
   }
}