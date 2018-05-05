using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class OpenedCupboard : ImmobileObject
    {
        public OpenedCupboard() : base(DecorType.OpenedCuboard, 2, 0, false, false) { }
    }
}
