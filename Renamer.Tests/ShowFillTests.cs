using NUnit.Framework;
using Renamer.Utilities;

namespace Renamer.Tests
{
   [TestFixture]
   public class ShowFillTests
   {
      public string ShowInfoXml;

      [SetUp]
      public void Setup()
      {
         ShowInfoXml = "<Showinfo>" +
            "<showid>124</showid>" +
            "<showname>Love is a Four Letter Word</showname>" +
            "<showlink>http://tvrage.com/shows/id-124</showlink>" +
            "<seasons>1</seasons>" +
            "<started>2001</started>" +
            "<startdate>Jan/30/2001</startdate>" +
            "<ended>Jul/24/2001</ended>" +
            "<origin_country>AU</origin_country>" +
            "<status>Canceled/Ended</status>" +
            "<classification>Scripted</classification>" +
            "<genres><genre>Drama</genre></genres>" +
            "<runtime>30</runtime>" +
            "<network country=\"AU\">ABC1</network>" +
            "<airtime>12:00</airtime>" +
            "<airday>Tuesday</airday>" +
            "<timezone>GMT+10 -DST</timezone>" +
            "</Showinfo>";
      }

      [Test]
      public void ShowNameIsFilled()
      {
         var show = Show.FromStream(ShowInfoXml.ToStream());
         Assert.AreEqual("Love is a Four Letter Word", show.Name);
      }

      [Test]
      public void SeasonsIsFilled()
      {
         var show = Show.FromStream(ShowInfoXml.ToStream());
         Assert.AreEqual(1, show.NumberOfSeasons);
      }

      [Test]
      public void IdIsFilled()
      {
         var show = Show.FromStream(ShowInfoXml.ToStream());
         Assert.AreEqual("124", show.Id);
      }

      [Test]
      public void YearStartedIsFilled()
      {
         var show = Show.FromStream(ShowInfoXml.ToStream());
         Assert.AreEqual(2001, show.YearStarted);
      }

      [Test]
      public void EndedIsFilled()
      {
         var show = Show.FromStream(ShowInfoXml.ToStream());
         Assert.AreEqual(true, show.IsEnded);
      }
   }
}
