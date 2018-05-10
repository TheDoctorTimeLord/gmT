using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.MapSource
{
    public class Cell
    {
        public CellType Type { get; set; }
        public ICreature Creature { get; set; }
        public ObjectsContainer ObjectContainer { get; set; }

        public Cell(string background)
        {
            Type = CellType.Wood;
            Creature = null;
            ObjectContainer = new ObjectsContainer();
        }
    }
}
