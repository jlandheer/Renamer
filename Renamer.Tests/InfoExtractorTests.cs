using NUnit.Framework;

namespace Renamer.Tests
{
   [TestFixture]
   public class InfoExtractorTests
   {
      [TestCase("24.S09E07.720p.HDTV.X264-DIMENSION", 9, 7)]
      [TestCase("01x15 - Shadows of P'Jem", 1, 15)]
      public void TestIt(string fileName, int expectedSeason, int expectedEpisode)
      {
         var extractor = new InfoExtractor(fileName);
         Assert.AreEqual(expectedSeason, extractor.SeasonNumber);
         Assert.AreEqual(expectedEpisode, extractor.EpisodeNumber);
      }

      [Test]
      public void IsIdealIsTrueForIdealFormat()
      {
         var extractor = new InfoExtractor("13x12 - The Game is on");
         Assert.AreEqual(13, extractor.SeasonNumber);
         Assert.AreEqual(12, extractor.EpisodeNumber);
         Assert.AreEqual("The Game is on", extractor.EpisodeName);
         Assert.IsTrue(extractor.IsIdeal);
      }

      [Test]
      public void IsIdealIsTrueForNamesWithDigits()
      {
         var extractor = new InfoExtractor("13x12 - The Game 4 2 is on");
         Assert.AreEqual(13, extractor.SeasonNumber);
         Assert.AreEqual(12, extractor.EpisodeNumber);
         Assert.AreEqual("The Game 4 2 is on", extractor.EpisodeName);
         Assert.IsTrue(extractor.IsIdeal);
      }

      [Test]
      public void IsIdealIsTrueForNamesWithSpecialCharacters()
      {
         var extractor = new InfoExtractor("13x12 - The Game - 4.2 is on.mkv");
         Assert.AreEqual(13, extractor.SeasonNumber);
         Assert.AreEqual(12, extractor.EpisodeNumber);
         Assert.AreEqual("The Game - 4.2 is on", extractor.EpisodeName);
         Assert.IsTrue(extractor.IsIdeal);
      }

      [Test]
      public void ExtensionMakesNoDifference()
      {
         var extractor = new InfoExtractor("13x12 - The Game is on.mkv");
         Assert.AreEqual(13, extractor.SeasonNumber);
         Assert.AreEqual(12, extractor.EpisodeNumber);
         Assert.AreEqual("The Game is on", extractor.EpisodeName);
         Assert.IsTrue(extractor.IsIdeal);
      }

      [Test]
      public void PathMakesNoDifference()
      {
         var extractor = new InfoExtractor(@"C:\Breaking Bad\13x12 - The Game is on.mkv");
         Assert.AreEqual(13, extractor.SeasonNumber);
         Assert.AreEqual(12, extractor.EpisodeNumber);
         Assert.AreEqual("The Game is on", extractor.EpisodeName);
         Assert.IsTrue(extractor.IsIdeal);
      }
   }
}
