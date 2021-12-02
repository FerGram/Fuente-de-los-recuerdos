using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionField : MonoBehaviour
{
	float zPos;

	Vector2 oriPos;

	[SerializeField]
	Camera cam;

	Vector3 minigameWindow;

    // Start is called before the first frame update
    void Awake()
    {

		zPos = transform.position.z;
		oriPos = transform.position;

		minigameWindow = GameObject.Find("Minigame Window").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		//Get pos of mouse and ajust z
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

		//Make field follow cursor.
		//Vector3 first = 
		transform.localPosition = Camera.main.ScreenToWorldPoint(mousePos) - minigameWindow;

	}
}
