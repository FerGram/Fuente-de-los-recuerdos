using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenGame : MonoBehaviour
{
    [SerializeField] GameObject chicken;
    [SerializeField] GameObject ChickenHouse;
    [SerializeField] GameObject Box;
    [SerializeField] GameObject Ground;
    [SerializeField] Sprite Ground02;
    [SerializeField] Sprite Ground03;

    bool text;
    public bool stop;
    public bool gameOver;
    public bool win;
    int lvl;

    public int safeChickens;
    public int lostChickens;

    public GameObject[] chickens;
    void Start()
    {
        safeChickens = 0;

        stop = false;
        gameOver = false;
        win = false;
        text = true;

        lvl = 1;

        GameObject.Find("Text").GetComponent<Text>().enabled = false;

        Level1();
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
    }

    void Update()
    {
        MinigameFlow();
    }

    void MinigameFlow()
    {
        if (gameOver && text && lvl != 3)
        {
            GameObject.Find("Text").GetComponent<Text>().enabled = true;
            GameObject.Find("Text").GetComponent<Text>().text = "Left Click to try again";
            text = false;
            stop = true;
        }
        else if (win && text && lvl != 3)
        {
            Debug.Log("You won");
            GameObject.Find("Text").GetComponent<Text>().enabled = true;
            GameObject.Find("Text").GetComponent<Text>().text = "Left Click to advance to the next level";
            text = false;
            stop = true;

        }
        else if (win && text && lvl == 3)
        {
            GameObject.Find("Text").GetComponent<Text>().enabled = true;
            GameObject.Find("Text").GetComponent<Text>().text = "COMPLETED";
            text = false;
            stop = true;
        }

        if (gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            text = true;
            GameObject.Find("Text").GetComponent<Text>().enabled = false;

            PrepareLvL();
            LoadLvL();

            stop = false;
        }
        else if (win && Input.GetMouseButtonDown(0) && lvl == 3)
        {
            //Minigame completed --> next scene
        }
        else if (win && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Next level");
            win = false;
            text = true;
            GameObject.Find("Text").GetComponent<Text>().enabled = false;

            // Next lvl
            lvl++;

            PrepareLvL();
            LoadLvL();

            stop = false;
        }
    }

    void PrepareLvL()
    {
        safeChickens = 0;

        GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");
        GameObject chickenHouse = GameObject.FindGameObjectWithTag("ChickenHouse");
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("ChickenBox");
        GameObject ground = GameObject.Find("Ground");

        for (int i = 0; i < chickens.Length; i++)
        {
            if (chickens[i] != null) Destroy(chickens[i]);
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i] != null) Destroy(boxes[i]);
        }

        Destroy(ground);

        Destroy(chickenHouse);
    }

    void LoadLvL()
    {
        if (lvl == 1)
        {
            Level1();
        }
        else if (lvl == 2)
        {
            Level2();
        }
        else if (lvl == 3)
        {
            Level3();
        }
        chickens = GameObject.FindGameObjectsWithTag("Chicken");
    }

    void Level1()
    {
        GameObject ground = Instantiate(Ground, new Vector3(0, 0, 1), Quaternion.identity);


        Instantiate(chicken, new Vector3(-30, -20, 0), Quaternion.identity);
        Instantiate(chicken, new Vector3(20, 15, 0), Quaternion.identity);

        Instantiate(ChickenHouse, new Vector3(0, 30, 0.5f), Quaternion.identity);
    }

    void Level2()
    {
        GameObject ground = Instantiate(Ground, new Vector3(0, 0, 1), Quaternion.identity);
        //ground.GetComponent<SpriteRenderer>().sprite = Ground02;
        //ground.GetComponent<SpriteRenderer>().color = Color.cyan;

        Instantiate(Box, new Vector3(0, 0, 0), Quaternion.identity);

        Instantiate(chicken, new Vector3(-30, -20, 0), Quaternion.identity);
        Instantiate(chicken, new Vector3(20, 30, 0), Quaternion.identity);

        Instantiate(ChickenHouse, new Vector3(0, 30, 0.5f), Quaternion.identity);
    }
    void Level3()
    {
        GameObject ground = Instantiate(Ground, new Vector3(0, 0, 1), Quaternion.identity);
        //ground.GetComponent<SpriteRenderer>().sprite = Ground03;

        Instantiate(Box, new Vector3(15, 0, 0), Quaternion.identity);
        Instantiate(Box, new Vector3(-20, 0, 0), Quaternion.Euler(0, 0, 90f));


        Instantiate(chicken, new Vector3(-30, -20, 0), Quaternion.identity);
        Instantiate(chicken, new Vector3(20, 30, 0), Quaternion.identity);

        Instantiate(ChickenHouse, new Vector3(0, 30, 0.5f), Quaternion.identity);
    }
}
