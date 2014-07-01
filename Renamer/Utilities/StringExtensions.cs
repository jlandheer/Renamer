using System.IO;
using System.Text;

namespace Renamer.Utilities
{
   public static class StringExtensions
   {
      public static MemoryStream ToStream(this string value)
      {
         return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
      }
   }
}
