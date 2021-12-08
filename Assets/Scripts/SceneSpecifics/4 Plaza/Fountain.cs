using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    private ParticleSystem[] particles;

    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();

        switch(GameStateData.Instance.gameData.fountainTrickleAmount){

            case 0: NotTrickle(); break;
            case 1: LittleTrickle(); break;
            case 2: ModerateTrickle(); break;
            case 3: FullTrickle(); break;
        }
    }

    private void FullTrickle()
    {
        foreach (var item in particles)
        {
            item.gameObject.SetActive(true);

            var main = item.main;
            var emissionBurst = item.emission.GetBurst(0);

            main.startSize = 3;
            emissionBurst.time = 0f;

            item.emission.SetBurst(0, emissionBurst);
        }
    }

    private void ModerateTrickle()
    {
        foreach (var item in particles)
        {
            item.gameObject.SetActive(true);

            var main = item.main;
            var emissionBurst = item.emission.GetBurst(0);
            
            main.startSize = 3;
            emissionBurst.time = 0.12f;

            item.emission.SetBurst(0, emissionBurst);
        }
    }

    private void LittleTrickle()
    {
        foreach (var item in particles)
        {
            item.gameObject.SetActive(true);

            //Vaya puta gilipollez tener que hacer variables
            var main = item.main;
            var emissionBurst = item.emission.GetBurst(0);

            main.startSize = 1;
            emissionBurst.time = 0.2f;

            item.emission.SetBurst(0, emissionBurst);
        }
    }

    private void NotTrickle()
    {
        foreach (var item in particles)
        {
            item.gameObject.SetActive(false);
        }
    }
}
