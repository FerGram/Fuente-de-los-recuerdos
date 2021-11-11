using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] ScenesEnum _sceneToChange;
    [SerializeField] GameEvent _sceneChangeGameEvent;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player") {
            
            if (_sceneChangeGameEvent != null) _sceneChangeGameEvent.Raise();
            Invoke("LoadScene", 0.2f);
        }
    }

    private void LoadScene(){
        SceneManager.LoadScene(_sceneToChange.ToString());
    }
}
