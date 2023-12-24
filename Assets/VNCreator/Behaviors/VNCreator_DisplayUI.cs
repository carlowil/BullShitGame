﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VNCreator
{
    public class VNCreator_DisplayUI : DisplayBase
    {
        [Header("Text")] public Text characterNameTxt;
        public Text dialogueTxt;

        [Header("Visuals")]
        public Image characterImg;

        public Image backgroundImg;
        [Header("Buttons")] public Button nextBtn;
        public Button previousBtn;
        public Button saveBtn;
        public Button menuButton;
        [Header("Choices")] public Button choiceBtn1;
        public Button choiceBtn2;
        public Button choiceBtn3;
        [Header("End")] public GameObject endScreen;
        [Header("Main menu")] [Scene] public string mainMenu;

        private void Start()
        {
            nextBtn.onClick.AddListener(delegate { NextNode(0); });
            if (previousBtn != null)
                previousBtn.onClick.AddListener(Previous);
            if (saveBtn != null)
                saveBtn.onClick.AddListener(Save);
            if (menuButton != null)
                menuButton.onClick.AddListener(ExitGame);

            if (choiceBtn1 != null)
                choiceBtn1.onClick.AddListener(delegate { NextNode(0); });
            if (choiceBtn2 != null)
                choiceBtn2.onClick.AddListener(delegate { NextNode(1); });
            if (choiceBtn3 != null)
                choiceBtn3.onClick.AddListener(delegate { NextNode(2); });

            endScreen.SetActive(false);

            MyStartCoroutine(DisplayCurrentNode());
        }

        protected override void NextNode(int choiceId)
        {
            if (lastNode)
            {
                var path = AssetDatabase.GetAssetPath(currentNode.nextScene);
                
                Debug.Log(path);
                
                SceneManager.LoadScene(path, LoadSceneMode.Single);
                
                endScreen.SetActive(true);
                return;
            }

            base.NextNode(choiceId);
            MyStartCoroutine(DisplayCurrentNode());
        }

        private IEnumerator DisplayCurrentNode()
        {
            characterNameTxt.text = currentNode.characterName;

                if (currentNode.characterSpr != null)
                {
                    characterImg.sprite = currentNode.characterSpr;
                    characterImg.color = Color.white;
                }
                else
                {
                    characterImg.color = new Color(1, 1, 1, 0);
                }

            if (currentNode.backgroundSpr != null)
                backgroundImg.sprite = currentNode.backgroundSpr;

            if (currentNode.choices <= 1)
            {
                nextBtn.gameObject.SetActive(true);

                choiceBtn1.gameObject.SetActive(false);
                choiceBtn2.gameObject.SetActive(false);
                choiceBtn3.gameObject.SetActive(false);

                previousBtn.gameObject.SetActive(loadList.Count != 1);
            }
            else
            {
                nextBtn.gameObject.SetActive(false);

                choiceBtn1.gameObject.SetActive(true);
                choiceBtn1.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[0];

                choiceBtn2.gameObject.SetActive(true);
                choiceBtn2.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[1];

                if (currentNode.choices == 3)
                {
                    choiceBtn3.gameObject.SetActive(true);
                    choiceBtn3.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[2];
                }
                else
                {
                    choiceBtn3.gameObject.SetActive(false);
                }
            }

            if (currentNode.backgroundMusic != null)
                VNCreator_MusicSource.instance.Play(currentNode.backgroundMusic);
            if (currentNode.soundEffect != null)
                VNCreator_SfxSource.instance.Play(currentNode.soundEffect);

            dialogueTxt.text = string.Empty;
            if (GameOptions.isInstantText)
            {
                dialogueTxt.text = currentNode.dialogueText;
            }
            else
            {
                var chars = currentNode.dialogueText.ToCharArray();
                var fullString = string.Empty;
                foreach (var c in chars)
                {
                    fullString += c;
                    dialogueTxt.text = fullString;
                    yield return new WaitForSeconds(0.01f / GameOptions.readSpeed);
                }
            }
        }

        protected override void Previous()
        {
            base.Previous();
            MyStartCoroutine(DisplayCurrentNode());
        }

        private void ExitGame()
        {
            SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
        }

        private IEnumerator _coroutine;
        
        private void MyStartCoroutine(IEnumerator routine)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = routine;
            StartCoroutine(routine);
        }
    }
}