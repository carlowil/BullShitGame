using UnityEditor;
using UnityEngine;
using VNCreator.VNCreator.Data;

namespace VNCreator.VNCreator.Editor.Windows
{
#if UNITY_EDITOR
    [CustomEditor(typeof(StoryObject))]
    public class StoryObjectEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(40);

            if (GUILayout.Button("Open", GUILayout.Height(40)))
            {
                StoryObjectEditorWindow.Open((StoryObject)target);
            }
        }
    }
#endif
}
