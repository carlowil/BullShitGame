using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VNCreator.VNCreator.Data
{
    [CreateAssetMenu(fileName = "New Story", menuName = "New Story")]
    public class StoryObject : ScriptableObject
    {
        [HideInInspector] public List<Link> links;
        [HideInInspector] public List<NodeData> nodes;

        public void SetLists(List<NodeData> nodes, List<Link> links)
        {
            this.links = new List<Link>();
            foreach (var t in links)
            {
                this.links.Add(t);
            }

            this.nodes = new List<NodeData>();
            
            foreach (var t in nodes)
            {
                this.nodes.Add(t);
            }
        }

        public NodeData GetFirstNode()
        {
            foreach (var t in nodes.Where(t => t.startNode))
            {
                return t;
            }

            Debug.LogError("You need a start node");
            return null;
        }
        public NodeData GetCurrentNode(string currentGuid)
        {
            return nodes.FirstOrDefault(t => t.guid == currentGuid);
        }

        private List<Link> _tempLinks = new();
        public NodeData GetNextNode(string currentGuid, int choiceId)
        {
            _tempLinks = new List<Link>();

            foreach (var t in links.Where(t => t.guid == currentGuid))
            {
                _tempLinks.Add(t);
            }

            return choiceId < _tempLinks.Count ? GetCurrentNode(_tempLinks[choiceId].targetGuid) : null;
        }
    }
}
