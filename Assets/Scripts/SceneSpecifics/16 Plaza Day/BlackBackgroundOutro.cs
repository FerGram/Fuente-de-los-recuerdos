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
        yield return new WaitForSeconds(8f);
        _black.DOFade(1, 6f);
        yield return new WaitForSeconds(5f);
        _text.DOFade(1, 3f);
        yield return new WaitForSeconds(6f);
        _text.DOFade(0, 3f);
        yield return new WaitForSeconds(6f);

        //VAYA PUTO DESASTRE JAJAJAJAJ
        Application.Quit();
    }
}
