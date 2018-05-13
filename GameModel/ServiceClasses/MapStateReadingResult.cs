using System.Collections.Generic;

namespace GameThief.GameModel.ServiceClasses
{
    public class MapStateReadingResult
    {
        public int Height = -1;
        public List<List<string>> MapInfo = new List<List<string>>();
        public bool WasSuccessful = true;
        public int Width = -1;
    }
}