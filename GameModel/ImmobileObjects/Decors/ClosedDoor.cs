using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class ClosedDoor : ImmobileObject
    {
        public ClosedDoor() : base(true, true, 20, 1, "closed_door.png") { }

        public override bool InteractWith(ICreature creature)
        {
            var position = creature.Position + GameState.ConvertDirectionToSize[creature.Direction];
            MapManager.Map[position.X, position.Y].ObjectContainer.AddDecor(new OpenedDoor());
            return true;
        }
    }
}
