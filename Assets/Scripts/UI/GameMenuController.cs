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
    [Range(0,8)] [SerializeField] int _volume;

    [Space]
    [SerializeField] Color32 _normalTextColor;
    [SerializeField] Color32 _selectedTextColor;

    [Space]
    [SerializeField] ScenesEnum _sceneToLoad;

    private int _maxVolume = 8;

    private void Start() {

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

        EventSystem.current.SetSelectedGameObject(_mainFirstButton);
        Cursor.lockState = CursorLockMode.Locked;
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

        //Need to wait for fade out screen and audio to play
        StartCoroutine(WaitForAudio());
    }
    
    IEnumerator WaitForAudio() {
        
        yield return new WaitForSeconds(11f);
        LoadScene();
    }

    public void LoadScene(){

        SceneManager.LoadSceneAsync(_sceneToLoad.ToString());
    }

    public void ExitGame(){
        Application.Quit();
    }
}
