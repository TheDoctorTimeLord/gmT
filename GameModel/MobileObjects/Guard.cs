using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects
{
    public class Guard : MobileObject
    {
        private const int Calm = 0;
        private const int Alert = 1;
        private const int Angry = 1;

        private List<Instruction> normalGuardTrack;
        private int currentInstruction;
        private readonly Queue<Query> actionQueue = new Queue<Query>();
        private int levelOfAlertness = 0;

        private Dictionary<Point, IDecor> previouslyVisibleCells = new Dictionary<Point, IDecor>();

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
            if (actionQueue.Count == 0 && levelOfAlertness == Calm)
            {
                ExecuteCurrentInstruction();
            }

            return actionQueue.Count == 0 ? Query.None : actionQueue.Peek();
        }

        private void ExecuteCurrentInstruction()
        {
            if (normalGuardTrack.Count == 0)
                return;

            switch (normalGuardTrack[currentInstruction].InstructionType)
            {
                case AIActionType.MoveTo:
                    var instructions = PathFinder.GetPathFromTo(Position, normalGuardTrack[currentInstruction].Position, SightDirection);
                    foreach (var instruction in instructions)
                        actionQueue.Enqueue(instruction);
                    break;
            }
            currentInstruction = (currentInstruction + 1) % normalGuardTrack.Count;
        }

        private void UpdateLevelOfAlertness()
        {
            //foreach (var possition in VisibleCells)
            //{
            //    var currentCell = MapManager.Map[possition.X, possition.Y];
            //    if (!previouslyVisibleCells.ContainsKey(possition))
            //    {
            //        previouslyVisibleCells.Add(possition, currentCell.ObjectContainer.GetTopDecor());
            //    }
            //    else
            //    {
            //        if (currentCell.Creature is Player)
            //            levelOfAlertness = Math.Max(levelOfAlertness, Angry);
            //        else if (currentCell.ObjectContainer.GetTopDecor() != previouslyVisibleCells[possition])
            //        {
            //            levelOfAlertness = Math.Max(levelOfAlertness, Alert);
            //            previouslyVisibleCells[possition] = currentCell.ObjectContainer.GetTopDecor();
            //        }
            //    }
            //}
        }

        public override void ActionTaken()
        {
            if (actionQueue.Count != 0)
                actionQueue.Dequeue();
        }

        public override void ActionRejected()
        {
        }

        public override void Interative(ICreature creature)
        {
        }
    }
}
