namespace GameThief.GameModel
{
    public interface ITimer
    {
        void Update();
        bool IsActive();
        void ActionAfterDeactivation();
    }
}
