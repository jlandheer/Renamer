using System.Collections.Generic;

namespace Renamer
{
   public class Season
   {
      public int Number { get; set; }
      public IList<Episode> Episodes { get; set; }
   }
}