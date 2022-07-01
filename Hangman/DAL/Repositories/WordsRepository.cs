using Hangman.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL.Repositories
{
    public class WordsRepository
    {
        public Word GetWordById(int id)
        {
            using (GameContext context = new GameContext())
            {
                return context.Words.Find(id);
            }
        }
        public Boolean IsEmptyWordList()
        {
            using (GameContext context = new GameContext())
            {
                var wordlist = context.Words.ToList();
                return wordlist.Count() == 0;
            }
        }

        public Word GetSecretWord()
        {
            using (GameContext context = new GameContext())
            {
                var word = context.Words.OrderBy(r => Guid.NewGuid()).First();
                return word;
            }
        }

        public void AddWords()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Hangman\Hangman\DAL\words.txt");
            using (GameContext context = new GameContext())
            {
                foreach (string line in lines)
                {
                    Word word = new Word();
                    word.SecretWord = line;
                    context.Words.Add(word);
                }
                context.SaveChanges();
            }
        }
    }
}
