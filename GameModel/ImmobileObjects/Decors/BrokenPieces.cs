using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class BrokenPieces : ImmobileObject
    {
        public BrokenPieces() : base(false, true, 0, 1, "broken_pieces.png") { }
    }
}
