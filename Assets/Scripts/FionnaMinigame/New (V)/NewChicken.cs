using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewChicken : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 dir;
    float speed;

    bool timerReady;
    float timerSeconds;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;

        timerSeconds = 0.0f;
        timerReady = false;
    }

    void Update()
    {
        if (timerSeconds <= 0.0f)
        {
            ChangeDirectionAndSpeed();

            timerSeconds = Seconds();
            timerReady = true;
        }
        else if (timerReady)
        {
            timerSeconds -= 1 * Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //When colliding with a wall
        if (col.gameObject.CompareTag("Wall"))
        {
            // Restart timer
            timerSeconds = Seconds();

            //Get normal vector of hitpoint.
            Vector2 normal = col.GetContact(0).normal;

            float x = Random.Range(-0.1f, 0.1f);
            float y = Random.Range(-0.1f, 0.1f);

            if (normal.x + x >= 1 && x >= 0 || normal.x <= -1 && x < 0) x = 0;
            if (normal.y + y >= 1 && y >= 0 || normal.y <= -1 && y < 0) y = 0;

            //Calculate reflection of direction with normal
            dir = Vector2.Reflect(dir, normal + new Vector2 (x, y));

            rb.velocity = dir * speed;
        }

        else if (col.gameObject.CompareTag("Chicken"))
        {
            // Restart timer
            timerSeconds = Seconds();

            //Get normal vector of hitpoint.
            Vector2 normal = col.GetContact(0).normal;

            float x = Random.Range(-0.1f, 0.1f);
            float y = Random.Range(-0.1f, 0.1f);

            if (normal.x + x >= 1 && x >= 0 || normal.x <= -1 && x < 0) x = 0;
            if (normal.y + y >= 1 && y >= 0 || normal.y <= -1 && y < 0) y = 0;

            //Calculate reflection of direction with normal
            dir = Vector2.Reflect(dir, normal + new Vector2(x, y));

            rb.velocity = dir * speed;
        }
    }

    float Seconds()
    {
        return Random.Range(1.5f, 3.0f);
    }

    void ChangeDirectionAndSpeed()
    {
        dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        speed = Random.Range(4.0f, 5.5f);
        rb.velocity = dir * speed;
    }
}
