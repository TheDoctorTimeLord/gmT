using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class OpenedCupboard : ImmobileObject
    {
        public OpenedCupboard() : base(false, false, 0, 2, "opened_cupboard.png") { }
    }
}
