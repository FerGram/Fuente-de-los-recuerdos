using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MinigameWindow : MonoBehaviour
{
	[SerializeField] float scaleDuration;
	[SerializeField] Ease ease;

	private FadeInOutBackground background;
	private Vector3 _oriScale, _startScale;

    // Start is called before the first frame update
    void Start()
    {
		background = FindObjectOfType<FadeInOutBackground>();
		
		_oriScale = transform.localScale;
		_startScale = Vector3.zero;

		transform.localScale = _startScale;

		StartCoroutine(Maximize());
	}

	IEnumerator Maximize()
	{
		yield return new WaitForSeconds(0.05f);
		if (background != null) background.FadeInScene(false);

		yield return new WaitForSeconds(1f);
		transform.localScale = _startScale;
		transform.DOScale(_oriScale, scaleDuration).SetEase(ease);
		
		yield return new WaitForSeconds(2f);
		if (background != null) background.FadeInScene(true);
	}

	public IEnumerator Minimize()
	{
		yield return new WaitForSeconds(0.05f);
		if (background != null) background.FadeInScene(false);

		yield return new WaitForSeconds(1f);
		transform.localScale = _startScale;
		gameObject.SetActive(false);
		
		yield return new WaitForSeconds(2f);
		if (background != null) background.FadeInScene(true);
	}
}
