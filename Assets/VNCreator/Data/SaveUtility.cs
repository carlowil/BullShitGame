using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VNCreator.VNCreator.Editor.Graph;
using VNCreator.VNCreator.Editor.Graph.Node;
#if UNITY_EDITOR
#endif

namespace VNCreator.VNCreator.Data
{
    public class SaveUtility
    {
#if UNITY_EDITOR
        public static void SaveGraph(StoryObject story, ExtendedGraphView graph)
        {
            EditorUtility.SetDirty(story);

            var links = new List<Link>();

            var nodes = graph.nodes.ToList()
                .Cast<BaseNode>()
                .ToList()
                .Select(node => new NodeData
                {
                    guid = node.nodeData.guid,
                    characterSpr = node.nodeData.characterSpr,
                    characterName = node.nodeData.characterName,
                    dialogueText = node.nodeData.dialogueText,
                    backgroundSpr = node.nodeData.backgroundSpr,
                    startNode = node.nodeData.startNode,
                    endNode = node.nodeData.endNode,
                    choices = node.nodeData.choices,
                    choiceOptions = node.nodeData.choiceOptions,
                    nodePosition = node.GetPosition(),
                    soundEffect = node.nodeData.soundEffect,
                    backgroundMusic = node.nodeData.backgroundMusic
                })
                .ToList();

            var edges = graph.edges.ToList();
            for (var i = 0; i < edges.Count; i++)
            {
                var output = (BaseNode)edges[i].output.node;
                var input = (BaseNode)edges[i].input.node;

                links.Add(new Link 
                { 
                    guid = output.nodeData.guid,
                    targetGuid = input.nodeData.guid,
                    portId = i
                });
            }

            story.SetLists(nodes, links);

            //_story.nodes = nodes;
            //_story.links = links;
        }

        public void LoadGraph(StoryObject story, ExtendedGraphView graph)
        {
            foreach (var tempNode in story.nodes.Select(data => graph.CreateNode("", data.nodePosition.position, data.choices, data.choiceOptions, data.startNode, data.endNode, data)))
            {
                graph.AddElement(tempNode);
            }

            GenerateLinks(story, graph);
        }

        private void GenerateLinks(StoryObject story, ExtendedGraphView graph)
        {
            var nodes = graph.nodes.ToList().Cast<BaseNode>().ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                var outputIdx = 1;
                var links = story.links.Where(x => x.guid == nodes[i].nodeData.guid).ToList();
                for (var j = 0; j < links.Count; j++)
                {
                    var targetGuid = links[j].targetGuid;
                    var target = nodes.First(x => x.nodeData.guid == targetGuid);
                    LinkNodes(nodes[i].outputContainer[links.Count > 1 ? outputIdx : 0].Q<Port>(), (Port)target.inputContainer[0], graph);
                    outputIdx += 2;
                }
            }
        }

        private static void LinkNodes(Port output, Port input, ExtendedGraphView graph)
        {
            //Debug.Log(_output);

            var temp = new Edge
            {
                output = output,
                input = input
            };

            temp.input.Connect(temp);
            temp.output.Connect(temp);
            graph.Add(temp);
        }
#endif
    }
}
