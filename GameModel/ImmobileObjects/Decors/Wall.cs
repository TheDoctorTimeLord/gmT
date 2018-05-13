using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Wall : ImmobileObject
    {
        public Wall() : base(DecorType.Wall, 1, 20, true, true)
        {
        }
    }
}