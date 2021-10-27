using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string _sceneToChange;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player") Invoke("LoadScene", 1f);
    }

    private void LoadScene(){
        SceneManager.LoadScene(_sceneToChange);
    }
}
