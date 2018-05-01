using System.Collections.Generic;

namespace GameThief.GameModel.Managers
{
    public static class TemporaryObjectsManager
    {
        private static HashSet<ITemporaryObject> temporaryObjects = new HashSet<ITemporaryObject>();
        private static List<ITemporaryObject> temporaryObjectsToRemove = new List<ITemporaryObject>();

        public static void UpdateTemporaryObjects()
        {
            foreach (var obj in temporaryObjects)
            {
                obj.Update();
                if (obj.IsActive())
                    continue;
                obj.ActionAfterDeactivation();
                temporaryObjectsToRemove.Add(obj);
            }
            foreach (var obj in temporaryObjectsToRemove)
            {
                temporaryObjects.Remove(obj);
            }
            temporaryObjectsToRemove.Clear();
        }

        public static void AddTemporaryObject(ITemporaryObject temporaryObject)
        {
            temporaryObjects.Add(temporaryObject);
        }
    }
}
