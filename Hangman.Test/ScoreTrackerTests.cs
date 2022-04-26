using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hangman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Tests
{
    [TestClass()]
    public class ScoreTrackerTests
    {
        ScoreTracker _scoreTracker;

        [TestInitialize]
        public void InitializeTest()
        {
            _scoreTracker = new ScoreTracker();
        }

        [TestMethod]
        public void AddPlayerTest()
        {
            Player p = new Player("Job");
            _scoreTracker.AddPlayer(p);
            Assert.IsTrue(_scoreTracker.Players.Contains(p));
        }

        [TestMethod]
        public void TestStats()
        {
            _scoreTracker.AddWonTime(500);
            _scoreTracker.AddWrongGuessCount(5);
            Assert.AreEqual(5, _scoreTracker.WrongGuessCount[0]);
            Assert.AreEqual(500, _scoreTracker.WonTimes[0]);
        }
    }
}