using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    Vector3 vector;

    // igual no hace falta
    public Vector3 position;
    //
    public GameObject[] chicken;

    void Start()
    {
        chicken = GameObject.FindGameObjectsWithTag("Chicken");
    }

    void Update()
    {
        vector = mainCamera.ScreenToWorldPoint(Input.mousePosition); //todo fix
        vector.z = -1;
        transform.position = vector;

        position = transform.position;

        MouseOnChicken();
    }

    void MouseOnChicken()
    {
        for (int i = 0; i < chicken.Length; i++)
        {
            if (chicken[i] != null)
            {
                if (Mathf.Abs(chicken[i].transform.position.x - this.transform.position.x) <= 2 && 
                    Mathf.Abs(chicken[i].transform.position.y - this.transform.position.y) <= 2)
                {
                    //Debug.Log("hola");
                    chicken[i].GetComponent<Chicken>().mouse = true;
                }
                else
                {
                    chicken[i].GetComponent<Chicken>().mouse = false;
                }
            }
        }
    }
}
