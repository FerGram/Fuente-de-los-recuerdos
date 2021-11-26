using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMinigameScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
		SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
