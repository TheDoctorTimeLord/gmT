using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.AnimatedObjects;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSourse;

namespace GameThief.GameModel
{
    public class GameState
    {
        public GameState(string nameBeginActState)
        {
            var infoByMap = FileManager.ReadingMapState(nameBeginActState);
            if (!infoByMap.IsSuccessfulReading)
                throw new Exception("Некорректное задание стартовых данных. Файл: " + nameBeginActState);
            MapManager.CreateMap(infoByMap.Width, infoByMap.Height, infoByMap.InfoAboutMap);
        }

        public void UpdateState()
        {
            foreach (var animate in AnimatesManager.Animates)
            {
                var query = animate.GetIntention();
                if (IsRequestVerified(query, animate))
                {
                    ActionIntention(query, animate);
                    animate.ActionTaken(query);
                }
                else
                    animate.ActionRejected(query);
            }

            TemporaryObjectsManager.UpdateTimers();
            AnimatesManager.UpdateAnimates();
        }

        private void ActionIntention(Query query, ICreature animate)
        {
            switch (query)
            {
                case Query.Interaction:

                    break;
            }
        }

        private bool IsRequestVerified(Query query, ICreature animate)
        {
            if ()

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

        public static readonly Dictionary<Direction, Size> ConvertDirectionToSize = new Dictionary<Direction, Size>
        {
            { Direction.Up, new Size(0, -1) },
            { Direction.Left, new Size(-1, 0) },
            { Direction.Down, new Size(0, 1) },
            { Direction.Right, new Size(1, 0) }
        };
    }
}
