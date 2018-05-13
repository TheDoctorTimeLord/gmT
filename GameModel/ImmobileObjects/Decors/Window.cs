using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Window : ImmobileObject
    {
        public Window() : base(DecorType.Window, 30, 0, true, false)
        {
        }
    }
}