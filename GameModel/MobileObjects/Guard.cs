using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects
{
    public class Guard : MobileObject
    {
        private const int Calm = 0;
        private const int Wary = 1;
        private const int Angry = 2;
        private const int Alert = 3;
        private const int SearchTime = 10;

        private readonly List<Instruction> normalGuardTrack = new List<Instruction>();
        private int currentInstruction;
        private readonly Deque<Query> actionQueue = new Deque<Query>();
        private int levelOfAlertness = 0;

        private Dictionary<Point, IDecor> previouslyVisibleCells = new Dictionary<Point, IDecor>();

        private int searchTime = 0;

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
            if (levelOfAlertness == Calm)
            {
                if (actionQueue.Count == 0)
                    ExecuteCurrentInstruction();
            }
            else if (levelOfAlertness == Wary)
            {
                if (actionQueue.Count == 0)
                    levelOfAlertness = Calm;
            }

            return actionQueue.Count == 0 ? Query.None : actionQueue.PeekFront();
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
                    var instructions = PathFinder.GetPathFromTo(Position, target, SightDirection);
                    foreach (var instruction in instructions)
                        actionQueue.PushBack(instruction);
                    break;
            }
            normalGuardTrack[currentInstruction].ResetInstruction();
            currentInstruction = (currentInstruction + 1) % normalGuardTrack.Count;
        }

        private void UpdateLevelOfAlertness()
        {
            foreach (var point in VisibleCells)
            {
                var cell = MapManager.Map[point.X, point.Y];

                if (levelOfAlertness == Angry)
                {
                    if (searchTime != 0)
                        searchTime--;
                    else
                        levelOfAlertness = Calm;
                }

                if (cell.Creature is Player)
                {
                    levelOfAlertness = Math.Max(levelOfAlertness, Alert);
                }
                else if (levelOfAlertness == Alert)
                {
                    levelOfAlertness = Angry;
                    searchTime = SearchTime;
                }
            }

            foreach (var noise in AudibleNoises)
            {
                if (noise.Source.Type != NoiseType.Guard)
                    levelOfAlertness = Math.Max(levelOfAlertness, Wary);
            }
        }

        public override void ActionTaken(Query query)
        {
            if (actionQueue.Count != 0)
                actionQueue.PopFront();
        }

        public override void ActionRejected(Query query)
        {
            var target = Position + GameState.ConvertDirectionToSize[SightDirection];
            if (MapManager.Map[target.X, target.Y].ObjectContainer.ShowDecor() is ClosedDoor)
                actionQueue.PushFront(Query.Interaction);
        }

        public override void Interative(ICreature creature)
        {
        }
    }
}
