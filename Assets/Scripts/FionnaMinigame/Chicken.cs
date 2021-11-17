using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    bool shoudLerp = false;
    bool mouse = false;
    bool isLost = false;

    float timeStartedLerping;
    float lerpTime;

    Vector2 endPosition;
    Vector2 startPosition;

    // Distance from center to border
    static float horizontalBorder;
    static float verticalBorder;

    static float scale = 5.0f;

    public GameObject ground;
    static float maxX;
    static float maxY;

    void Start()
    {
        horizontalBorder = GetComponent<SpriteRenderer>().sprite.bounds.size.x * scale / 2;
        verticalBorder = GetComponent<SpriteRenderer>().sprite.bounds.size.y * scale / 2;

        maxX = ground.GetComponent<SpriteRenderer>().sprite.bounds.size.x * 80 / 2 - horizontalBorder;
        maxY = ground.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 80 / 2 - verticalBorder;
    }

    void Update()
    {
        if (!mouse)
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

        if (Random.Range(0.0f, 1.0f) > 0.5f)
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
        lerpTime = Random.Range(1, 3);
        shoudLerp = true;
    }

    Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageCompleted = timeSinceStarted / lerpTime;

        return Vector3.Lerp(start, end, percentageCompleted);
    }
}
