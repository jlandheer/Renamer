using NUnit.Framework;

namespace Renamer.Tests
{
   [TestFixture]
   public class DirectoryRenamerTests
   {
      public class CleanFileNameTests
      {
         [Test]
         public void LessThanIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName(@"a<a"));
         }

         [Test]
         public void GreaterThanIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName(@"a>a"));
         }

         [Test]
         public void ColonIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName(@"a|a"));
         }

         [Test]
         public void DoubleQuoteIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName("a\"a"));
         }

         [Test]
         public void ForwardSlashIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName("a/a"));
         }

         [Test]
         public void BackslashIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName(@"a\a"));
         }

         [Test]
         public void PipeIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName(@"a|a"));
         }

         [Test]
         public void QuestionMarkIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName(@"a?a"));
         }

         [Test]
         public void AsteriskIsReplaced()
         {
            var renamer = new DirectoryRenamer();
            Assert.AreEqual("a-a", renamer.CleanEpisodeName(@"a*a"));
         }
      }
   }
}