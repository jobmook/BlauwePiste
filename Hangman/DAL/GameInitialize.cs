using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL
{
    internal class GameInitialize
    {
        static void AddWords()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Hangman\Hangman\DAL\words.txt");
            using (GameContext context = new GameContext())
            {
                foreach (string line in lines)
                {
                    
                }
            }
                

        }

    }
}
