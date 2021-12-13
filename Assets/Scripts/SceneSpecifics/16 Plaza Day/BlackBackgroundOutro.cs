using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BlackBackgroundOutro : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Image _black;

    void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _black = GetComponent<Image>();
    }

    public void StartFadeOut(){
        GetComponent<Animator>().enabled = false;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut(){
        yield return new WaitForSeconds(4f); // 0-4
        _black.DOFade(1, 4f);                 //4-8
        yield return new WaitForSeconds(6f); //4-10
        _text.DOFade(1, 3f);                 //10-13
        yield return new WaitForSeconds(6f); //10-16
        _text.DOFade(0, 3f);                 //16-19
        yield return new WaitForSeconds(6f); //16-22

        //VAYA PUTO DESASTRE JAJAJAJAJ
        Application.Quit();
    }
}
