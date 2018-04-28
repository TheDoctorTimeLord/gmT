using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSourse;

namespace GameThief.GameModel
{
    public class GameState
    {
        public void UpdateState()
        {
            foreach (var animate in AnimatesManager.Animates)
            {
                var query = animate.GetIntention();
                if (IsRequestVerified(query, animate))
                    animate.ActionTaken(query);
                else
                    animate.ActionRejected(query);
            }

            TimersManager.UpdateTimers();
        }

        private bool IsRequestVerified(Query query, ICreature animate)
        {
            if (query == Query.Interaction)
                return true;

            var target = animate.GetPosition() + ConvertMoveToSize[query];
            if (!MapManager.InBounds(target))
                return false;
            if (MapManager.Map.Cells[target.X, target.Y].Creature != null)
                return false;
            if (MapManager.Map.Cells[target.X, target.Y].Object.IsSolid)
                return false;
            return true;
        }

        public static readonly Dictionary<Query, Size> ConvertMoveToSize = new Dictionary<Query, Size>
        {
            { Query.MoveUp, new Size(0, -1) },
            { Query.MoveLeft, new Size(-1, 0) },
            { Query.MoveDown, new Size(0, 1) },
            { Query.MoveRight, new Size(1, 0) }
        };
    }
}
