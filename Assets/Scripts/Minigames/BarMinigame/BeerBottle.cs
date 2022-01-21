using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : MonoBehaviour
{
    BarController controller;

    [SerializeField] GameObject beerLiquid;

    GameObject selectedBottle;
    Vector3 fillingPos;

    Vector3 startingPos;

    public bool fillBottle;
    bool movingForward;
    bool movingBackward;
    bool preparePositions;

    Vector3 beerExitPos;
    void Start()
    {
        startingPos = new Vector3(-0.36f, -3.28f, 1.5f);

        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<BarController>();
        fillBottle = false;
        movingForward = false;
        movingBackward = false;
        preparePositions = true; 
    }

    void Update()
    {
        //Debug.Log(moving);
        if (fillBottle)
        {
            if (!movingForward && preparePositions)
            {
                selectedBottle = controller.selectedBottle;
                fillingPos = selectedBottle.transform.GetChild(2).position;
                preparePositions = false;
                movingForward = true;
            }
            else if (movingBackward)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 1.5f);
                transform.position = Vector3.MoveTowards(transform.position, startingPos, 0.075f);

                if (transform.position == startingPos && transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    movingBackward = false;
                    preparePositions = true;
                    fillBottle = false;
                    controller.filledBottles++;
                }
            }
            else //movingForward
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 160), 1.5f);
                transform.position = Vector3.MoveTowards(transform.position, fillingPos, 0.075f);

                if (transform.rotation == Quaternion.Euler(0, 0, 160) && transform.position == fillingPos && movingForward == true)
                {
                    Debug.Log(movingForward);
                    movingForward = false;
                    // Fill bottle
                    transform.GetChild(0).gameObject.SetActive(true);
                    if (selectedBottle.name == "1Bottle")
                    {
                        StartCoroutine(InstantiateBeer(90));
                    }
                    else
                    {
                        StartCoroutine(InstantiateBeer(65));
                    }
                }
            }
        }
    }

    IEnumerator InstantiateBeer(int n)
    {
        beerExitPos = transform.GetChild(1).position;

        for (int i = 0; i < n; i++)
        {
            Instantiate(beerLiquid, beerExitPos, Quaternion.identity);
            yield return new WaitForSeconds(0.06f);
        }

        yield return new WaitForSeconds(0.25f);
        transform.GetChild(0).gameObject.SetActive(false);
        movingBackward = true;

        yield return new WaitForSeconds(0.5f);
        selectedBottle.transform.GetChild(1).gameObject.SetActive(true);
    }
}
