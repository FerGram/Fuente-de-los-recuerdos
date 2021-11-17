using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    bool shoudLerp = false;
    bool mouse = false;

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

    [SerializeField] private Camera mainCamera;
    Vector2 mousePosition;

    public GameObject pointer;

    void Start()
    {
        Instantiate(pointer);

        mouse = false;
        scale = 5.0f;

        horizontalBorder = GetComponent<SpriteRenderer>().sprite.bounds.size.x * scale / 2;
        verticalBorder = GetComponent<SpriteRenderer>().sprite.bounds.size.y * scale / 2;

        maxX = ground.GetComponent<SpriteRenderer>().sprite.bounds.size.x * 80 / 2 - horizontalBorder;
        maxY = ground.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 80 / 2 - verticalBorder;
    }

    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        pointer.transform.position = mousePosition;

        //if (mousePosition)

        if (!mouse)
        {
            if (!RayCollision())
            {
                if (shoudLerp)
                {
                    transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
                    if (ChickenIsOut())
                    {
                        //isLost = true;
                        Destroy(this.gameObject);
                    }
                    else if (MovementFinished()) shoudLerp = false;
                }
                else
                {
                    MoveChicken();
                }
            }
            else
            {
                MoveChicken();
            }
        }
        else
        {

        }
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

        if (Random.Range(0.0f, 1.0f) > 0.90f)
        {
            //Chicken moves outside of the screen
            if (transform.position.x < 0) x -= maxX - 2 * horizontalBorder;
            else x += maxX + 2 * horizontalBorder;

            if (transform.position.y < 0) y -= maxY - 2 * verticalBorder;
            else y += maxY - 2 * verticalBorder;
        }
        return new Vector2(x, y);
    }

    void MoveChicken()
    {
        startPosition = transform.position;
        endPosition = SelectNextPosition();

        timeStartedLerping = Time.time;
        lerpTime = Random.Range(2, 3);
        shoudLerp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chicken"))
        {
            /*
            Debug.Log("TRIGGERED");
            other = collision.gameObject;
            collisionWithOtherChicken = true;
            */
        }
        else // Chicken collides with mouse
        {
     
        }
    }
    
    bool RayCollision()
    {

        float length_ray = 5;

        Vector2 origin = transform.position + new Vector3 (0, verticalBorder, 0);
        Vector2 direction = Vector2.up;

        hit = Physics2D.Raycast(origin, direction, length_ray);

        // Upwards ray
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.DrawRay(origin, direction, Color.green);
                Debug.Log("UPWARDS");
                return true;
            }
        }

        // Downwards ray
        hit = Physics2D.Raycast(origin, -direction, length_ray);
        if (hit)
        {
            if (hit.collider.CompareTag("Chicken") && hit.collider.name != this.name)
            {
                Debug.Log("DOWNWARDS");
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
