using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] ScenesEnum _sceneToLoad;
    [SerializeField] Vector2 _nextSpawnPos;

    private bool _playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player") _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if (other.tag == "Player") _playerInRange = false;
    }

    public void OnInteract(){

        if (_playerInRange) {

            //Set the new loaded scene as the scene to load if player exits the game
            GameStateData.Instance.gameData.sceneToLoad = _sceneToLoad;

            SceneLoader.Instance.LoadScene(_sceneToLoad, _nextSpawnPos);
        }
    }
}
