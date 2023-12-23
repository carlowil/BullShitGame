﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
using UnityEngine.UIElements;
using System;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace VNCreator
{
#if UNITY_EDITOR
    public class BaseNode : Node
    {
        public NodeData nodeData;
        public NodeViewer visuals;

        public BaseNode(NodeData _data)
        {
            nodeData = _data ?? new NodeData();
            nodeData.characterSprts = new Sprite[3];
            visuals = new NodeViewer(this);
        }
    }

    public class NodeViewer : VisualElement
    {
        BaseNode node;

        public NodeViewer(BaseNode _node)
        {
            node = _node;

            VisualTreeAsset tree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/VNCreator/Editor/Graph/Node/BaseNodeTemplate.uxml");
            tree.CloneTree(this);

            styleSheets.Add(
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/VNCreator/Editor/Graph/Node/BaseNodeStyle.uss"));

            
            for (var i = 0; i < node.nodeData.characterSprts.Length; i++) {
                VisualElement charSprDisplay = this.Query<VisualElement>("Char_Img_1");
                charSprDisplay.style.backgroundImage =
                    node.nodeData.characterSprts[i] ? node.nodeData.characterSprts[i].texture : null;

                var charSprField = this.Query<ObjectField>("Icon_Selection_1").First();
                charSprField.objectType = typeof(Sprite);
                charSprField.value = node.nodeData.characterSprts[i];
                charSprField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(
                    e =>
                    {
                        node.nodeData.characterSprts[i] = (Sprite)e.newValue;
                        charSprDisplay.style.backgroundImage = node.nodeData.characterSprts[i]
                            ? node.nodeData.characterSprts[i].texture
                            : null;
                    }
                );
            }
            
            TextField charNameField = this.Query<TextField>("Char_Name");
            charNameField.value = node.nodeData.characterName;
            charNameField.RegisterValueChangedCallback(
                e => { node.nodeData.characterName = charNameField.value; }
            );

            TextField dialogueField = this.Query<TextField>("Dialogue_Field");
            dialogueField.multiline = true;
            dialogueField.value = node.nodeData.dialogueText;
            dialogueField.RegisterValueChangedCallback(
                e => { node.nodeData.dialogueText = dialogueField.value; }
            );

            ObjectField sfxField = this.Query<ObjectField>("Sound_Field").First();
            sfxField.objectType = typeof(AudioClip);
            sfxField.value = node.nodeData.soundEffect;
            sfxField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(
                e => { node.nodeData.soundEffect = (AudioClip)e.newValue; }
            );

            ObjectField musicField = this.Query<ObjectField>("Music_Field").First();
            musicField.objectType = typeof(AudioClip);
            musicField.value = node.nodeData.soundEffect;
            musicField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(
                e => { node.nodeData.backgroundMusic = (AudioClip)e.newValue; }
            );

            VisualElement backSprDisplay = this.Query<VisualElement>("Back_Img");
            backSprDisplay.style.backgroundImage =
                node.nodeData.backgroundSpr ? node.nodeData.backgroundSpr.texture : null;

            ObjectField backSprField = this.Query<ObjectField>("Back_Selector").First();
            backSprField.objectType = typeof(Sprite);
            backSprField.value = node.nodeData.backgroundSpr;
            backSprField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(
                e =>
                {
                    node.nodeData.backgroundSpr = (Sprite)e.newValue;
                    backSprDisplay.style.backgroundImage =
                        node.nodeData.backgroundSpr ? node.nodeData.backgroundSpr.texture : null;
                }
            );
        }
    }
#endif
}