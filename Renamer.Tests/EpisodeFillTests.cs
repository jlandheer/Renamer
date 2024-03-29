﻿using System.Linq;
using NUnit.Framework;
using Renamer.Utilities;

namespace Renamer.Tests
{
   [TestFixture]
   public class EpisodeFillTests
   {
      public string EpisodeInfoXml = "<Show>" +
                                     "<name>Love is a Four Letter Word</name>" +
                                     "<totalseasons>1</totalseasons>" +
                                     "<Episodelist>" +
                                     "<Season no=\"1\">" +
                                     "<episode>" +
                                     "<epnum>1</epnum>" +
                                     "<seasonnum>01</seasonnum>" +
                                     "<prodnum/>" +
                                     "<airdate>2001-01-30</airdate>" +
                                     "<link>http://www.tvrage.com/shows/id-124/episodes/245617</link>" +
                                     "<title>Love</title>" +
                                     "</episode>" +
                                     "<episode>" +
                                     "<epnum>2</epnum>" +
                                     "<seasonnum>02</seasonnum>" +
                                     "<prodnum/>" +
                                     "<airdate>2001-02-06</airdate>" +
                                     "<link>http://www.tvrage.com/shows/id-124/episodes/245618</link>" +
                                     "<title>Dill</title>" +
                                     "</episode>" +
                                     "<episode>" +
                                     "<epnum>3</epnum>" +
                                     "<seasonnum>03</seasonnum>" +
                                     "<prodnum/>" +
                                     "<airdate>2001-02-13</airdate>" +
                                     "<link>http://www.tvrage.com/shows/id-124/episodes/245619</link>" +
                                     "<title>Fame</title>" +
                                     "</episode>" +
                                     "</Season>" +
                                     "<Season no=\"2\">" +
                                     "<episode>" +
                                     "<epnum>1</epnum>" +
                                     "<seasonnum>01</seasonnum>" +
                                     "<prodnum/>" +
                                     "<airdate>2001-01-30</airdate>" +
                                     "<link>http://www.tvrage.com/shows/id-124/episodes/245617</link>" +
                                     "<title>Love2</title>" +
                                     "</episode>" +
                                     "<episode>" +
                                     "<epnum>2</epnum>" +
                                     "<seasonnum>02</seasonnum>" +
                                     "<prodnum/>" +
                                     "<airdate>2001-02-06</airdate>" +
                                     "<link>http://www.tvrage.com/shows/id-124/episodes/245618</link>" +
                                     "<title>Dill2</title>" +
                                     "</episode>" +
                                     "</Season>" +
                                     "</Episodelist>" +
                                     "</Show>";

      [Test]
      public void TwoSeasonsAreFilled()
      {
         var show = new Show();
         show.FillEpisodes(EpisodeInfoXml.ToStream());

         Assert.AreEqual(2, show.Episodes.GroupBy(i => i.Season).Count());
      }

      [Test]
      public void FirstSeasonHasThreeEpisodes()
      {
         var show = new Show();
         show.FillEpisodes(EpisodeInfoXml.ToStream());

         Assert.AreEqual(3, show.Episodes.Count(i => i.Season == 1));
      }

      [Test]
      public void SesondSeasonHasTwoEpisodes()
      {
         var show = new Show();
         show.FillEpisodes(EpisodeInfoXml.ToStream());

         Assert.AreEqual(2, show.Episodes.Count(i => i.Season == 2));
      }

      [Test]
      public void SesondSeasonSeconsEpisodeNameIfFilled()
      {
         var show = new Show();
         show.FillEpisodes(EpisodeInfoXml.ToStream());

         Assert.AreEqual("Dill2", show.Episodes.Single(i => i.Season == 2 && i.Number == 2).Name);
      }

      [Test]
      public void SesondSeasonSecondEpisodeNumberIsFilled()
      {
         var show = new Show();
         show.FillEpisodes(EpisodeInfoXml.ToStream());

         Assert.AreEqual(2, show.Episodes.Single(i => i.Season == 2 && i.Number == 2).Number);
      }
   }
}