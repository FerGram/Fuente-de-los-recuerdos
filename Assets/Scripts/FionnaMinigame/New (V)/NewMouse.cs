using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMouse : MonoBehaviour
{
    float zPos;

    Vector2 oriPos;

    [SerializeField]
    Camera cam;

    Vector3 minigameWindow;
    /*
    void Awake()
    {
        zPos = transform.position.z;
        oriPos = transform.position;

        minigameWindow = GameObject.Find("Minigame Window").transform.position;
    }
    */

    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1);

        // TEMPORAL
        Debug.Log("RepulsionField position");
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
        //

        //transform.localPosition = Camera.main.ScreenToWorldPoint(mousePos) - minigameWindow;
    }
}
