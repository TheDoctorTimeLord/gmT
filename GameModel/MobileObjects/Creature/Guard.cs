using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects.Creature
{
    public class Guard : MobileObject
    {
        public override CreatureTypes Type { get; set; } = CreatureTypes.Guard;

        private const int SearchTime = 10;

        private readonly List<Instruction> normalGuardTrack = new List<Instruction>();
        private int currentInstruction;
        private Deque<Query> actionQueue = new Deque<Query>();
        private LevelOfAlertness levelOfAlertness;
        private bool changedLevelOfAlertness;

        private int searchTime;

        private Point target = Point.Empty;

        public Guard(InitializationMobileObject init) : base(init)
        {
            foreach (var pameter in init.Parameters)
            {
                switch (pameter.Item1)
                {
                    case "path":
                        var track = GameInformationManager.GetTrackByName(pameter.Item2);
                        normalGuardTrack = track;
                        break;
                }
            }
        }

        protected override Query GetIntentionOfCreature()
        {
            UpdateLevelOfAlertness();

            switch (levelOfAlertness)
            {
                case LevelOfAlertness.Calm:
                    UpdateActionQueueWithClam();
                    break;
                case LevelOfAlertness.Wary:
                    UpdateActionQueueWithWary();
                    break;
                case LevelOfAlertness.Angry:
                    UpdateActionQueueWithAngry();
                    break;
                case LevelOfAlertness.Alert:
                    UpdateActionQueueWithAlert();
                    break;
            }

            return actionQueue.Count == 0 ? Query.None : actionQueue.PeekFront();
        }

        public override void ActionTaken(Query query)
        {
            if (actionQueue.Count != 0)
                actionQueue.PopFront();
            if (query == Query.Move)
                MapManager.AddNoiseSourse(new NoiseSource(NoiseType.StepsOfGuard, 1, 4, Position, "S"));
        }

        public override void ActionRejected(Query query)
        {
            switch (query)
            {
                case Query.Move:
                    var target = Position + GameState.ConvertDirectionToSize[Direction];
                    if (!MapManager.InBounds(target))
                        return;
                    if (MapManager.Map[target.X, target.Y].ObjectContainer.ShowDecor() is ClosedDoor)
                        actionQueue.PushFront(Query.Interaction);
                    break;
            }
        }

        public override void Interative(ICreature creature)
        {
            if (creature is Player)
            {
                if (Inventory.Items.Count == 0)
                    return;
                var item = Inventory.Items.First();
                Inventory.RemoveItem(item);
                creature.Inventory.AddItem(item);
            }
        }

        private void UpdateActionQueueWithClam()
        {
            if (actionQueue.Count == 0)
                ExecuteCurrentInstruction();
        }

        private void UpdateActionQueueWithWary()
        {
            if (changedLevelOfAlertness)
            {
                CheckTheSituation();
            }
            else if (actionQueue.Count == 0)
            {
                levelOfAlertness = LevelOfAlertness.Calm;
            }
        }

        private void UpdateActionQueueWithAngry()
        {
            if (changedLevelOfAlertness && searchTime != 0 && actionQueue.Count == 0)
                SearchPlayer();
        }

        private void UpdateActionQueueWithAlert()
        {
            СhasePlayer();
        }

        private void СhasePlayer()
        {
            var instructions = PathFinder.GetPathFromTo(Position, target, Direction);
            actionQueue = new Deque<Query>();
            for (var i = 0; i < instructions.Count - 1; i++)
            {
                actionQueue.PushBack(instructions[i]);
            }
            actionQueue.PushBack(Query.Interaction);
        }

        private void SearchPlayer()
        {
            for (var i = 0; i < searchTime; i++)
            {
                actionQueue.PushBack(GameState.Random.Next(0, 1) == 0 ? Query.RotateLeft : Query.RotateRight);
            }
        }

        private void CheckTheSituation()
        {
            var instructions = PathFinder.GetPathFromTo(Position, target, Direction);
            for (var i = 0; i < instructions.Count - 1; i++)
            {
                actionQueue.PushBack(instructions[i]);
            }
        }

        private void ExecuteCurrentInstruction()
        {
            if (normalGuardTrack.Count == 0)
                return;

            switch (normalGuardTrack[currentInstruction].GetNextParameter())
            {
                case "MoveTo":
                    var target = new Point(int.Parse(normalGuardTrack[currentInstruction].GetNextParameter()),
                                           int.Parse(normalGuardTrack[currentInstruction].GetNextParameter()));
                    var instructions = PathFinder.GetPathFromTo(Position, target, Direction);
                    foreach (var instruction in instructions)
                        actionQueue.PushBack(instruction);
                    break;
            }
            normalGuardTrack[currentInstruction].ResetInstruction();
            currentInstruction = (currentInstruction + 1) % normalGuardTrack.Count;
        }

        private void UpdateLevelOfAlertness()
        {
            changedLevelOfAlertness = false;
            foreach (var point in VisibleCells)
            {
                var cell = MapManager.Map[point.X, point.Y];

                if (levelOfAlertness == LevelOfAlertness.Angry && !changedLevelOfAlertness)
                {
                    if (searchTime != 0)
                        searchTime--;
                    else
                        ChangeLevelOfAlertness(LevelOfAlertness.Calm, Point.Empty);
                }

                if (cell.Creature is Player)
                {
                    if (levelOfAlertness > LevelOfAlertness.Alert)
                        continue;
                    ChangeLevelOfAlertness(LevelOfAlertness.Alert, point);
                }
                else if (levelOfAlertness == LevelOfAlertness.Alert && point == target)
                {
                    ChangeLevelOfAlertness(LevelOfAlertness.Angry, target);
                    searchTime = SearchTime;
                }
            }

            foreach (var noise in AudibleNoises)
            {
                if (!FamiliarNoises.Contains(noise.Source.Type))
                {
                    if (levelOfAlertness < LevelOfAlertness.Wary)
                    {
                        ChangeLevelOfAlertness(LevelOfAlertness.Wary, noise.Source.Position);
                    }
                }
            }
        }

        private void ChangeLevelOfAlertness(LevelOfAlertness newLevel, Point point)
        {
            levelOfAlertness = newLevel;
            changedLevelOfAlertness = true;
            target = point;
        }

        private readonly HashSet<NoiseType> FamiliarNoises = new HashSet<NoiseType>
        {
            NoiseType.StepsOfGuard,
            NoiseType.GuardVoice
        };
    }
}
