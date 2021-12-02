using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    bool uprooted;
    bool edible;
    bool onGround;
    public bool OnScreen;

    [SerializeField] ParticleSystem dirtEffect;
    [SerializeField] GameObject ground;
    [SerializeField] GameObject goodBasket;
    [SerializeField] GameObject badBasket;

    static float basketsTop;
    static float plantTop;

    static float plantWidth;
    Vector2 difference;

    float startingZ;
    float startingScaleY;

    bool firstTime;

    Rigidbody2D rg;
    [SerializeField] GameObject controller;

    void Start()
    {
        firstTime = true;

        if (Random.Range(0.0f, 1.0f) > 0.5f) edible = true;
        else edible = false;

        if (edible)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        controller = GameObject.FindGameObjectWithTag("GameController");

        startingZ = transform.position.z;

        plantWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x / 2;
        uprooted = false;
        rg = gameObject.transform.GetComponent<Rigidbody2D>();

        basketsTop = goodBasket.GetComponent<SpriteRenderer>().sprite.bounds.size.y * goodBasket.transform.localScale.y / 2;
        plantTop = GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y / 2;

        startingScaleY = transform.localScale.y;
        transform.localScale = new Vector3(transform.localScale.x, startingScaleY * 0.8f, transform.localScale.z);
    }

    void Update()
    {
        if (transform.position.y - plantTop > goodBasket.transform.position.y + basketsTop)
        {
            transform.position = new Vector3 (transform.position.x, transform.position.y, 2.0f);
        }
        else if (transform.position.y + plantTop < goodBasket.transform.position.y + basketsTop && uprooted)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startingZ);
        }

        if (transform.position.y < ground.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, startingZ);
        }
        else if (transform.position.y >= Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, startingZ);
        }

        if (OnScreen)
        {
            if (transform.position.x >= Camera.main.orthographicSize * Screen.width / Screen.height)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, startingZ);
            }
            else if (transform.position.x <= -Camera.main.orthographicSize * Screen.width / Screen.height)
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, startingZ);
            }
        }
    }

    private void OnMouseDown()
    {
        difference = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, 
                                  Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);

        if (uprooted)
        {
            rg.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnMouseUp()
    {
        if (uprooted)
        {
            rg.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnMouseDrag()
    {
        if (!uprooted && !controller.GetComponent<NudoController>().lvlIsMoving)
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - difference.y >= transform.position.y &&
                Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) <= plantWidth)
                transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - difference.y, startingZ);
        }
        else if (uprooted)
        {
            //The first time it is uprooted
            if (firstTime)
            {
                firstTime = false;
                transform.localScale = new Vector3(transform.localScale.x, startingScaleY * 0.8f, transform.localScale.z);
            }
            // Can't move plant downwards when it's on the ground
            if (onGround && Camera.main.ScreenToWorldPoint(Input.mousePosition).y - difference.y >= transform.position.y)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - difference.x,
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y - difference.y, startingZ);
            }
            else if (!onGround)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - difference.x,
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y - difference.y, startingZ);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == ground)
        {
            if (!uprooted)
            {
                Instantiate(dirtEffect, new Vector3(transform.position.x, transform.position.y - plantWidth, -2), Quaternion.identity);
                uprooted = true;
            }
            onGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.z == 2.0f)
        {
            if (collision.gameObject == goodBasket)
            {
                controller.GetComponent<NudoController>().numberOfPlants--;
                if (!edible)
                {
                    controller.GetComponent<NudoController>().mistakes++;
                }
                Destroy(this.gameObject);
            } 
            else if (collision.gameObject == badBasket)
            {
                controller.GetComponent<NudoController>().numberOfPlants--;
                if (edible)
                {
                    controller.GetComponent<NudoController>().mistakes++;
                }
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == ground)
            onGround = true;

        if (transform.position.z == 2.0f)
        {
            if (collision.gameObject.CompareTag("GoodBasketHelper"))
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(goodBasket.transform.position.x, transform.position.y, 2.0f), 0.1f);
            }
            else if (collision.gameObject.CompareTag("BadBasketHelper"))
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(badBasket.transform.position.x, transform.position.y, 2.0f), 0.1f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ground)
            onGround = true;
    }
}
