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

    float collisionMaxX;
    float collisionMaxY;

    RaycastHit2D hit;

    bool mouseMovesChicken;

    bool box;

    bool upwardRayBox;
    bool downwardRayBox;
    bool forwardRayBox;
    bool backwardRayBox;

    bool diagonalRightRayBox;
    bool diagonalLeftRayBox;

    public GameObject chickenHouse;

    static GameObject controller;
    static ChickenGame scriptGame;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        scriptGame = controller.GetComponent<ChickenGame>();

        chickenHouse = GameObject.FindGameObjectWithTag("ChickenHouse");

        mouseMovesChicken = true;

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
        if (!scriptGame.stop)
        {
            if (!mouseMovesChicken)
                this.GetComponent<SpriteRenderer>().color = Color.gray;
            else
                this.GetComponent<SpriteRenderer>().color = Color.white;
            
            Debug.Log("mouse " + mouse);
            
            if (!mouse)
            {
                if (!RayCollision())
                {
                    if (shoudLerp)
                    {
                        transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
                        if (ChickenIsOut())
                        {
                            Destroy(this.gameObject);

                            scriptGame.lostChickens++;
                        }
                        else if (MovementFinished())
                        {
                            shoudLerp = false;
                            mouseMovesChicken = true;
                        }
                    }
                    else
                    {
                        MoveChicken(NormalSelectNextPosition(2.0f));
                    }
                }
                else
                {
                    if (box)
                    {
                        box = false;
                        MoveChicken(BoxSelectNextPosition());
                    }
                    else
                    {
                        MoveChicken(NormalSelectNextPosition(2.0f));
                    }
                }
            }
            else
            {
                MouseOnChicken();
            }
        }
    }

    void MouseOnChicken()
    {
        if (mouseMovesChicken)
        {
            mouseMovesChicken = false;
            MoveChicken(new Vector3 (chickenHouse.transform.position.x, chickenHouse.transform.position.y, 0));
        }
        transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
    }

    bool MovementFinished()
    {
        return transform.position == new Vector3(endPosition.x, endPosition.y, 0);
    }

    bool ChickenIsOut()
    {
        return (Mathf.Abs(transform.position.x) > maxX + 2 * horizontalBorder || Mathf.Abs(transform.position.y) > maxY + 2 * verticalBorder);
    }

    Vector2 BoxSelectNextPosition()
    {
        float x, y;

        if (diagonalRightRayBox)
        {
            diagonalRightRayBox = false;
            x = Random.Range(collisionMaxX, -maxX);
            y = Random.Range(collisionMaxY, -maxY);
        }
        else if (diagonalLeftRayBox)
        {
            diagonalLeftRayBox = false;
            x = Random.Range(collisionMaxX, maxX);
            y = Random.Range(collisionMaxY, maxY);
        }
        else
        {
            if (forwardRayBox)
            {
                forwardRayBox = false;
                x = Random.Range(-maxX, collisionMaxX);

            }
            else if (backwardRayBox)
            {
                backwardRayBox = false;
                x = Random.Range(collisionMaxX, maxX);

            }
            else
                x = Random.Range(-collisionMaxX, collisionMaxX);


            if (upwardRayBox)
            {
                upwardRayBox = false;
                y = Random.Range(-maxY, collisionMaxY);
            }
            else if (downwardRayBox)
            {
                downwardRayBox = false;
                y = Random.Range(collisionMaxY, maxY);
            }
            else
                y = Random.Range(-collisionMaxY, collisionMaxY);
        }

        return new Vector2(x, y);
    }

    Vector2 NormalSelectNextPosition(float probOut)
    {
        float x, y;
        
        x = Random.Range(-maxX, maxX);
        y = Random.Range(-maxY, maxY);

        if (Random.Range(0.0f, 1.0f) > probOut)
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

        float length_ray = 0.3f;
        float diagonal_length_ray = 0.15f;

        Vector2 origin = transform.position + new Vector3 (0, verticalBorder, 0);
        Vector2 direction = Vector2.up;

        hit = Physics2D.Raycast(origin, direction, length_ray);

        // Upwards ray
        if (hit)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Chicken"))
                {
                    Debug.DrawRay(origin, direction, Color.green);
                    return true;
                }
                else if(hit.collider.CompareTag("ChickenBox"))
                {
                    box = true;
                    upwardRayBox = true;
                    collisionMaxX = maxX;
                    collisionMaxY = hit.collider.transform.position.y - hit.collider.bounds.extents.y * 4;
                    return true;
                }
            }
        }

        // Downwards ray
        origin = transform.position - new Vector3(0, verticalBorder, 0);
        hit = Physics2D.Raycast(origin, -direction, length_ray);
        if (hit)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Chicken"))
                {
                    Debug.DrawRay(origin, direction, Color.green);
                    return true;
                }
                else if (hit.collider.CompareTag("ChickenBox"))
                {
                    box = true;
                    downwardRayBox = true;
                    collisionMaxX = maxX;
                    collisionMaxY = hit.collider.transform.position.y + hit.collider.bounds.extents.y * 4;
                    return true;
                }
            }
        }

        // Forwards ray
        origin = transform.position + new Vector3(horizontalBorder, 0, 0);
        direction = Vector3.right;
        hit = Physics2D.Raycast(origin, direction, length_ray);
        if (hit)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Chicken"))
                {
                    Debug.DrawRay(origin, direction, Color.green);
                    return true;
                }
                else if (hit.collider.CompareTag("ChickenBox"))
                {
                    box = true;
                    forwardRayBox = true;
                    collisionMaxX = hit.collider.transform.position.x - hit.collider.bounds.extents.x * 2;
                    collisionMaxY = maxY;
                    return true;
                }
            }
        }

        // Backwards ray
        origin = transform.position - new Vector3(horizontalBorder, 0, 0);
        hit = Physics2D.Raycast(origin, -direction, length_ray);
        if (hit)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Chicken"))
                {
                    Debug.DrawRay(origin, direction, Color.green);
                    return true;
                }
                else if (hit.collider.CompareTag("ChickenBox"))
                {
                    box = true;
                    backwardRayBox = true;
                    collisionMaxX = hit.collider.transform.position.x + hit.collider.bounds.extents.x * 2;
                    collisionMaxY = maxY;
                    return true;
                }
            }
        }

        // Diagonal rays
        origin = transform.position + new Vector3(horizontalBorder, verticalBorder, 0);
        direction = Vector3.right + Vector3.up;

        hit = Physics2D.Raycast(origin, direction, diagonal_length_ray);
        if (hit)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Chicken"))
                {
                    Debug.DrawRay(origin, direction, Color.green);
                    return true;
                }
                else if (hit.collider.CompareTag("ChickenBox"))
                {
                    box = true;
                    diagonalRightRayBox = true;
                    collisionMaxX = hit.collider.transform.position.x - hit.collider.bounds.extents.x * 2;
                    collisionMaxY = hit.collider.transform.position.y - hit.collider.bounds.extents.y * 4;
                    return true;
                }
            }
        }

        origin = transform.position + new Vector3(-horizontalBorder, -verticalBorder, 0);

        hit = Physics2D.Raycast(origin, -direction, diagonal_length_ray);
        if (hit)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Chicken"))
                {
                    Debug.DrawRay(origin, direction, Color.green);
                    return true;
                }
                else if (hit.collider.CompareTag("ChickenBox"))
                {
                    box = true;
                    diagonalLeftRayBox = true;
                    collisionMaxX = hit.collider.transform.position.x + hit.collider.bounds.extents.x * 2;
                    collisionMaxY = hit.collider.transform.position.y + hit.collider.bounds.extents.y * 4;
                    return true;
                }
            }
        }

        origin = transform.position + new Vector3(-horizontalBorder, verticalBorder, 0);
        direction = Vector3.left + Vector3.up;

        hit = Physics2D.Raycast(origin, direction, diagonal_length_ray);
        if (hit)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Chicken"))
                {
                    Debug.DrawRay(origin, direction, Color.green);
                    return true;
                }
                else if (hit.collider.CompareTag("ChickenBox"))
                {
                    box = true;
                    diagonalLeftRayBox = true;
                    collisionMaxX = hit.collider.transform.position.x + hit.collider.bounds.extents.x * 2;
                    collisionMaxY = hit.collider.transform.position.y - hit.collider.bounds.extents.y * 4;
                    return true;
                }
            }
        }

        origin = transform.position + new Vector3(horizontalBorder, -verticalBorder, 0);

        hit = Physics2D.Raycast(origin, -direction, diagonal_length_ray);
        if (hit)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Chicken"))
                {
                    Debug.DrawRay(origin, direction, Color.green);
                    return true;
                }
                else if (hit.collider.CompareTag("ChickenBox"))
                {
                    box = true;
                    diagonalRightRayBox = true;
                    collisionMaxX = hit.collider.transform.position.x - hit.collider.bounds.extents.x * 2;
                    collisionMaxY = hit.collider.transform.position.y + hit.collider.bounds.extents.y * 4;
                    return true;
                }
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
