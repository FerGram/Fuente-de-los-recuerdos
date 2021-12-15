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

    bool mouseOnChicken;
    bool beingRescued;

    Vector3 nextPos;

    static NewChickenController chickenController;

    SpriteRenderer sR;

    void Awake()
    {
        chickenController = GameObject.FindGameObjectWithTag("GameController").GetComponent<NewChickenController>();

        rb = GetComponent<Rigidbody2D>();

        timerSeconds = 0.0f;
        timerReady = false;
        mouseOnChicken = false;
        beingRescued = false;

        sR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        sR.flipX = Xflip();
        if (!mouseOnChicken && !beingRescued)
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
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!beingRescued)
        {
            //When colliding with a wall
            if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("ChickenBox"))
            {
                // Restart timer
                timerSeconds = Seconds();

                mouseOnChicken = false;

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

                mouseOnChicken = false;

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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ChickenHouse"))
        {
            beingRescued = true;
            rb.velocity = Vector2.zero;
            nextPos = new Vector3 (col.gameObject.transform.position.x, col.gameObject.transform.position.y - 0.5f, -2);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ChickenHouse"))
        {

            transform.position = Vector3.MoveTowards(transform.position, nextPos, 0.1f);
            transform.localScale *= 0.90f;
            if (transform.position == nextPos)
            {
                Destroy(this.gameObject);
                chickenController.safeChickens++;
            }
        }
        //When entering the repulsion zone.
        else if (col.gameObject.CompareTag("RepulsionField"))
        {
            mouseOnChicken = true;

            //Calculate vector going away from center of cursor toward chick
            dir = (transform.position - col.bounds.center) * 1000; //multiply it by 1000 to make it bigger than 1.

            //Normalize it
            dir.Normalize();

            rb.velocity = dir * speed;
        }        
    }

    bool Xflip()
    {
        return dir.x < 0 && dir.x >= -1;
    }
    
    float Seconds()
    {
        return Random.Range(1.5f, 3.0f);
    }

    void ChangeDirectionAndSpeed()
    {
        dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        speed = Random.Range(2.5f, 2.5f);
        rb.velocity = dir * speed;
    }
}