using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNudo : DialogueTrigger
{
	//Method triggered from GameInteraction Event
	public override void OnInteract()
	{
		//Decide ink file to play
		_JSONDataContainer.SetJSON(_inkJSON);
		_JSONDataContainer.SetPath("Nudo");
		
		//Execute base class funcitonality
		base.OnInteract();
	}

	//Method triggered from DragAndDrop
	public override void OnInteract(GameObject obj)
	{

		ItemEnum type = obj.GetComponent<ItemType>().GetItemtype();

		//Decide ink file to play
		switch (type)
		{
			case ItemEnum.TractorTire: _JSONDataContainer.SetPath("NudoTractorTire"); break;
			case ItemEnum.BikeTire: _JSONDataContainer.SetPath("NudoTractorTire"); break;
		}

		//Execute base class funcitonality
		base.OnInteract();
	}
}
