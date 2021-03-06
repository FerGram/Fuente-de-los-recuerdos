using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
	[SerializeField]
	int _sceneIndex;

	[SerializeField]
	GameObject _minigameWindow;

	Scene _mainScene;

	RectTransform _windowRect;

	[SerializeField]
	Sprite minigameCursorSprite;

	Sprite prevImageCursor;

	GameObject cursor;

	// Start is called before the first frame update
	void Start()
    {
		cursor = GameObject.FindGameObjectWithTag("Cursor");

		_mainScene = SceneManager.GetActiveScene();

		MinigameEvents.current.onLoadMinigame += LoadMinigame;
		MinigameEvents.current.onUnloadMinigame += UnloadMinigame;
	}

    public void LoadMinigame(int minigame)
	{
		//Get scene to load
		_sceneIndex = minigame;
		Scene scene = SceneManager.GetSceneByBuildIndex(_sceneIndex);

		// Save previous image
		prevImageCursor = cursor.GetComponent<Image>().sprite;
		// Change Cursor's image
		cursor.GetComponent<Image>().sprite = minigameCursorSprite;

		//Load scene asyncronously
		//SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive, LoadSceneParameters );
		SceneManager.LoadScene(_sceneIndex, LoadSceneMode.Additive);
		SceneManager.sceneLoaded += SetActiveScene;
		_minigameWindow.SetActive(true);
	}

	public void UnloadMinigame(int minigame)
	{
		Scene sceneToDestroy = SceneManager.GetActiveScene();
		SceneManager.SetActiveScene(_mainScene);
		SceneManager.UnloadSceneAsync(sceneToDestroy);

		// Change Cursor's image
		cursor.GetComponent<Image>().sprite = prevImageCursor;

		StartCoroutine(_minigameWindow.GetComponent<MinigameWindow>().Minimize());
	}

	void SetActiveScene(Scene scene, LoadSceneMode loadMode)
	{
		SceneManager.SetActiveScene(scene);
		//Debug.Log("Scene loaded:" + SceneManager.GetActiveScene().name);
		SceneManager.sceneLoaded -= SetActiveScene;
	}

	public Vector2 ConvertFromScreenToViewport(Camera minigameCam, MeshRenderer _minigameWindow)
	{
		Vector2 mousePos = Input.mousePosition;

		mousePos = Camera.main.ScreenToWorldPoint(mousePos); //TODO reference the main cam.
		mousePos = new Vector2(mousePos.x - _minigameWindow.bounds.min.x, mousePos.y - _minigameWindow.bounds.min.y);

		mousePos.x /= _minigameWindow.transform.localScale.x;
		mousePos.y /= _minigameWindow.transform.localScale.y;

		Vector3 finalPos = minigameCam.ViewportToWorldPoint(mousePos);
		finalPos.z = 0;

		return finalPos;
	}
}
