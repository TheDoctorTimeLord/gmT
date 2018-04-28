using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel
{
    public class Cell
    {
        public ICreature Creature { get; set; }
        public ImmobileObject Object { get; set; }

        public Cell()
        {
            Creature = null;
            Object = null;
        }
    }
}
