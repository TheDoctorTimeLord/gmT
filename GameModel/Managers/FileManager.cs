using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.Managers
{
    static class FileManager
    {
        private const string NameMapState = "map-state.txt";
        private const string NameAnimateState = "animate-state.txt";

        public static ResultReadingMapState ReadingMapState(string nameSource)
        {
            var result = new ResultReadingMapState();
            var pathToSource = GetPathToSours(nameSource);

            if (!Directory.Exists(pathToSource))
                throw new Exception("Не существует директории сохранения: " + pathToSource);

            var pathToMapState = Path.Combine(pathToSource, NameMapState);

            if (!File.Exists(pathToMapState))
                throw new Exception("Не существует файла информации о карте: " + pathToMapState);

            var content = File.ReadLines(pathToMapState);
            if (!content.Any())
            {
                result.IsSuccessfulReading = false;
                return result;
            }

            var isSize = true;
            foreach (var line in content)
            {
                if (isSize)
                {
                    isSize = false;
                    var size = line.Split(' ').Select(int.Parse).ToList();
                    if (size.Count != 2)
                    {
                        result.IsSuccessfulReading = false;
                        return result;
                    }
                    result.Width = size[0];
                    result.Height = size[1];
                }
                result.InfoAboutMap.Add(line.Split(' ').ToList());
            }
            if (result.InfoAboutMap.Count != result.Width * result.Height)
                result.IsSuccessfulReading = false;
            return result;
        }

        private static string GetPathToSours(string nameSource) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Saves", nameSource);
    }
}
