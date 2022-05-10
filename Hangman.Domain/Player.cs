using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }

        public virtual List<Game> Games { get; set; }

        public Player(string name)
        {
            Name = name;
            
        }
    }
}