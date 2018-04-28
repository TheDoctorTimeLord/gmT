using System.Collections.Generic;

namespace GameThief.GameModel.Managers
{
    public static class TemporaryObjectsManager
    {
        private static HashSet<ITemporaryObject> timers = new HashSet<ITemporaryObject>();

        public static void UpdateTimers()
        {
            foreach (var timer in timers)
            {
                timer.Update();
                if (timer.IsActive())
                    continue;
                timer.ActionAfterDeactivation();
                timers.Remove(timer);
            }
        }

        public static void AddTimer(ITemporaryObject temporaryObject)
        {
            timers.Add(temporaryObject);
        }
    }
}
