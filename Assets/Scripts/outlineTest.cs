using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outlineTest : MonoBehaviour
{
    private Renderer rend;
    private float value;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            
            ChangeMaterial();
        }
    }

    private void ChangeMaterial()
    {
        if (value == 0) value = 1;
        else value = 0;

        rend.material.SetFloat("_OutlineAlpha", value);
    }
}
