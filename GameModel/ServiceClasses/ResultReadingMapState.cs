using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ServiceClasses
{
    public class ResultReadingMapState
    {
        public bool IsSuccessfulReading = true;
        public int Width = -1;
        public int Height = -1;
        public List<List<string>> InfoAboutMap = new List<List<string>>();
    }
}
