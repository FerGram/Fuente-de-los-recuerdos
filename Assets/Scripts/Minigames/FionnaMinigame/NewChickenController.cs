using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewChickenController : MonoBehaviour
{
    //[SerializeField] GameObject chicken;
    //[SerializeField] GameObject chickenHouse;
    //[SerializeField] GameObject box;
	[SerializeField] int minigameLayer;

	[SerializeField]
	GameObject[] levels;

    //[SerializeField] GameObject backGround;
    //Vector2 bGPos;

    GameObject chickensHolder;
    int n_chickens;
    public int safeChickens;

    bool changeLvL;

    int lvl = 1;

    void Awake()
    {
        //bGPos = new Vector2(backGround.transform.position.x, backGround.transform.position.y);

        safeChickens = 0;

        chickensHolder = GameObject.FindWithTag("Chicken");
        n_chickens = chickensHolder.transform.childCount;

        lvl = 1;

        changeLvL = false;

    }

    void Update()
    {
        if (safeChickens >= n_chickens)
        {
            changeLvL = true;
        }

        if (changeLvL && lvl != 3)
        {
            changeLvL = false;

            safeChickens = 0;

            lvl++;

            LoadLvL();          
        }
        else if (changeLvL && lvl == 3)
        {
			Debug.Log("Minigame has ended");
			MinigameEvents.current.UnloadMinigame(0);
        }
    }

    void CleanLvL()
    {
        //GameObject[] boxes = GameObject.FindGameObjectsWithTag("ChickenBox");

        //foreach (GameObject box in boxes)
        //{
        //    Destroy(box);
        //}

        //Destroy(GameObject.FindGameObjectWithTag("ChickenHouse"));
    }

    void LoadLvL()
    {
        if (lvl == 2)
        {
			levels[0].SetActive(false);
			levels[1].SetActive(true);
        }
        else if (lvl == 3)
        {
			levels[1].SetActive(false);
			levels[2].SetActive(true);
		}

		chickensHolder = GameObject.FindWithTag("Chicken");
		n_chickens = chickensHolder.transform.childCount;
	}

    void Level2()
    {

		//GameObject box1 = Instantiate(box, new Vector3(-1.5f + bGPos.x, 3.0f + bGPos.y, 0), Quaternion.identity);
		//box1.layer = minigameLayer;

		//GameObject box2 = Instantiate(box, new Vector3(-4.5f + bGPos.x, -1.3f + bGPos.y, 0), Quaternion.identity);
		//box2.layer = minigameLayer;
		//GameObject box3 = Instantiate(box, new Vector3(2.5f + bGPos.x, -2.3f + bGPos.y, 0), Quaternion.identity);
		//box3.layer = minigameLayer;
		//GameObject box4 = Instantiate(box, new Vector3(3.75f + bGPos.x, 2.0f + bGPos.y, 0), Quaternion.identity);
		//box4.layer = minigameLayer;

		//GameObject chickenHouse1 = Instantiate(chickenHouse, new Vector3(-6.0f + bGPos.x, 3.5f + bGPos.y, -1), Quaternion.identity);
		//chickenHouse1.layer = minigameLayer;

		//GameObject chicken1 = Instantiate(chicken, new Vector3(-6.7f + bGPos.x, -3.0f + bGPos.y, -2), Quaternion.identity);
		//chicken1.layer = minigameLayer;
		//chicken1.name = "chiken1";

		//GameObject chicken2 = Instantiate(chicken, new Vector3(-1.5f + bGPos.x, -2.3f + bGPos.y, -2), Quaternion.identity);
		//chicken2.layer = minigameLayer;
		//chicken2.name = "chiken2";

		//GameObject chicken3 = Instantiate(chicken, new Vector3(0.5f + bGPos.x, 1.65f + bGPos.y, -2), Quaternion.identity);
		//chicken3.layer = minigameLayer;
		//chicken3.name = "chiken3";

		//GameObject chicken4 = Instantiate(chicken, new Vector3(6.4f + bGPos.x, 3.0f + bGPos.y, -2), Quaternion.identity);
		//chicken4.layer = minigameLayer;
		//chicken4.name = "chiken4";

		//GameObject chicken5 = Instantiate(chicken, new Vector3(6.85f + bGPos.x, -1.4f + bGPos.y, -2), Quaternion.identity);
		//chicken5.layer = minigameLayer;
		//chicken5.name = "chiken5";

	}

	void Level3()
    {
		//GameObject box1 = Instantiate(box, new Vector3(-4.0f + bGPos.x, 2.0f + bGPos.y, 0), Quaternion.identity);
		//box1.layer = minigameLayer;
		//GameObject box2 = Instantiate(box, new Vector3(4.0f + bGPos.x, 2.0f + bGPos.y, 0), Quaternion.identity);
		//box2.layer = minigameLayer;
		//GameObject box3 = Instantiate(box, new Vector3(4.0f + bGPos.x, -2.0f + bGPos.y, 0), Quaternion.identity);
		//box3.layer = minigameLayer;
		//GameObject box4 = Instantiate(box, new Vector3(-4.0f + bGPos.x, -2.0f + bGPos.y, 0), Quaternion.identity);
		//box4.layer = minigameLayer;
		//GameObject box5 = Instantiate(box, new Vector3(0.0f + bGPos.x, -3.0f + bGPos.y, 0), Quaternion.identity);
		//box5.layer = minigameLayer;
		//GameObject box6 = Instantiate(box, new Vector3(0.0f + bGPos.x, 3.0f + bGPos.y, 0), Quaternion.identity);
		//box6.layer = minigameLayer;
		//GameObject box7 = Instantiate(box, new Vector3(-7.0f + bGPos.x, 0.0f + bGPos.y, 0), Quaternion.identity);
		//box7.layer = minigameLayer;
		//GameObject box8 = Instantiate(box, new Vector3(7.0f + bGPos.x, 0.0f + bGPos.y, 0), Quaternion.identity);
		//box8.layer = minigameLayer;

		//GameObject chickenHouse1 = Instantiate(chickenHouse, new Vector3(0.0f + bGPos.x, 0.0f + bGPos.y, 0.0f), Quaternion.identity);
		//chickenHouse1.layer = minigameLayer;

		//GameObject chicken1 = Instantiate(chicken, new Vector3(-7.6f + bGPos.x, 2.7f + bGPos.y, -2), Quaternion.identity);
		//chicken1.layer = minigameLayer;
		//chicken1.name = "chiken1";

		//GameObject chicken2 = Instantiate(chicken, new Vector3(7.1f + bGPos.x, 2.85f + bGPos.y, -2), Quaternion.identity);
		//chicken2.layer = minigameLayer;
		//chicken2.name = "chiken2";

		//GameObject chicken3 = Instantiate(chicken, new Vector3(7.3f + bGPos.x, -3.65f + bGPos.y, -2), Quaternion.identity);
		//chicken3.layer = minigameLayer;
		//chicken3.name = "chiken3";

		//GameObject chicken4 = Instantiate(chicken, new Vector3(-7.4f + bGPos.x, -4.1f + bGPos.y, -2), Quaternion.identity);
		//chicken4.layer = minigameLayer;
		//chicken4.name = "chiken4";

		//GameObject chicken5 = Instantiate(chicken, new Vector3(-0.0f + bGPos.x, -4.4f + bGPos.y, -2), Quaternion.identity);
		//chicken5.layer = minigameLayer;
		//chicken5.name = "chiken5";

		//GameObject chicken6 = Instantiate(chicken, new Vector3(0.8f + bGPos.x, 3.25f + bGPos.y, -2), Quaternion.identity);
		//chicken6.layer = minigameLayer;
		//chicken6.name = "chiken6";

		//GameObject chicken7 = Instantiate(chicken, new Vector3(-4.45f + bGPos.x, -0.1f + bGPos.y, -2), Quaternion.identity);
		//chicken7.layer = minigameLayer;
		//chicken7.name = "chiken7";

		//GameObject chicken8 = Instantiate(chicken, new Vector3(4.8f + bGPos.x, -0.1f + bGPos.y, -2), Quaternion.identity);
		//chicken8.layer = minigameLayer;
		//chicken8.name = "chiken8";


	}
}