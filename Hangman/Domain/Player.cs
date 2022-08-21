using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hangman
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual List<Game> Games { get; set; }

    }
}