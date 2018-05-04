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
        private Deque<Query> actionQueue = new Deque<Query>();
        private int levelOfAlertness;
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
            changedLevelOfAlertness = false;
            UpdateLevelOfAlertness();
            if (levelOfAlertness == Calm)
            {
                if (actionQueue.Count == 0)
                    ExecuteCurrentInstruction();
            }
            else if (levelOfAlertness == Wary)
            {
                if (changedLevelOfAlertness)
                {
                    CheckTheSituation();
                }
                else if (actionQueue.Count == 0)
                {
                    levelOfAlertness = Calm;
                }
            }
            else if (levelOfAlertness == Angry)
            {
                if (changedLevelOfAlertness && searchTime != 0 && actionQueue.Count == 0)
                    SearchPlayer();
            }
            else if (levelOfAlertness == Alert)
            {
                СhasePlayer();
            }

              return actionQueue.Count == 0 ? Query.None : actionQueue.PeekFront();
        }

        private void СhasePlayer()
        {
            var instructions = PathFinder.GetPathFromTo(Position, target, SightDirection);
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
            var instructions = PathFinder.GetPathFromTo(Position, target, SightDirection);
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

                if (levelOfAlertness == Angry && !changedLevelOfAlertness)
                {
                    if (searchTime != 0)
                        searchTime--;
                    else
                    {
                        ChangeLevelOfAlertness(Calm);
                        target = Point.Empty;
                    }
                }

                if (cell.Creature is Player)
                {
                    if (levelOfAlertness <= Alert)
                    {
                        ChangeLevelOfAlertness(Alert);
                        target = point;
                    }
                }
                else if (levelOfAlertness == Alert && point == target)
                {
                    ChangeLevelOfAlertness(Angry);
                    searchTime = SearchTime;
                }
            }

            foreach (var noise in AudibleNoises)
            {
                if (noise.Source.Type != NoiseType.GuardVoice)
                {
                    if (levelOfAlertness < Wary)
                    {
                        ChangeLevelOfAlertness(Wary);
                        target = noise.Source.Position;
                    }
                }
            }
        }

        private void ChangeLevelOfAlertness(int newLevel)
        {
            levelOfAlertness = newLevel;
            changedLevelOfAlertness = true;
        }

        public override void ActionTaken(Query query)
        {
            if (actionQueue.Count != 0)
                actionQueue.PopFront();
        }

        public override void ActionRejected(Query query)
        {
            var target = Position + GameState.ConvertDirectionToSize[SightDirection];
            if (!MapManager.InBounds(target))
                return;
            if (MapManager.Map[target.X, target.Y].ObjectContainer.ShowDecor() is ClosedDoor)
                actionQueue.PushFront(Query.Interaction);
        }

        public override void Interative(ICreature creature)
        {
        }
    }
}
