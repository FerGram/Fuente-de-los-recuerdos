using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewChickenController : MonoBehaviour
{
    [SerializeField] GameObject chicken;
    [SerializeField] GameObject chickenHouse;
    [SerializeField] GameObject box;

    GameObject[] chickensArray;
    int n_chickens;
    public int safeChickens;

    bool changeLvL;

    int lvl;

    Text text;
    void Awake()
    {
        safeChickens = 0;

        chickensArray = GameObject.FindGameObjectsWithTag("Chicken");
        n_chickens = chickensArray.Length;

        lvl = 1;

        changeLvL = false;

        text = GameObject.Find("Text").GetComponent<Text>();
        text.enabled = false;
    }

    void Update()
    {
        if (safeChickens >= n_chickens)
        {
            changeLvL = true;
        }

        if (changeLvL && lvl != 3)
        {
            text.enabled = true;
            text.text = "Left click -- Next Level";

            if (Input.GetMouseButtonDown(0))
            {
                changeLvL = false;
                text.enabled = false;

                safeChickens = 0;

                lvl++;

                LoadLvL();
            }
        }
        else if (changeLvL && lvl == 3)
        {
            text.enabled = true;
            text.text = "COMPLETED";

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Minigame has ended");
            }
        }
    }

    void CleanLvL()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("ChickenBox");

        foreach (GameObject box in boxes)
        {
            Destroy(box);
        }

        Destroy(GameObject.FindGameObjectWithTag("ChickenHouse"));
    }

    void LoadLvL()
    {
        CleanLvL();
        if (lvl == 2)
        {
            Level2();
        }
        else if (lvl == 3)
        {
            Level3();
        }
        chickensArray = GameObject.FindGameObjectsWithTag("Chicken");
        n_chickens = chickensArray.Length;
    }

    void Level2()
    {
        Instantiate(box, new Vector3(-1.5f, 3.0f, 0), Quaternion.identity);
        Instantiate(box, new Vector3(-4.5f, -1.3f, 0), Quaternion.identity);
        Instantiate(box, new Vector3(2.5f, -2.3f, 0), Quaternion.identity);
        Instantiate(box, new Vector3(3.75f, 2.0f, 0), Quaternion.Euler(0, 0, 45.0f));

        Instantiate(chickenHouse, new Vector3(-8.0f, -4.0f, -1), Quaternion.identity);

        Instantiate(chicken, new Vector3(-6.7f, 2.5f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(-1.5f, -2.3f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(0.5f, 1.65f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(6.4f, 3.4f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(6.85f, -1.4f, -2), Quaternion.identity);
    }

    void Level3()
    {
        Instantiate(box, new Vector3(-4.0f, 2.0f, 0), Quaternion.Euler(0, 0, 45.0f));
        Instantiate(box, new Vector3(4.0f, 2.0f, 0), Quaternion.Euler(0, 0, -45.0f));
        Instantiate(box, new Vector3(4.0f, -2.0f, 0), Quaternion.Euler(0, 0, 45.0f));
        Instantiate(box, new Vector3(-4.0f, -2.0f, 0), Quaternion.Euler(0, 0, -45.0f));
        Instantiate(box, new Vector3(0.0f, -3.0f, 0), Quaternion.identity);
        Instantiate(box, new Vector3(0.0f, 3.0f, 0), Quaternion.identity);
        Instantiate(box, new Vector3(-7.0f, 0.0f, 0), Quaternion.identity);
        Instantiate(box, new Vector3(7.0f, 0.0f, 0), Quaternion.identity);

        Instantiate(chickenHouse, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        Instantiate(chicken, new Vector3(-7.6f, 2.7f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(7.1f, 3.5f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(7.3f, -3.65f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(-7.4f, -4.1f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(-0.0f, -4.4f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(0.1f, 4.25f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(-4.45f, -0.1f, -2), Quaternion.identity);
        Instantiate(chicken, new Vector3(4.8f, -0.1f, -2), Quaternion.identity);
    }
}
