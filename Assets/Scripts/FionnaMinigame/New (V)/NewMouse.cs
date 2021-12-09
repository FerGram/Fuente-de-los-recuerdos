using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMouse : MonoBehaviour
{
    float zPos;

    Vector2 oriPos;

    [SerializeField]
    Camera minigameCam;

    MeshRenderer minigameWindow;

	MinigameController minigameController;
    
    void Awake()
    {
		minigameController = FindObjectOfType<MinigameController>();

        minigameWindow = GameObject.Find("Minigame Window").GetComponent<MeshRenderer>();
    }
    

    void Update()
    {
		Vector2 finalPos = minigameController.ConvertFromScreenToViewport(minigameCam, minigameWindow);
        transform.position = finalPos;
    }
}
