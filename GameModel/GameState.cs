using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameThief.GameModel
{
    public class GameState
    {
        public HashSet<ICreature> Animates { get; private set; } = new HashSet<ICreature>();
        private static readonly HashSet<ICreature> addedAnimations = new HashSet<ICreature>();

        public void UpdateState()
        {
            foreach (var animate in Animates)
            {
                var query = animate.GetIntention();
                if (IsRequestVerified(query, animate))
                    animate.ActionTaken(query);
                else
                    animate.ActionRejected(query);
            }

            TimersManager.UpdateTimers();

            Animates.UnionWith(addedAnimations);
        }

        private bool IsRequestVerified(Query query, ICreature animate)
        {
            if (query == Query.Interaction)
                return true;

            var target = animate.GetPosition() + ConvertMoveToSize[query];
            if (!Map.InBounds(target))
                return false;
            if (Map.Cells[target.X, target.Y].Creature != null)
                return false;
            if (Map.Cells[target.X, target.Y].Object.IsSolid)
                return false;
            return true;
        }

        public static void CreateCreatureByName(string nameCreature)
        {
            if (nameCreature == "Player")
            {
                var pl = new Player();
                addedAnimations.Add(pl);
            }
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
