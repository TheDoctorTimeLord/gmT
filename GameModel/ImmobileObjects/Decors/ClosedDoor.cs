using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class ClosedDoor : ImmobileObject
    {
        public ClosedDoor() : base(DecorType.ClosedDoor, 10, 1, true, true)
        {
        }

        public override bool InteractWith(ICreature creature)
        {
            var position = creature.Position + GameState.ConvertDirectionToSize[creature.Direction];
            MapManager.Map[position.X, position.Y].ObjectContainer.AddDecor(new OpenedDoor());
            return true;
        }
    }
}