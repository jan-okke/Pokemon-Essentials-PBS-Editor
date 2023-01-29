using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Essentials_PBS_Editor.Entities
{
    public class Pokemon
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }
        public List<Move> Moves { get; set; }
        public int AbilityIndex { get; set; }
        public string Ability { get; set; }
        public List<int> IVs { get; set; }
        public bool Shiny { get; set; }
        public string Item { get; set; }
        public string Ball { get; set; }
        public bool Shadow { get; set; }
        public string Nickname { get; set; }

        public override string ToString()
        {
            return $"{Name},{Level}";
        }
    }
}
