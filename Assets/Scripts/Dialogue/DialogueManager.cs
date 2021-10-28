using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _dialogueText;

    private Story _currentStory; 

    public bool isPlaying {get; private set;}

    //public Animator _animator;
    
    private void Awake() {

        _dialoguePanel.SetActive(false);
        isPlaying = false;
    }

    private void Update() {
        
        if (!isPlaying) return;

        if (Input.GetMouseButtonDown(0)) ContinueStory();
    }

    public void StartDialogue(TextAsset dialogue){

        _currentStory = new Story(dialogue.text);
        isPlaying = true;
        _dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ContinueStory()
    {
        StopAllCoroutines();

        if (_currentStory.canContinue){

            string text = _currentStory.Continue();
            StartCoroutine(TypeWritingEffect(text));
        }
        else{ ExitDialogue(); }
    }

    private void ExitDialogue()
    {
        isPlaying = false;
        _dialoguePanel.SetActive(false);
        _dialogueText.text = "";
    }

    IEnumerator TypeWritingEffect (string text){

        _dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
