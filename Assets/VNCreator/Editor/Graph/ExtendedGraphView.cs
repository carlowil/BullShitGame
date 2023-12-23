using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VNCreator.VNCreator.Data;
using VNCreator.VNCreator.Editor.Graph.Node;
#if UNITY_EDITOR
#endif

namespace VNCreator.VNCreator.Editor.Graph
{
#if UNITY_EDITOR
    public class ExtendedGraphView : GraphView
    {
        private Vector2 _mousePosition;

        public ExtendedGraphView()
        {
            SetupZoom(0.1f, 2);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }

        public void GenerateNode(string nodeName, Vector2 mousePos, int choiceAmount, bool startNode, bool endNode)
        {
            AddElement(CreateNode(nodeName, mousePos, choiceAmount, startNode, endNode));
        }

        private BaseNode CreateNode(string nodeName, Vector2 mousePos, int choiceAmount, bool startNode, bool endNode)
        {
            return CreateNode(nodeName, mousePos, choiceAmount, new string[choiceAmount].ToList(), startNode, endNode, new NodeData());
        }

        public BaseNode CreateNode(string nodeName, Vector2 mousePos, int choiceAmount, List<string> choices, bool startNode, bool endNode, NodeData data)
        {
            var node = new BaseNode(data)
            {
                title = nodeName
            };

            node.SetPosition(new Rect((new Vector2(viewTransform.position.x, viewTransform.position.y) * -(1 / scale)) + (mousePos * (1/scale)), Vector2.one));
            node.nodeData.startNode = startNode;
            node.nodeData.endNode = endNode;
            node.nodeData.choices = choiceAmount;
            node.nodeData.choiceOptions = choices;

            if (!startNode)
            {
                var inputPort = CreatePort(node, Direction.Input, Port.Capacity.Multi);
                inputPort.portName = "Input";
                node.inputContainer.Add(inputPort);
            }

            if (!endNode)
            {
                if (choiceAmount > 1)
                {
                    for (var i = 0; i < choiceAmount; i++)
                    {
                        var outputPort = CreatePort(node, Direction.Output, Port.Capacity.Single);
                        outputPort.portName = "Choice " + (i + 1);

                        //_node.nodeData.choiceOptions.Add(_node.nodeData.choiceOptions[i]);

                        var value = data.choiceOptions.Count == 0 ? "Choice " + (i + 1) : node.nodeData.choiceOptions[i];
                        var idx = i;

                        var field = new TextField { value = value };
                        field.RegisterValueChangedCallback(
                            _ =>
                            {
                                node.nodeData.choiceOptions[idx] = field.value;
                            }
                            );

                        node.outputContainer.Add(field);
                        node.outputContainer.Add(outputPort);
                    }
                }
                else
                {
                    var outputPort = CreatePort(node, Direction.Output, Port.Capacity.Single);
                    outputPort.portName = "Next";
                    node.outputContainer.Add(outputPort);
                }
            }
            
            node.mainContainer.Add(node.visuals);

            node.RefreshExpandedState();
            node.RefreshPorts();

            return node;
        }

        private static Port CreatePort(BaseNode node, Direction portDir, Port.Capacity capacity)
        {
            return node.InstantiatePort(Orientation.Horizontal, portDir, capacity, typeof(float));
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach((port) =>
            {
                if (startPort != port && startPort.node != port.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        private void MousePos(Vector2 v2)
        {
            _mousePosition = v2;
        }
    }
#endif
}