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
        private const string MapStateFilename = "map-state.txt";
        private const string MobileObjectsStateFilename = "mobile-objects-state.txt";

        public static MapStateReadingResult ReadMapState(string sourceName)
        {
            var result = new MapStateReadingResult();
            var pathToSource = GetPathToSource(sourceName);

            if (!Directory.Exists(pathToSource))
                throw new Exception("Не существует директории сохранения: " + pathToSource);

            var pathToMapStateFile = Path.Combine(pathToSource, MapStateFilename);

            if (!File.Exists(pathToMapStateFile))
                throw new Exception("Не существует файла информации о карте: " + pathToMapStateFile);

            var content = File.ReadLines(pathToMapStateFile);
            if (!content.Any())
            {
                result.WasSuccessful = false;
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
                        result.WasSuccessful = false;
                        return result;
                    }
                    result.Width = size[0];
                    result.Height = size[1];
                }
                result.MapInfo.Add(line.Split(' ').ToList());
            }
            if (result.MapInfo.Count != result.Width * result.Height)
                result.WasSuccessful = false;
            return result;
        }

        private static string GetPathToSource(string nameSource) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Saves", nameSource);
    }
}
