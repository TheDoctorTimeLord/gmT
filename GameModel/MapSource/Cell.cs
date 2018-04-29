using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.MapSource
{
    public class Cell
    {
        public ICreature Creature { get; set; } = null;
        public ObjectsContainer ObjectContainer { get; set; } = new ObjectsContainer();
    }
}
