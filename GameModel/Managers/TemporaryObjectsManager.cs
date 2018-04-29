using System.Collections.Generic;

namespace GameThief.GameModel.Managers
{
    public static class TemporaryObjectsManager
    {
        private static HashSet<ITemporaryObject> temporaryObjects = new HashSet<ITemporaryObject>();

        public static void UpdateTemporaryObjects()
        {
            foreach (var obj in temporaryObjects)
            {
                obj.Update();
                if (obj.IsActive())
                    continue;
                obj.ActionAfterDeactivation();
                temporaryObjects.Remove(obj);
            }
        }

        public static void AddTemporaryObject(ITemporaryObject temporaryObject)
        {
            temporaryObjects.Add(temporaryObject);
        }
    }
}
