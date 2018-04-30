using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects
{
    public class Guard : MobileObject
    {
        private List<Instruction> normalGuardTrack;

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
                    default:
                        break;
                }
            }
        }

        protected Query GetIntentionOfCreature()
        {
            throw new NotImplementedException();
        }

        public void ActionTaken(Query query)
        {
            throw new NotImplementedException();
        }

        public void ActionRejected(Query query)
        {
            throw new NotImplementedException();
        }

        public void Interative(ICreature creature)
        {
        }
    }
}
