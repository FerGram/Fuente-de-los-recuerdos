using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    GameObject controller;
    ChickenGame scriptGame;

    int chickensToSave;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        scriptGame = controller.GetComponent<ChickenGame>();
        chickensToSave = scriptGame.chickens.Length / 2;

        for (int i = 0; i < scriptGame.chickens.Length; i++)
        {
            scriptGame.chickens[i].GetComponent<Chicken>().chickenHouse = this.gameObject;
        }
    }

    void Update()
    {
        if (scriptGame.lostChickens > chickensToSave) scriptGame.gameOver = true;

        ChickenOnHouse();
    }

    void ChickenOnHouse()
    {
        for (int i = 0; i < scriptGame.chickens.Length; i++)
        {
            if (scriptGame.chickens[i] != null)
            {
                if (Mathf.Abs(scriptGame.chickens[i].transform.position.x - this.transform.position.x) <= 2 &&
                    Mathf.Abs(scriptGame.chickens[i].transform.position.y - this.transform.position.y) <= 2)
                {
                    scriptGame.safeChickens++;
                    Destroy(scriptGame.chickens[i]);
                    if (scriptGame.safeChickens == chickensToSave) scriptGame.win = true;
                }
            }
        }
    }
}
