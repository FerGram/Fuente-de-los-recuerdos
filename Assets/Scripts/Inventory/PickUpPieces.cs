using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPieces : PickUp
{
	public override void OnInteract()
	{

		if (_playerInRange)
		{

			if (UIItem != null) _inventory.AddItem(UIItem);
			if (_onItemPickUp != null) _onItemPickUp.Raise();

			Destroy(gameObject);
		}
	}
}
