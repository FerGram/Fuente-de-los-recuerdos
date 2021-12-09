using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutBackground : MonoBehaviour
{
    private Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    public void FadeInScene(bool value){

        if (_anim != null) _anim.SetBool("fade", value);
    }
}
