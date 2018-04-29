using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ServiceClasses
{
    public class MapStateReadingResult
    {
        public bool WasSuccessful = true;
        public int Width = -1;
        public int Height = -1;
        public List<List<string>> MapInfo = new List<List<string>>();
    }
}
