using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudoController : MonoBehaviour
{
    public int mistakes;
    public int numberOfPlants;
    int lvl;

    [SerializeField] GameObject level;


    GameObject[] plants;

    public bool lvlIsMoving;
	public bool isDragging;

	private MinigameController minigameController;

	public Camera minigameCam;

	private MeshRenderer windowRect;

	public LayerMask layerMask;

	private Plant plant;

	void Start()
    {
        lvlIsMoving = false;
        lvl = 1;
        mistakes = 0;
        plants = GameObject.FindGameObjectsWithTag("Plant1");

		minigameController = FindObjectOfType<MinigameController>();
		windowRect = GameObject.Find("Minigame Window").GetComponent<MeshRenderer>();

		for (int i = 0; i < plants.Length; i++)
        {
            plants[i].GetComponent<Plant>().OnScreen = true;
        }

        numberOfPlants = plants.Length;
    }

    void Update()
    {
        if (numberOfPlants == 0)
        {
            if (lvl == 1)
            {
                lvl++;
                CurrentPlants("Plant2");
                StartCoroutine(MoveNextLvLToCamera(-25.0f));
            }
            else if (lvl == 2)
            {
                lvl++;
                CurrentPlants("Plant3");
                StartCoroutine(MoveNextLvLToCamera(-50.0f));
            }
            else
            {
                //MINIGAME COMPLETED
            }
        }

		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;

			Vector2 finalPos = minigameController.ConvertFromScreenToViewport(minigameCam, windowRect);


			RaycastHit2D hit = Physics2D.Raycast(finalPos, Vector2.zero, Mathf.Infinity, layerMask);
			//testT.position = finalPos;

		
			if (hit.collider != null)
			{
				plant = hit.collider.gameObject.GetComponent<Plant>();

				if (plant != null)
				{
					plant.MouseDown();
				}
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			plant.MouseUp();
			plant = null;
		}

		if (isDragging)
			plant.MouseDrag();


	}

    void CurrentPlants(string tag)
    {
        plants = GameObject.FindGameObjectsWithTag(tag);
        numberOfPlants = plants.Length;
    }

    IEnumerator MoveNextLvLToCamera(float x)
    {
        lvlIsMoving = true;

        while (lvlIsMoving)
        {

            level.transform.position = Vector3.MoveTowards(level.transform.position, new Vector2(level.transform.position.x - 100, level.transform.position.y), 0.1f);
            if (level.transform.position.x <= x)
            {
                lvlIsMoving = false;

                for (int i = 0; i < plants.Length; i++)
                {
                    plants[i].GetComponent<Plant>().OnScreen = true;
                }
            }
            yield return new WaitForSeconds(0.0f);
        }
    }
}
