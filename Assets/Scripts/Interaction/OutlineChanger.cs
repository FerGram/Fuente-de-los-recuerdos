using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineChanger : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void SetOutline(bool value)
    {
        if (value) rend.material.SetFloat("_OutlineAlpha", 1);
        else rend.material.SetFloat("_OutlineAlpha", 0);
    }
}
