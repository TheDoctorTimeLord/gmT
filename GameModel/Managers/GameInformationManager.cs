using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.Managers
{
    public static class GameInformationManager
    {
        private static Dictionary<string, List<Instruction>> tracksByName = new Dictionary<string, List<Instruction>>();

        public static List<Instruction> GetTrackByName(string trackName)
        {
            // TODO Доработать возожность логирования, при запросе по ключу, не содержащемуся в словаре
            if (tracksByName.ContainsKey(trackName))
                return new List<Instruction>(tracksByName[trackName]);
            return null;
        }

        public static void CreateTrackByName(Dictionary<string, List<Instruction>> newTrackByName)
        {
            tracksByName = new Dictionary<string, List<Instruction>>(newTrackByName);
        }
    }
}
