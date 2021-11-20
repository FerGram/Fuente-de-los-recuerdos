using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    bool shoudLerp = false;
    public bool mouse = false;

    float timeStartedLerping;
    float lerpTime;

    Vector2 endPosition;
    Vector2 startPosition;

    // Distance from center to border
    static float horizontalBorder;
    static float verticalBorder;

    static float scale;

    public GameObject ground;
    static float maxX;
    static float maxY;

    RaycastHit2D hit;

    bool separateChickens;
    bool towardsHouse;
    bool doIt;

    static GameObject chickenHouse;
    void Start()
    {
        doIt = true;

        chickenHouse = GameObject.FindGameObjectWithTag("ChickenHouse");
        towardsHouse = false;

        mouse = false;
        scale = GetComponent<Transform>().localScale.x;

        horizontalBorder = GetComponent<SpriteRenderer>().sprite.bounds.size.x * scale / 2;
        verticalBorder = GetComponent<SpriteRenderer>().sprite.bounds.size.y * scale / 2;

        float groundScale = ground.GetComponent<Transform>().localScale.x;
        maxX = ground.GetComponent<SpriteRenderer>().sprite.bounds.size.x * groundScale / 2 - horizontalBorder;
        maxY = ground.GetComponent<SpriteRenderer>().sprite.bounds.size.y * groundScale / 2 - verticalBorder;
    }

    void Update()
    {
        Debug.Log("doIt: " + doIt);
        if (!mouse)
        {
            if (!RayCollision() || separateChickens)
            {
                separateChickens = false;
                if (shoudLerp)
                {
                    transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
                    if (ChickenIsOut())
                    {
                        Destroy(this.gameObject);
                    }
                    else if (MovementFinished())
                    {
                        shoudLerp = false;
                        doIt = true;
                    }
                }
                else if (!towardsHouse)
                {
                    MoveChicken(SelectNextPosition());
                }
            }
            else
            {
                towardsHouse = false;
                separateChickens = true;
                MoveChicken(SelectNextPosition());
            }
        }
        else
        {
            MouseOnChicken();
        }
    }

    void MouseOnChicken()
    {
        if (doIt)
        {
            doIt = false;
            MoveChicken(new Vector3 (chickenHouse.transform.position.x, chickenHouse.transform.position.y, -1));
        }
        transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
        towardsHouse = true;
    }

    bool MovementFinished()
    {
        return transform.position == new Vector3(endPosition.x, endPosition.y, 0);
    }

    bool ChickenIsOut()
    {
        return (Mathf.Abs(transform.position.x) > maxX + 2 * horizontalBorder || Mathf.Abs(transform.position.y) > maxY + 2 * verticalBorder);
    }

    Vector2 SelectNextPosition()
    {
        float x, y;

        x = Random.Range(-maxX, maxX);
        y = Random.Range(-maxY, maxY);

        if (Random.Range(0.0f, 1.0f) > 1.90f)
        {
            //Chicken moves outside of the screen
            if (transform.position.x < 0) x -= maxX - 2 * horizontalBorder;
            else x += maxX + 2 * horizontalBorder;

            if (transform.position.y < 0) y -= maxY - 2 * verticalBorder;
            else y += maxY - 2 * verticalBorder;
        }
        return new Vector2(x, y);
    }

    void MoveChicken(Vector2 vector)
    {
        startPosition = transform.position;
        endPosition = vector;

        timeStartedLerping = Time.time;
        lerpTime = Random.Range(2, 3);
        shoudLerp = true;
    }
    
    bool RayCollision()
    {

        float length_ray = 1f;

        Vector2 origin = transform.position + new Vector3 (0, verticalBorder, 0);
        Vector2 direction = Vector2.up;

        hit = Physics2D.Raycast(origin, direction, length_ray);

        // Upwards ray
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, direction, Color.green);
                return true;
            }
        }

        // Downwards ray
        hit = Physics2D.Raycast(origin, -direction, length_ray);
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, -direction, Color.green);
                return true;
            }
        }

        // Forwards ray
        origin = transform.position + new Vector3(horizontalBorder, 0, 0);
        direction = Vector3.right;

        hit = Physics2D.Raycast(origin, direction, length_ray);
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, direction, Color.green);
                return true;
            }
        }

        // Backwards ray
        hit = Physics2D.Raycast(origin, -direction, length_ray);
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, -direction, Color.green);
                return true;
            }
        }

        // Diagonal rays
        origin = transform.position + new Vector3(horizontalBorder, verticalBorder, 0);
        direction = Vector3.right + Vector3.up;

        hit = Physics2D.Raycast(origin, direction, length_ray);
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, direction, Color.green);
                return true;
            }
        }

        origin = transform.position + new Vector3(-horizontalBorder, -verticalBorder, 0);

        hit = Physics2D.Raycast(origin, -direction, length_ray);
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, -direction, Color.green);
                return true;
            }
        }

        origin = transform.position + new Vector3(-horizontalBorder, verticalBorder, 0);
        direction = Vector3.left + Vector3.up;

        hit = Physics2D.Raycast(origin, direction, length_ray);
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, direction, Color.green);
                return true;
            }
        }

        origin = transform.position + new Vector3(horizontalBorder, -verticalBorder, 0);

        hit = Physics2D.Raycast(origin, -direction, length_ray);
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, -direction, Color.green);
                return true;
            }
        }

        return false;
    }

    Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageCompleted = timeSinceStarted / lerpTime;

        return Vector3.Lerp(start, end, percentageCompleted);
    }
}
