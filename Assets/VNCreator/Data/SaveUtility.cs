using System.Collections.Generic;
using System.Linq;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
using UnityEngine;
#endif
using UnityEngine.UIElements;
using VNCreator.Editors.Graph;

namespace VNCreator
{
    public class SaveUtility
    {
#if UNITY_EDITOR
        public void SaveGraph(StoryObject _story, ExtendedGraphView _graph)
        {
            EditorUtility.SetDirty(_story);

            var nodes = new List<NodeData>();
            var links = new List<Link>();

            foreach (var _node in _graph.nodes.ToList().Cast<BaseNode>().ToList())
            {
                nodes.Add(new NodeData
                    {
                        guid = _node.nodeData.guid,
                        characterSpr = _node.nodeData.characterSpr,
                        characterName = _node.nodeData.characterName,
                        dialogueText = _node.nodeData.dialogueText,
                        backgroundSpr = _node.nodeData.backgroundSpr,
                        startNode = _node.nodeData.startNode,
                        endNode = _node.nodeData.endNode,
                        nextScene = _node.nodeData.nextScene,
                        choices = _node.nodeData.choices,
                        choiceOptions = _node.nodeData.choiceOptions,
                        nodePosition = _node.GetPosition(),
                        soundEffect = _node.nodeData.soundEffect,
                        backgroundMusic = _node.nodeData.backgroundMusic
                    }
                );
            }

            var edges = _graph.edges.ToList();
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

            _story.SetLists(nodes, links);

            //_story.nodes = nodes;
            //_story.links = links;
        }

        public void LoadGraph(StoryObject story, ExtendedGraphView graph)
        {
            foreach (var data in story.nodes)
            {
                var tempNode = graph.CreateNode("", data.nodePosition.position, data.choices,
                    data.choiceOptions, data.startNode, data.endNode, data);
                graph.AddElement(tempNode);
            }

            GenerateLinks(story, graph);
        }

        void GenerateLinks(StoryObject _story, ExtendedGraphView _graph)
        {
            List<BaseNode> _nodes = _graph.nodes.ToList().Cast<BaseNode>().ToList();

            for (int i = 0; i < _nodes.Count; i++)
            {
                int _outputIdx = 1;
                List<Link> _links = _story.links.Where(x => x.guid == _nodes[i].nodeData.guid).ToList();
                for (int j = 0; j < _links.Count; j++)
                {
                    string targetGuid = _links[j].targetGuid;
                    BaseNode _target = _nodes.First(x => x.nodeData.guid == targetGuid);
                    LinkNodes(_nodes[i].outputContainer[_links.Count > 1 ? _outputIdx : 0].Q<Port>(),
                        (Port)_target.inputContainer[0], _graph);
                    _outputIdx += 2;
                }
            }
        }

        void LinkNodes(Port _output, Port _input, ExtendedGraphView _graph)
        {
            Edge _temp = new Edge
            {
                output = _output,
                input = _input
            };

            _temp.input.Connect(_temp);
            _temp.output.Connect(_temp);
            _graph.Add(_temp);
        }
#endif
    }
}