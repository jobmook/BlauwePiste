using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hangman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Tests
{
    [TestClass]
    public class PlayerTests
    {
        protected Player _player;

        [TestInitialize]
        public void InitializePlayer()
        {
            _player = new Player("Job");
        }

        [TestMethod]
        public void TestPlayerGameWon()
        {
            _player.PlayerGameWon();
            Assert.AreEqual(1, _player.GamesWon);
        }

        [TestMethod]
        public void TestPlayerGameLost()
        {
            _player.PlayerGameLost();
            Assert.AreEqual(1, _player.GamesLost);
        }

        [TestMethod]
        public void TestWinRatio()
        {
            _player.PlayerGameWon();
            _player.PlayerGameLost();
            _player.PlayerGameLost();
            Assert.AreEqual(0.5, _player.GetWinRatio());
        }
    }
}