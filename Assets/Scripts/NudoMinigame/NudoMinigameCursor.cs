using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudoMinigameCursor : MonoBehaviour
{
	MinigameController minigameController;

	[SerializeField]
	Camera minigameCam;

	MeshRenderer minigameRenderer;

    // Start is called before the first frame update
    void Start()
    {
		minigameController = FindObjectOfType<MinigameController>();
		minigameRenderer = GameObject.Find("Minigame Window").GetComponent<MeshRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = minigameController.ConvertFromScreenToViewport(minigameCam, minigameRenderer);
	}
}
