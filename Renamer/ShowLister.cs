using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Renamer
{
   public class ShowLister
   {
      public IEnumerable<ShowDirectory> Shows { get; private set; }

      public ShowLister(string basePath)
      {
         Shows = Directory.GetDirectories(basePath).Select(i => DirectoryToShow(i)).ToList();
      }

      private ShowDirectory DirectoryToShow(string directory)
      {
         var lastIndex = directory.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase);
         var name = directory.Substring(lastIndex + 1);
         return new ShowDirectory { ShowNameOnDisk = name, Location = directory };
      }
   }
}