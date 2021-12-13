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

    public Story _currentDialogue; 
    private TextMeshProUGUI[] _choicesText;
    private bool _canPressSpace = true;

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

        if (Input.GetKeyDown(KeyCode.Space) && _canPressSpace) ContinueStory();
    }

    public void StartDialogue()
    {
        _currentDialogue = GameStateData.Instance.gameStory;

		if (_currentDialogue == null)
		{
            GameStateData.Instance.gameStory = new Story(_JSONDataContainer.GetJSON().text);
            GameStateData.Instance.BindStoryFunctions();
			_currentDialogue = GameStateData.Instance.gameStory;
        }

		_currentDialogue.ChoosePathString(_JSONDataContainer.GetPath());

        isPlaying = true;

        DisplayDialogueUI(true);
        FadeInBackground(true);

        ContinueStory();
    }

    public void EnterMinigameResponse(int minigame){

        DisableUI();
        MinigameEvents.current.LoadMinigame(minigame);
    }
    
    private void DisplayDialogueUI(bool value){

        if (_greyBackground != null) _greyBackground.gameObject.SetActive(value);
        if (_dialoguePanel != null) _dialoguePanel.gameObject.SetActive(value);
        if (_mainCharImage != null) _mainCharImage.gameObject.SetActive(value);
        if (_NPCImage != null) _NPCImage.gameObject.SetActive(value);
    }

    public void FadeInBackground(bool value)
    {
        if (_greyBackground == null) return;
        Animator bgAnim = _greyBackground.GetComponent<Animator>();

        if (bgAnim != null) bgAnim.SetBool("fade", value);
    }

    private void ContinueStory()
    {
        StopAllCoroutines();

        if (_currentDialogue.canContinue){

            if (MinigameEvents.current != null && MinigameEvents.current.insideMinigame) return;

            string text = _currentDialogue.Continue();
            text = SetDialogueName(text);
            StartCoroutine(TypeWritingEffect(text));

            DisplayTalkingCharacter();
            DisplayChoices();
        }
        else{ ExitDialogue(); }
    }

	private void DisableUI()
	{
		DisplayDialogueUI(false);
		FadeInBackground(false);

        //Fuck it
        GameObject menuButton = GameObject.FindGameObjectWithTag("MenuButton");
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");

        if (menuButton != null) menuButton.SetActive(false);
        if (inventory != null) inventory.SetActive(false);
	}

	public void EnableUI(int minigame)
	{
		DisplayDialogueUI(true);
		FadeInBackground(true);

        //Fuck it
        GameObject menuButton = GameObject.FindGameObjectWithTag("MenuButton");
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");

        if (menuButton != null) menuButton.SetActive(true);
        if (inventory != null) inventory.SetActive(true);
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
        while(i < text.Length && text[i] != '<'){

            name += text[i];
            i++;
        }

        if (i == text.Length) {
            _nameText.text = "Patrick";
            return text;
        }

        _nameText.text = name;
        text = text.Substring(i + 1);
        return text;
    }

    IEnumerator TypeWritingEffect (string text){

        _dialogueText.text = text;
		
        for (int i = 0; i < text.Length; i++)
        {
			_dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(_displaySpeed);
        }

    }

    private void DisplayTalkingCharacter(){

        Animator mainAnimator = null;
        Animator NPCAnimator = null;

        if (_mainCharImage != null) mainAnimator = _mainCharImage.gameObject.GetComponent<Animator>();
        if (_NPCImage != null) NPCAnimator = _NPCImage.gameObject.GetComponent<Animator>();

        //Patrick: Main character, Lewis: Hiker
        if (_nameText.text == "Patrick" ||_nameText.text == "Lewis"){

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
        //Can't continue if choice is not made
        _canPressSpace = false;

        //Set the GO to be active and change its text
        for (int i = 0; i < currentChoices.Count; i++)
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
        _canPressSpace = true;
        ContinueStory();

    }
}
