using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangman.Domain;

namespace Hangman.DAL
{
    public class GameInitialize
    {
        public static void AddWords()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Hangman\Hangman\DAL\words.txt");
            using (GameContext context = new GameContext())
            {
                foreach (string line in lines)
                {
                    Word word = new Word(line);
                    context.Words.Add(word);
                    
                }
                context.SaveChanges();
            }
        }
    }
}
