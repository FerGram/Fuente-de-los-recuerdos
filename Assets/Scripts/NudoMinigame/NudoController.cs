using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudoController : MonoBehaviour
{
    public int mistakes;
    public int numberOfPlants;
    int lvl;

    [SerializeField] GameObject lvl1;
    [SerializeField] GameObject lvl2;
    [SerializeField] GameObject lvl3;

    GameObject[] plants;

    public bool lvlIsMoving;
    Vector3 NextPosition;
    void Start()
    {
        lvlIsMoving = false;
        lvl = 1;
        mistakes = 0;
        plants = GameObject.FindGameObjectsWithTag("Plant1");

        for (int i = 0; i < plants.Length; i++)
        {
            plants[i].GetComponent<Plant>().OnScreen = true;
        }

        numberOfPlants = plants.Length;
    }

    void Update()
    {
        if (numberOfPlants == 0)
        {
            if (lvl == 1)
            {
                lvl++;
                CurrentPlants("Plant2");
                StartCoroutine(MoveNextLvLToCamera(lvl1, lvl2));
            }
            else if (lvl == 2)
            {
                lvl++;
                CurrentPlants("Plant3");
                StartCoroutine(MoveNextLvLToCamera(lvl2, lvl3));
            }
        }
    }

    void CurrentPlants(string tag)
    {
        plants = GameObject.FindGameObjectsWithTag(tag);
        numberOfPlants = plants.Length;
    }

    IEnumerator MoveNextLvLToCamera(GameObject actualLvl, GameObject nextLvl)
    {
        lvlIsMoving = true;
        if (lvl == 2)
        {
            NextPosition = new Vector3(5.48f, actualLvl.transform.position.y, actualLvl.transform.position.z);
        }
        else if (lvl == 3)
        {
            NextPosition = new Vector3(0.0f, actualLvl.transform.position.y, actualLvl.transform.position.z);
        }

        while (lvlIsMoving)
        {
            nextLvl.transform.position = Vector3.MoveTowards(nextLvl.transform.position, NextPosition, 0.1f);
            actualLvl.transform.position = Vector3.MoveTowards(actualLvl.transform.position,
                                       new Vector2(actualLvl.transform.position.x - 100, actualLvl.transform.position.y), 0.1f);
            if (nextLvl.transform.position.x <= NextPosition.x)
            {
                lvlIsMoving = false;

                for (int i = 0; i < plants.Length; i++)
                {
                    plants[i].GetComponent<Plant>().OnScreen = true;
                }
            }
            yield return new WaitForSeconds(0.0f);
        }
    }
}
