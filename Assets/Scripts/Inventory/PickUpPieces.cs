using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPieces : PickUp
{
	public override void Start()
	{
		base.Start();
		if (GameStateData.Instance.gameData.chessPiecesInPlace) Destroy(gameObject);
	}
}
