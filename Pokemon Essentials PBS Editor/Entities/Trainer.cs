using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Essentials_PBS_Editor.Entities
{
    public class Trainer
    {
        public List<Pokemon> Pokemons { get; set; }
        public string TrainerClass { get; set; }
        public string TrainerName { get; set; }
        public string LoseText { get; set; }
        public int Version { get; set; } = 0;

        public Trainer() 
        { 
            Pokemons = new List<Pokemon>();
        }
        public override string ToString()
        {
            return $"[{TrainerClass},{TrainerName},{Version}]";
        }
    }
}
