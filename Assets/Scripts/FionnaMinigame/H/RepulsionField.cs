using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionField : MonoBehaviour
{
	float zPos;

    // Start is called before the first frame update
    void Start()
    {
		zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
		//Get pos of mouse and ajust z
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPos);

		//Make field follow cursor.
		transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
