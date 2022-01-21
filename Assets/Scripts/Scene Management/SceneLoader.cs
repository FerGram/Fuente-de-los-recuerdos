using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] float _timeBetweenEventAndSceneLoad;
    [SerializeField] GameEvent _sceneOutGameEvent;
    [SerializeField] GameEvent _sceneInGameEvent;

    private Vector2 _nextSpawnPos;
    
    private void OnEnable() { 
        SceneManager.activeSceneChanged += OnSceneLoaded; 
        transform.name = "Scene Loader";
    }
    
    private void OnDisable() { 
        SceneManager.activeSceneChanged -= OnSceneLoaded; 
    }

    public void LoadScene(ScenesEnum sceneToLoad){

        //Raises event to make things like transitions or audio playing
        if (_sceneOutGameEvent != null) _sceneOutGameEvent.Raise();
        //After some time, Invoke load new scene
        StartCoroutine(LoadSceneRoutine(sceneToLoad));
    }

    public void LoadScene(ScenesEnum sceneToLoad, LoadSceneMode mode){

        if (_sceneOutGameEvent != null) _sceneOutGameEvent.Raise();
        StartCoroutine(LoadSceneRoutine(sceneToLoad, mode));
    }

    public void LoadScene(ScenesEnum sceneToLoad, Vector2 nextSpawnPos){

        _nextSpawnPos = nextSpawnPos;
        if (_sceneOutGameEvent != null) _sceneOutGameEvent.Raise();
        StartCoroutine(LoadSceneRoutine(sceneToLoad));
    }


    IEnumerator LoadSceneRoutine(ScenesEnum sceneToLoad){

        yield return new WaitForSeconds(_timeBetweenEventAndSceneLoad);
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
    
    IEnumerator LoadSceneRoutine(ScenesEnum sceneToLoad, LoadSceneMode mode){

        yield return new WaitForSeconds(_timeBetweenEventAndSceneLoad);
        SceneManager.LoadScene(sceneToLoad.ToString(), mode);
    }

    //To fix spawn position (not the best place to do this but it will do)
    private void OnSceneLoaded(Scene oldScene, Scene newScene){

        if (oldScene.name == ScenesEnum.ChessMinigame.ToString() ||
            oldScene.name == ScenesEnum.FionnaMinigame.ToString() ||
            oldScene.name == ScenesEnum.NudoMinigame.ToString() ||
			oldScene.name == ScenesEnum.BarMinigame.ToString() ||

			newScene.name == ScenesEnum.ChessMinigame.ToString() ||
            newScene.name == ScenesEnum.FionnaMinigame.ToString() ||
            newScene.name == ScenesEnum.NudoMinigame.ToString() ||
			newScene.name == ScenesEnum.BarMinigame.ToString()) return;


        if (_sceneInGameEvent != null) _sceneInGameEvent.Raise();

        PlayerMovement[] player = FindObjectsOfType<PlayerMovement>();

        //For loop because there can be more than 1 player (ending cinematic)
        for (int i = 0; i < player.Length; i++)
        {
            //The vector2 comparison is to test if _nextSpawnPos has been assigned a value
            if (player[i] != null && _nextSpawnPos != new Vector2(0, 0))
            {
                player[i].transform.position = _nextSpawnPos + 
                                                new Vector2(i * 1.1f * _nextSpawnPos.x / Mathf.Abs(_nextSpawnPos.x), 0);
            }
        }
    }
}
