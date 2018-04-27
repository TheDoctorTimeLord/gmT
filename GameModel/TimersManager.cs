using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel
{
    public static class TimersManager
    {
        private static HashSet<ITimer> timers = new HashSet<ITimer>();

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

        public static void AddTimer(ITimer timer)
        {
            timers.Add(timer);
        }
    }
}
