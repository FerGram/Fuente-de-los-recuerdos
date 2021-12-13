using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FadeToraOutro : MonoBehaviour
{
    private SpriteRenderer _image;

    void Start()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    public void StartFadeOut(){
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut(){
        yield return new WaitForSeconds(6f);
        _image.DOFade(0, 2f);
    }
}
