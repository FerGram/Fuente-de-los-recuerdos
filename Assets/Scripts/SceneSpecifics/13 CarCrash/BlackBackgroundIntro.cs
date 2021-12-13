using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BlackBackgroundIntro : MonoBehaviour
{
    private TextMeshProUGUI _text;

    void Start()
    {
        if (GameStateData.Instance.gameData.nextMorningShown) {
            GetComponent<Animator>().enabled = true; 
            return;
        }
        _text = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn(){

        _text.DOFade(1, 4f);
        yield return new WaitForSeconds(4f);
        GetComponent<Animator>().enabled = true;
        _text.DOFade(0, 1f);
        GameStateData.Instance.gameData.nextMorningShown = true;
    }
}
