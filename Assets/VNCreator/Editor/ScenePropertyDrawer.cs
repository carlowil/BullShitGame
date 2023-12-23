using UnityEditor;
using UnityEngine;
using VNCreator.VNCreator.Misc;

namespace VNCreator.VNCreator.Editor
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class ScenePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sceneObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);
            var scene = (SceneAsset)EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true);
            property.stringValue = AssetDatabase.GetAssetPath(scene);
        }
    }
#endif
}
