using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBottles : MonoBehaviour
{
    //GameStateDataContainer gameData;
    public bool bottlesFilled;

    [SerializeField] GameObject[] liquid;

    private void Start()
    {
        bottlesFilled = GameStateData.Instance.gameData.bottlesFilled;
    }

    void Update()
    {
        if (bottlesFilled == true)
        {
            liquid[0].SetActive(true);
            liquid[1].SetActive(true);
            liquid[2].SetActive(true);
        }
    }
}
