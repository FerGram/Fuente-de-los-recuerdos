using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("Dialogue UI")]
    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _dialogueText;

    [Header("Choices UI")]
    [SerializeField] GameObject[] _choices;

    [Header("Other")]
    [SerializeField] float _displaySpeed = 0.01f;

    private Story _currentStory; 
    private TextMeshProUGUI[] _choicesText;

    public bool isPlaying {get; private set;}

    //public Animator _animator;
    
    private void Awake() {

        _dialoguePanel.SetActive(false);
        isPlaying = false;
        
        _choicesText = new TextMeshProUGUI[_choices.Length];

        for (int i = 0; i < _choices.Length; i++)
        {
            _choicesText[i] = _choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
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
            DisplayChoices();
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
            yield return new WaitForSeconds(_displaySpeed);
        }

    }

    private void DisplayChoices(){

        //Get current INK file choices
        List<Choice> currentChoices = _currentStory.currentChoices;

        //INK file choices can't be 0 or more than the amount of choice G.O given to the UI 
        if (currentChoices.Count == 0) return;
        if (currentChoices.Count > _choices.Length){

            Debug.LogError("Number of choices given exceeds the amount of choices the UI can support");
            return;
        }

        //Set the GO to be active and change its text
        for (int i = 0; i < _choicesText.Length; i++)
        {
            _choices[i].SetActive(true);
            _choicesText[i].text = currentChoices[i].text;
        }
    }

    //This method is triggered in the OnClick Event of Choice GameObject
    public void MakeChoice(int choiceIndex){

        _currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
