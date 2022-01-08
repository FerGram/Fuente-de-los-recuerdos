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

    [SerializeField] BeerBottle beerBottle;
    public GameObject selectedBottle;
    public int filledBottles;

	bool isMinigameFinished;

	void Start()
    {
        filledBottles = 0;

        beerBottle = beerBottle.GetComponent<BeerBottle>();

        minigameController = FindObjectOfType<MinigameController>();
        minigameRenderer = GameObject.Find("Minigame Window").GetComponent<MeshRenderer>();
    }

    void Update()
    {
        transform.position = minigameController.ConvertFromScreenToViewport(minigameCam, minigameRenderer);
        //transform.position = new Vector3 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -5);

        if (filledBottles >= 3 && !isMinigameFinished)
        {
			isMinigameFinished = true;
			StartCoroutine(FinishMinigame());
            Debug.Log("Minigame finished");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 finalPos = minigameController.ConvertFromScreenToViewport(minigameCam, minigameRenderer);

			//Vector2 finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            cursor.position = finalPos;
            RaycastHit2D hit = Physics2D.Raycast(finalPos, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null && beerBottle.fillBottle == false)
            {
                selectedBottle = hit.collider.gameObject;
                if (!selectedBottle.transform.GetChild(1).gameObject.GetComponent<Renderer>().isVisible)
                {
                    beerBottle.fillBottle = true;
                }
                
                Debug.Log("Bottle hit");
            }
        }
    }

	IEnumerator FinishMinigame()
	{
		yield return new WaitForSeconds(2f);
		MinigameEvents.current.UnloadMinigame(0);
	}
}
