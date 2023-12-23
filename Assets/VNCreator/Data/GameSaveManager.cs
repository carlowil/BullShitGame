using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VNCreator.VNCreator.Data
{
    public static class GameSaveManager
    {
        public static string currentLoadName = string.Empty;

        public static List<string> Load(string loadName)
        {
            if (loadName == string.Empty)
            {
                currentLoadName = loadName;
                return null;
            }

            if (!PlayerPrefs.HasKey(currentLoadName))
            {
                Debug.LogError("You have not saved anything with the name " + currentLoadName);
                return null;
            }

            var loadString = PlayerPrefs.GetString(currentLoadName);
            var loadList = loadString.Split('_').ToList();
            loadList.RemoveAt(loadList.Count - 1);
            currentLoadName = loadName;
            return loadList;
        }

        public static List<string> Load()
        {
            if (currentLoadName == string.Empty)
            {
                return null;
            }

            if (!PlayerPrefs.HasKey(currentLoadName))
            {
                Debug.LogError("You have not saved anything with the name " + currentLoadName);
                return null;
            }

            var loadString = PlayerPrefs.GetString(currentLoadName);
            var loadList = loadString.Split('_').ToList();
            return loadList;
        }

        public static void Save(List<string> storyPath)
        {
            var save = string.Join("_", storyPath.ToArray());
            PlayerPrefs.SetString(currentLoadName, save);
        }

        public static void NewLoad(string saveName)
        {
            currentLoadName = saveName;
            PlayerPrefs.SetString(saveName, string.Empty);
        }
    }
}
