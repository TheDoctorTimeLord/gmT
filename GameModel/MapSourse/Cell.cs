using GameThief.GameModel.AnimatedObjects;
using GameThief.GameModel.InanimateObjects;

namespace GameThief.GameModel.MapSourse
{
    public class Cell
    {
        public ICreature Creature { get; set; } = null;
        public ImmobileObject Object { get; set; } = new ImmobileObject();
    }
}
