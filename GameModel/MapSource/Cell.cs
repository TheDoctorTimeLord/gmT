using System;
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
            Enum.TryParse(background, true, out CellType type);
            Type = type;
            Creature = null;
            ObjectContainer = new ObjectsContainer();
        }
    }
}
