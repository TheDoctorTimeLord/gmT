using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Wall : ImmobileObject
    {
        public Wall() : base(DecorType.Wall, 1, 20, true, true) { }
    }
}
