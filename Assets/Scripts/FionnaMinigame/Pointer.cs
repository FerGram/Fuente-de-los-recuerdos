using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    Vector3 vector;

    Vector3 position;


    GameObject controller;
    ChickenGame scriptGame;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        scriptGame = controller.GetComponent<ChickenGame>();
    }

    void Update()
    {
        vector = mainCamera.ScreenToWorldPoint(Input.mousePosition); //todo fix AAAA
        vector.z = -1;
        transform.position = vector;

        position = transform.position;

        MouseOnChicken();
    }

    void MouseOnChicken()
    {
        for (int i = 0; i < scriptGame.chickens.Length; i++)
        {
            if (scriptGame.chickens[i] != null)
            {
                Chicken chicken = scriptGame.chickens[i].GetComponent<Chicken>();
                if (this.transform.position.x <= chicken.transform.position.x + chicken.horizontalBorder && 
                    this.transform.position.x >= chicken.transform.position.x - chicken.horizontalBorder &&
                    this.transform.position.y <= chicken.transform.position.y + chicken.verticalBorder   &&
                    this.transform.position.y >= chicken.transform.position.y - chicken.verticalBorder)
                {
                    scriptGame.chickens[i].GetComponent<Chicken>().mouse = true;
                    /*
                    public bool pointerIsLeft;
                    public bool pointerIsRight;
                    public bool pointerIsAbove;
                    public bool pointerIsBelow;
                    */
                    if (scriptGame.chickens[i].transform.position.x > this.transform.position.x)
                        scriptGame.chickens[i].GetComponent<Chicken>().pointerIsLeft = true;
                    else
                        scriptGame.chickens[i].GetComponent<Chicken>().pointerIsRight = true;

                    if (scriptGame.chickens[i].transform.position.y > this.transform.position.y)
                        scriptGame.chickens[i].GetComponent<Chicken>().pointerIsBelow = true;
                    else
                        scriptGame.chickens[i].GetComponent<Chicken>().pointerIsAbove = true;
                }
                else
                {
                    scriptGame.chickens[i].GetComponent<Chicken>().mouse = false;
                }
            }
        }
    }
}
