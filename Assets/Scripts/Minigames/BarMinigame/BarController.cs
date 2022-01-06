using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    public Transform cursor;

    public LayerMask layerMask;

    [SerializeField] Camera minigameCam;
    MeshRenderer minigameRenderer;
    MinigameController minigameController;

    [SerializeField] GameObject BeerBottle;
    GameObject selectedBottle;
    int filledBottle;
    void Awake()
    {
        minigameController = FindObjectOfType<MinigameController>();
        minigameRenderer = GameObject.Find("Minigame Window").GetComponent<MeshRenderer>();
    }

    void Update()
    {
        //transform.position = minigameController.ConvertFromScreenToViewport(minigameCam, minigameRenderer);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            //Vector2 finalPos = minigameController.ConvertFromScreenToViewport(minigameCam, minigameRenderer);
            Vector2 finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursor.position = finalPos;
            RaycastHit2D hit = Physics2D.Raycast(finalPos, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                selectedBottle = hit.collider.gameObject; 
                Debug.Log("Bottle hit");

            }
        }

    }
}
