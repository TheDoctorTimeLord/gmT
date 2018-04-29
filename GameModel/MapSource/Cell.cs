using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.MapSource
{
    public class Cell
    {
        public string BackgroundFilename { get; set; }
        public ICreature Creature { get; set; }
        public ObjectsContainer ObjectContainer { get; set; }

        public Cell(string backgroundFilename)
        {
            BackgroundFilename = backgroundFilename;
            Creature = null;
            ObjectContainer = new ObjectsContainer();
        }
    }
}
