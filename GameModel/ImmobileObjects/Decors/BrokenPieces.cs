using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class BrokenPieces : ImmobileObject
    {
        public BrokenPieces() : base(DecorType.BrokenPieces, 1, 0, false, false)
        {
        }
    }
}