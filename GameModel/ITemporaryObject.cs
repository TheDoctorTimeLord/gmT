namespace GameThief.GameModel
{
    public interface ITemporaryObject
    {
        void Update();
        bool IsActive();
        void ActionAfterDeactivation();
    }
}
