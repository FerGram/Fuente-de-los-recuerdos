using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueDisplay : MonoBehaviour
{
   [Header("Dialogue UI")]
    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _dialogueText;
    [SerializeField] Image _mainCharImage;
    [SerializeField] Image _NPCImage;
    [SerializeField] Image _greyBackground;

    [Header("Choices UI")]
    [SerializeField] GameObject[] _choices;

    [Header("Other")]
    [SerializeField] float _displaySpeed = 0.01f;
    [SerializeField] GameEvent _dialogueEnded;
    [SerializeField] JSONDataContainer _JSONDataContainer;

    private Story _currentDialogue; 
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

    public void StartDialogue()
    {
        _currentDialogue = new Story(_JSONDataContainer.GetJSON().text);
        isPlaying = true;

        DisplayDialogueUI(true);
        FadeInBackground(true);

        ContinueStory();
    }

    private void DisplayDialogueUI(bool value){

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(value);
        }
    }

    public void FadeInBackground(bool value)
    {
        Animator bgAnim = _greyBackground.GetComponent<Animator>();

        if (bgAnim != null) bgAnim.SetBool("fade", value);
    }

    private void ContinueStory()
    {
        StopAllCoroutines();

        if (_currentDialogue.canContinue){

            string text = _currentDialogue.Continue();
            text = SetDialogueName(text);
            StartCoroutine(TypeWritingEffect(text));

            DisplayTalkingCharacter();
            DisplayChoices();
        }
        else{ ExitDialogue(); }
    }

    private void ExitDialogue()
    {
        isPlaying = false;
        _dialoguePanel.SetActive(false);
        _dialogueText.text = "";
        
        DisplayDialogueUI(false);
        FadeInBackground(false);

        //Game Event
        _dialogueEnded.Raise();
    }

    private string SetDialogueName(string text){

        int i = 0;
        string name = "";
        while(text[i] != '.'){

            name += text[i];
            i++;
        }
        _nameText.text = name;
        text = text.Substring(i + 1);

        return text;
    }

    IEnumerator TypeWritingEffect (string text){

        _dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_displaySpeed);
        }

    }

    private void DisplayTalkingCharacter(){

        Animator mainAnimator = _mainCharImage.gameObject.GetComponent<Animator>();
        Animator NPCAnimator = _NPCImage.gameObject.GetComponent<Animator>();

        if (_nameText.text == "Patrick"){

            if (mainAnimator != null) mainAnimator.SetBool("talks", true);
            if (NPCAnimator != null) NPCAnimator.SetBool("talks", false);
        }
        else{

            if (mainAnimator != null) mainAnimator.SetBool("talks", false);
            if (NPCAnimator != null) NPCAnimator.SetBool("talks", true);
        }
    }

    private void DisplayChoices(){

        //Get current INK file choices
        List<Choice> currentChoices = _currentDialogue.currentChoices;

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

        //Deactivate choices
        for (int i = 0; i < _choices.Length; i++)
        {
            _choices[i].SetActive(false);
        }

        //Tell Ink the choice made
        _currentDialogue.ChooseChoiceIndex(choiceIndex);
    }
}
