using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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

    [Space]
    [SerializeField] int _volume;

    private int _maxVolume = 9;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (_mainMenu.activeSelf) EventSystem.current.SetSelectedGameObject(_mainFirstButton);
            else EventSystem.current.SetSelectedGameObject(_optionsFirstButton);
        }
    }    

    public void OpenOptions(){

        _mainMenu.SetActive(false);
        _volumeMenu.SetActive(false);
        _optionsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_optionsFirstButton);
    }

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

    public void CloseVolume(){

        _volumeMenu.SetActive(false);
        _optionsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_optionsFirstButton);
    }

    public void OnSelect(BaseEventData eventData)
    {
        TextMeshProUGUI currentText = eventData.selectedObject.GetComponentInChildren<TextMeshProUGUI>();
        currentText.color = new Color32(137, 215, 142, 255);
        currentText.text = "► " + currentText.text;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        TextMeshProUGUI currentText = eventData.selectedObject.GetComponentInChildren<TextMeshProUGUI>();
        currentText.color = new Color32(0, 63, 4, 255);

        if (currentText.text.Length > 2) currentText.text = currentText.text.Substring(2);
        else currentText.text = currentText.text.Substring(1);
    }

    public void UpdateVolume(){

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            _volume = Mathf.Min(_volume + 1, _maxVolume);
            UpdateBars();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            _volume = Mathf.Max(_volume - 1, 0);
            UpdateBars();
        }
    }

    public void UpdateBars(){

        //We have 9 bars and 10 levels of volume so the last bar on will be the volume level + 1
        for (int i = 0; i < _volumeBars.Length; i++)
        {
            if (i < _volume) _volumeBars[i].color = new Color32(137, 215, 142, 255);
            else _volumeBars[i].color = new Color32(0, 63, 4, 255);
        }
    }

    public void ExitGame(){
        Application.Quit();
    }
}
