using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Renamer.Utilities;

namespace Renamer.Tests
{
   [TestFixture]
   public class SearchResultsTests
   {
      public string SearchXml = "<Results>" +
                                "<show>" +
                                "<showid>18164</showid>" +
                                "<name>Breaking Bad</name>" +
                                "<link>http://www.tvrage.com/Breaking_Bad</link>" +
                                "<country>US</country>" +
                                "<started>2008</started>" +
                                "<ended></ended>" +
                                "<seasons>5</seasons>" +
                                "<status></status>" +
                                "<classification>Scripted</classification>" +
                                "<genres><genre>Crime</genre><genre>Drama</genre><genre>Thriller</genre></genres>" +
                                "</show>" +
                                "<show>" +
                                "<showid>20245</showid>" +
                                "<name>Breaking the Magician's Code: Magic's Biggest Secrets Finally Revealed</name>" +
                                "<link>http://www.tvrage.com/shows/id-20245</link>" +
                                "<country>US</country>" +
                                "<started>2008</started>" +
                                "<ended>2009</ended>" +
                                "<seasons>1</seasons>" +
                                "<status>Canceled/Ended</status>" +
                                "<classification>Reality</classification>" +
                                "<genres><genre>Discovery/Science</genre><genre>Family</genre><genre>Horror/Supernatural</genre><genre>How To/Do It Yourself</genre><genre>Mystery</genre><genre>Talent</genre></genres>" +
                                "</show>" +
                                "</Results>";

      [Test]
      public void SearchCount()
      {
         var client = new TvRageClient();
         var results = client.CreateShowIdentifiersFromXmlStream(SearchXml.ToStream());
         Assert.AreEqual(2, results.Count());
      }

      [Test]
      public void FirstResultIsBreakingBad()
      {
         var client = new TvRageClient();
         var results = client.CreateShowIdentifiersFromXmlStream(SearchXml.ToStream());

         Assert.AreEqual("Breaking Bad", results.First().ShowName);
      }

      [Test]
      public void FirstResultIsNotEnded()
      {
         var client = new TvRageClient();
         var results = client.CreateShowIdentifiersFromXmlStream(SearchXml.ToStream());

         var firstResult = results.First();
         Assert.IsNull(firstResult.Ended);
         Assert.AreEqual(false, firstResult.IsEnded);
      }
   }
}
