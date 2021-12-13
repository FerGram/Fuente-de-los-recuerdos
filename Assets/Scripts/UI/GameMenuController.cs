using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class GameMenuController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    //To activate or deactivate
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _optionsMenu;
    [SerializeField] GameObject _volumeMenu;

    //Selected buttons
    [Space]
    [SerializeField] GameObject _mainFirstButton;
    [SerializeField] GameObject _optionsFirstButton;
    [SerializeField] GameObject _optionsClosedButton;
    [SerializeField] GameObject _volumeFirstButton;

    [Space]
    [SerializeField] Image[] _volumeBars;
    [SerializeField] AudioMixer _audioMixer;

    [Space]
    [SerializeField] Color32 _normalTextColor;
    [SerializeField] Color32 _selectedTextColor;

    private int _maxVolume = 8;
    private int _volume;

    private void Start() {

        //Only when in main menu
        if (SceneManager.GetActiveScene().name == ScenesEnum._0_MainMenu.ToString()){

            //Show continue button or start game button
            if (GameStateData.Instance.gameData.firstTimePlaying){
                _mainMenu.transform.GetChild(0).gameObject.SetActive(true);
                _mainMenu.transform.GetChild(1).gameObject.SetActive(false);
                _mainFirstButton = _mainMenu.transform.GetChild(0).gameObject;
            }
            else {
                _mainMenu.transform.GetChild(0).gameObject.SetActive(false);
                _mainMenu.transform.GetChild(1).gameObject.SetActive(true);
                _mainFirstButton = _mainMenu.transform.GetChild(1).gameObject;
                EventSystem.current.SetSelectedGameObject(_mainFirstButton);
            }
        }

        _volume = AudioManager.Instance.Volume;
        SetVolume();
    }

    private void Update() {

        //Remove mouse click input (only controlled by arrow keys)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            if (_mainMenu.activeSelf) EventSystem.current.SetSelectedGameObject(_mainFirstButton);
            else if (_optionsMenu.activeSelf) EventSystem.current.SetSelectedGameObject(_optionsFirstButton);
            else EventSystem.current.SetSelectedGameObject(_volumeFirstButton);
        }
    }    

    //Open options panel
    public void OpenOptions(){

        _mainMenu.SetActive(false);
        _volumeMenu.SetActive(false);
        _optionsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_optionsFirstButton);
    }

    //Close options panel
    public void CloseOptions(){

        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_optionsClosedButton);
    }

    public void OpenVolume(){

        _volumeMenu.SetActive(true);
        _optionsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_volumeFirstButton);
    }

    //In game method
    private void OnEnable() {

        Cursor.lockState = CursorLockMode.Locked;
        EventSystem.current.SetSelectedGameObject(_mainFirstButton);
    }

    //In game method
    private void OnDisable() {
        
        Cursor.lockState = CursorLockMode.None;
    }

    //Open volume panel
    public void CloseVolume(){

        _volumeMenu.SetActive(false);
        _optionsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_optionsFirstButton);
    }

    public void OnSelect(BaseEventData eventData)
    {
        TextMeshProUGUI currentText = eventData.selectedObject.GetComponentInChildren<TextMeshProUGUI>();
        currentText.color = _selectedTextColor;
        currentText.text = "► " + currentText.text;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        TextMeshProUGUI currentText = eventData.selectedObject.GetComponentInChildren<TextMeshProUGUI>();
        currentText.color = _normalTextColor;

        if (currentText.text.Length > 2) currentText.text = currentText.text.Substring(2);
        else currentText.text = currentText.text.Substring(1);
    }

    public void UpdateVolume(){

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            _volume = Mathf.Min(_volume + 1, _maxVolume);
            UpdateBars();
            SetVolume();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            _volume = Mathf.Max(_volume - 1, 0);
            UpdateBars();
            SetVolume();
        }
    }

    public void UpdateBars(){

        //We have 9 bars and 10 levels of volume so the last bar on will be the volume level + 1
        for (int i = 0; i < _volumeBars.Length; i++)
        {
            if (i < _volume) _volumeBars[i].color = _selectedTextColor;
            else _volumeBars[i].color = _normalTextColor;
        }
    }

    private void SetVolume(){

        int value;
        //Audio range going from -50dB to 0dB
        if (_volume == 0) value = -80;
        else value = -50 + (50 / _maxVolume) * _volume;
        _audioMixer.SetFloat("volume", (float)value);
    }


    public void StartNewGame(){

        GameStateData.Instance.gameData.firstTimePlaying = false;
        //Need to wait for fade out screen and audio to play
        StartCoroutine(WaitForAudio());
    }
    
    IEnumerator WaitForAudio() {
        
        yield return new WaitForSeconds(11f);
        LoadScene();
    }

    public void LoadScene(){

        SceneManager.LoadSceneAsync(GameStateData.Instance.gameData.sceneToLoad.ToString());
    }

    public void LoadMainMenu(){

        SceneManager.LoadSceneAsync(ScenesEnum._0_MainMenu.ToString());
    }

    public void ExitGame(){
        Application.Quit();
    }
}
