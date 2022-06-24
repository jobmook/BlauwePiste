using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Domain
{
    public class Word
    {
        public int WordID { get; set; }
        public string SecretWord { get; set; }

        public Word(string secretWord)
        {
            SecretWord = secretWord;
        }
    }
}
