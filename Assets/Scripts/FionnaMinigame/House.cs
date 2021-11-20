using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    GameObject[] chicken;
    int countChickens;
    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.FindGameObjectsWithTag("Chicken");
    }

    // Update is called once per frame
    void Update()
    {
        ChickenOnHouse();
    }

    void ChickenOnHouse()
    {
        for (int i = 0; i < chicken.Length; i++)
        {
            if (chicken[i] != null)
            {
                if (Mathf.Abs(chicken[i].transform.position.x - this.transform.position.x) <= 2 &&
                    Mathf.Abs(chicken[i].transform.position.y - this.transform.position.y) <= 2)
                {
                    //countChickens++;
                    Destroy(chicken[i]);
                    //chicken[i].GetComponent<Chicken>().mouse = false;
                }
            }
        }
    }
}
