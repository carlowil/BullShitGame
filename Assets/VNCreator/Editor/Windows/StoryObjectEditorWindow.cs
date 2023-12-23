using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VNCreator.VNCreator.Data;
using VNCreator.VNCreator.Editor.Graph;

namespace VNCreator.VNCreator.Editor.Windows
{
#if UNITY_EDITOR
    public class StoryObjectEditorWindow : EditorWindow
    {
        private StoryObject _storyObj;
        private ExtendedGraphView _graphView;
        private readonly SaveUtility _saveUtility = new();

        private Vector2 _mousePosition;

        public static void Open(StoryObject storyObj)
        {
            var window = GetWindow<StoryObjectEditorWindow>("Story");
            window._storyObj = storyObj;
            window.CreateGraphView(storyObj.nodes == null ? 0 : 1);
            window.minSize = new Vector2(200, 100);
        }

        private void MouseDown(MouseDownEvent e)
        {
            if (e.button != 1) return;
            _mousePosition = Event.current.mousePosition;
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Node"), false, () => _graphView.GenerateNode("", _mousePosition, 1, false, false));
            menu.AddItem(new GUIContent("Add Node (2 Choices)"), false, () => _graphView.GenerateNode("", _mousePosition, 2, false, false));
            menu.AddItem(new GUIContent("Add Node (3 Choices)"), false, () => _graphView.GenerateNode("", _mousePosition, 3, false, false));
            menu.AddItem(new GUIContent("Add Node (Start)"), false, () => _graphView.GenerateNode("", _mousePosition, 1, true, false));
            menu.AddItem(new GUIContent("Add Node (End)"), false, () => _graphView.GenerateNode("", _mousePosition, 1, false, true));
            menu.AddItem(new GUIContent("Save"), false, () => SaveUtility.SaveGraph(_storyObj, _graphView));
            menu.ShowAsContext();
        }

        private void CreateGraphView(int nodeCount)
        {
            _graphView = new ExtendedGraphView();
            _graphView.RegisterCallback<MouseDownEvent>(MouseDown);
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
            if (nodeCount == 0) 
            {
                //graphView.GenerateNode(Vector2.zero, 1, true, false);
                return;
            }
            _saveUtility.LoadGraph(_storyObj, _graphView);
        }
    }
#endif
}


