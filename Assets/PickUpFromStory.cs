using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFromStory : PickUp
{
	[SerializeField]
	GameObject[] items;

	public override void Start()
	{
		
	}

	public override void OnInteract()
	{

	}

	public void GiveItem(int itemI)
	{
		if (items != null) _inventory.AddItem(items[itemI]);
		if (_onItemPickUp != null) _onItemPickUp.Raise();

	}
}
