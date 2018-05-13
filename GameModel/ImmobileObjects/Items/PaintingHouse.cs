using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.MobileObjects.Creature;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class PaintingHouse : Item
    {
        public PaintingHouse() : base(200, DecorType.PaintingHouse)
        {
        }

        public override bool InteractWith(ICreature creature)
        {
            if (creature is Player)
                return creature.Inventory.AddItem(this);
            return false;
        }
    }
}