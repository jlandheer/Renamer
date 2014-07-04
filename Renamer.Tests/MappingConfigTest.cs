using AutoMapper;
using NUnit.Framework;
using Renamer.UI;

namespace Renamer.Tests
{
   [TestFixture]
   public class MappingConfigTest
   {
      [Test]
      public void ConfigurationIsValid()
      {
         var typeListToForceAssemblyLoad = new[] {
            typeof(RenamerProfile)
         };

         Assert.DoesNotThrow(() => MainWindow.InitializeMapper());
         Mapper.AssertConfigurationIsValid();
      }
   }
}