﻿#if UNITY_EDITOR
#endif
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VNCreator.VNCreator.Data;

namespace VNCreator.VNCreator.Editor.Graph.Node
{
#if UNITY_EDITOR
    public class BaseNode : UnityEditor.Experimental.GraphView.Node
    {
        public readonly NodeData nodeData;
        public readonly NodeViewer visuals;

        public BaseNode(NodeData data)
        {
            nodeData = data ?? new NodeData();
            visuals = new NodeViewer(this);
        }
    }

    public class NodeViewer : VisualElement
    {
        public NodeViewer(BaseNode node)
        {
            var tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/VNCreator/Editor/Graph/Node/BaseNodeTemplate.uxml");
            tree.CloneTree(this);

            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/VNCreator/Editor/Graph/Node/BaseNodeStyle.uss"));

            VisualElement charSprDisplay = this.Query<VisualElement>("Char_Img");
            charSprDisplay.style.backgroundImage = node.nodeData.characterSpr ? node.nodeData.characterSpr.texture : null;

            var charSprField = this.Query<ObjectField>("Icon_Selection").First();
            charSprField.objectType = typeof(Sprite);
            charSprField.value = node.nodeData.characterSpr;
            charSprField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    node.nodeData.characterSpr = (Sprite)e.newValue;
                    charSprDisplay.style.backgroundImage = node.nodeData.characterSpr ? node.nodeData.characterSpr.texture : null;
                }
            );

            TextField charNameField = this.Query<TextField>("Char_Name");
            charNameField.value = node.nodeData.characterName;
            charNameField.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.characterName = charNameField.value;
                }
            );

            TextField dialogueField = this.Query<TextField>("Dialogue_Field");
            dialogueField.multiline = true;
            dialogueField.value = node.nodeData.dialogueText;
            dialogueField.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.dialogueText = dialogueField.value;
                }
            );

            var sfxField = this.Query<ObjectField>("Sound_Field").First();
            sfxField.objectType = typeof(AudioClip);
            sfxField.value = node.nodeData.soundEffect;
            sfxField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    node.nodeData.soundEffect = (AudioClip)e.newValue;
                }
            );

            var musicField = this.Query<ObjectField>("Music_Field").First();
            musicField.objectType = typeof(AudioClip);
            musicField.value = node.nodeData.soundEffect;
            musicField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    node.nodeData.backgroundMusic = (AudioClip)e.newValue;
                }
            );

            VisualElement backSprDisplay = this.Query<VisualElement>("Back_Img");
            backSprDisplay.style.backgroundImage = node.nodeData.backgroundSpr ? node.nodeData.backgroundSpr.texture : null;

            var backSprField = this.Query<ObjectField>("Back_Selector").First();
            backSprField.objectType = typeof(Sprite);
            backSprField.value = node.nodeData.backgroundSpr;
            backSprField.RegisterCallback<ChangeEvent<Object>>(
                e =>
                {
                    node.nodeData.backgroundSpr = (Sprite)e.newValue;
                    backSprDisplay.style.backgroundImage = node.nodeData.backgroundSpr ? node.nodeData.backgroundSpr.texture : null;
                }
            );
        }
    }
#endif
}
