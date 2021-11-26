using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{
	Rigidbody2D rb;

	[SerializeField]
	Vector2 dir; //dir of the chick

	[SerializeField]
	float speed;

    void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = dir * speed;
    }

	private void OnCollisionEnter2D(Collision2D col)
	{
		//When colliding with a wall
		if (col.gameObject.tag == "Wall")
		{
			Debug.Log("Colliding with wall");

			//Get normal vector of hitpoint.
			Vector2 normal = col.contacts[0].normal;

			//Calculate reflection of direction with normal
			dir = Vector2.Reflect(dir, normal);

			rb.velocity = dir * speed;
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		//When entering the repulsion zone.
		if (col.gameObject.tag == "RepulsionField")
		{
			//Calculate vector going away from center of cursor toward chick
			dir = (transform.position - col.bounds.center) * 1000; //multiply it by 1000 to make it bigger than 1.

			//Normalize it
			dir.Normalize();

			rb.velocity = dir * speed;
		}
	}
}
