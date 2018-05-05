using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class OpenedDoor : ImmobileObject
    {
        public OpenedDoor() : base(DecorType.OpenedDoor, 1, 0, false, false) { }

        public override bool InteractWith(ICreature creature)
        {
            var position = creature.Position + GameState.ConvertDirectionToSize[creature.Direction];
            MapManager.Map[position.X, position.Y].ObjectContainer.AddDecor(new ClosedDoor());
            return true;
        }
    }
}
