using System.Collections.Generic;
using UnityEngine;
using VNCreator.VNCreator.Data;

namespace VNCreator.VNCreator.Misc
{
    public class DisplayBase : MonoBehaviour
    {
        public StoryObject story;

        protected NodeData currentNode;
        protected bool lastNode;

        protected List<string> loadList = new();

        private void Awake()
        {
            if (PlayerPrefs.GetString(GameSaveManager.currentLoadName) == string.Empty)
            {
                currentNode = story.GetFirstNode();
                loadList.Add(currentNode.guid);
            }
            else
            {
                loadList = GameSaveManager.Load();
                if(loadList == null || loadList.Count == 0)
                {
                    currentNode = story.GetFirstNode();
                    loadList = new List<string>();
                    loadList.Add(currentNode.guid);
                }
                else
                {
                    currentNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
                }
            }
        }

        protected virtual void NextNode(int choiceId)
        {
            if (!lastNode) 
            {
                currentNode = story.GetNextNode(currentNode.guid, choiceId);
                lastNode = currentNode.endNode;
                loadList.Add(currentNode.guid);
            }
        }

        protected virtual void Previous()
        {
            loadList.RemoveAt(loadList.Count - 1);
            currentNode = story.GetCurrentNode(loadList[^1]);
            lastNode = currentNode.endNode;
        }

        protected void Save()
        {
            GameSaveManager.Save(loadList);
        }
    }
}
