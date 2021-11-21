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
                if (Mathf.Abs(scriptGame.chickens[i].transform.position.x - this.transform.position.x) <= 2 && 
                    Mathf.Abs(scriptGame.chickens[i].transform.position.y - this.transform.position.y) <= 2)
                {
                    scriptGame.chickens[i].GetComponent<Chicken>().mouse = true;
                }
                else
                {
                    scriptGame.chickens[i].GetComponent<Chicken>().mouse = false;
                }
            }
        }
    }
}
