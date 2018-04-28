using GameThief.GameModel.AnimatedObjects;
using GameThief.GameModel.InanimateObjects;

namespace GameThief.GameModel.MapSourse
{
    public class Cell
    {
        public ICreature Creature { get; set; }
        public ImmobileObject Object { get; set; }

        public Cell()
        {
            Creature = null;
            Object = null;
        }
    }
}
