using Hangman.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL.Repositories
{
    public class WordsRepository
    {
        public async Task<Word> GetWordByIdAsync(int id)
        {
            using (GameContext context = new GameContext())
            {
                return await context.Words.FindAsync(id);
            }
        }
        public async Task<Boolean> IsEmptyWordListAsync()
        {
            using (GameContext context = new GameContext())
            {
                var wordlist = await context.Words.ToListAsync();
                return wordlist.Count() == 0;
            }
        }

        public async Task<Word> GetSecretWordAsync()
        {
            using (GameContext context = new GameContext())
            {
                var word = await context.Words.OrderBy(r => Guid.NewGuid()).FirstAsync();
                return word;
            }
        }

        public async void AddWordsAsync()
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
                await context.SaveChangesAsync();
            }
        }
    }
}
