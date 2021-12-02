using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MinigameWindow : MonoBehaviour
{
	[SerializeField]
	float scaleDuration;

	[SerializeField]
	Ease ease;
	Vector3 _oriScale, _startScale;

    // Start is called before the first frame update
    void Start()
    {
		_oriScale = transform.localScale;
		_startScale = transform.localScale / 10;

		StartCoroutine(TryTween());
	}

    // Update is called once per frame
    void Update()
    {
		//if (Input.GetMouseButtonDown(0))
		//	StartCoroutine(TryTween());
    }

	IEnumerator TryTween()
	{
		transform.localScale = _startScale;
		yield return new WaitForSeconds(0f);
		transform.DOScale(_oriScale, scaleDuration).SetEase(ease);
	}
}
