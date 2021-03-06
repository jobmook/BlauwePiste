using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hangman;
using System;
using System.Linq;

namespace Hangman.Test
{
    [TestClass]
    public class GameTest
    {
        protected Game _game;
        protected Player _player;

        [TestInitialize]
        public void InitializeGame()
        {
            _player = new Player("Job");
            _game = new Game("testwoord");
            
        }

        public void Should_have_correct_secretword_upon_creation()
        {
            Assert.AreEqual("hallo", new Game("hallo").SecretWord);
        }

        [TestMethod()]
        public void ConstructorTest()
        {
            Game game2 = new Game("Hallo");
            Assert.AreNotEqual(_game, game2);
        }

        [TestMethod]
        public void Should_not_add_to_list_again_after_same_guessed_letter()
        { 
            _game.CheckGuess('t');
            _game.CheckGuess('t');
            Assert.AreEqual(1, _game.AllGuessedLetters.Count(x => x != null && x.Equals('t')));
        }

        [TestMethod]
        public void Should_add_correct_guess_to_list()
        {
            _game.CheckGuess('t');
            Assert.IsTrue(_game.CorrectGuessedLetters.Contains('t'));
            Assert.IsTrue(_game.AllGuessedLetters.Contains('t'));
        }

        [TestMethod]
        public void Should_add_wrong_guess_to_list()
        {
            _game.CheckGuess('p');
            Assert.IsTrue(_game.WrongGuessedLetters.Contains('p'));
            Assert.IsTrue(_game.AllGuessedLetters.Contains('p'));
        }

        [TestMethod]
        public void Should_return_true_after_win()
        {
            _game.CheckGuess('t');
            _game.CheckGuess('e');
            _game.CheckGuess('s');
            _game.CheckGuess('w');
            _game.CheckGuess('o');
            _game.CheckGuess('r');
            _game.CheckGuess('d');
            Assert.IsTrue(_game.Win());
        }

        [TestMethod]
        public void Should_return_true_after_loss()
        {
            _game.CheckGuess('a');
            _game.CheckGuess('b');
            _game.CheckGuess('c');
            _game.CheckGuess('d');
            _game.CheckGuess('e');
            _game.CheckGuess('f');
            _game.CheckGuess('h');
            _game.CheckGuess('i');
            _game.CheckGuess('j');
            _game.CheckGuess('k');
            _game.CheckGuess('l');
            _game.CheckGuess('x');
            _game.CheckGuess('y');
            _game.CheckGuess('z');
            Assert.IsTrue(_game.Lose());
        }

        [TestMethod]
        public void Should_increment_turn_after_guess()
        {
            _game.CheckGuess('t');
            Assert.AreEqual(1, _game.Turns);
        }

        [TestMethod]
        public void Should_not_decrease_tries_after_correct_guess()
        {
            _game.CheckGuess('t');
            Assert.AreEqual(10, _game.TriesLeft);
        }

        [TestMethod]
        public void Should_decrease_tries_left_after_wron_guess()
        {
            _game.CheckGuess('p');
            Assert.AreEqual(9, _game.TriesLeft);
        }

        [TestMethod]
        public void TestSecretWord()
        {
            Assert.AreEqual("testwoord", _game.SecretWord);
        }

        [TestMethod]
        public void TestAllGuessedToString()
        {
            _game.CheckGuess('t');
            _game.CheckGuess('a');
            Assert.AreEqual("t, a, ", _game.GuessedLettersToString());
        }

        [TestMethod]
        public void TestUpdateSolutionString()
        {
            _game.CheckGuess('t');
            Assert.AreEqual("t * * t * * * * * ", _game.UpdateSolutionString());
        }
    }
}