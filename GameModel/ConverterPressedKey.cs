using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel
{
    public static class ConverterPressedKey
    {
        private static Dictionary<Keys, Query> convertKeyToQuery = new Dictionary<Keys, Query>();

        public static Query Convert(Keys keyPressed) =>
            !convertKeyToQuery.ContainsKey(keyPressed) ? Query.None : convertKeyToQuery[keyPressed];

        public static void CreateConverter(Dictionary<Keys, Query> converter)
        {
            convertKeyToQuery = new Dictionary<Keys, Query>(converter);
        }

        public static void ChangeMatching(Keys key, Query query)
        {
            if (convertKeyToQuery.ContainsKey(key))
                convertKeyToQuery[key] = query;
            else
                convertKeyToQuery.Add(key, query);
        }
    }
}
