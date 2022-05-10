
using Hangman.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; } 
        
        public DbSet<Player> Players { get; set; }

        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer(@"Server=localhost;Database = HangmanDB;Integrated Security=true");
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Game>(new GameConfiguration());
            modelBuilder.ApplyConfiguration<Player>(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration<Word>(new WordConfiguration());
        }
    }
}
