using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTriggerTora : DialogueTrigger
{
    //Method triggered from GameInteraction Event
    public override void OnInteract()
    {
        //Decide ink file to play
        _JSONDataContainer.SetJSON(_inkJSON);
		_JSONDataContainer.SetPath("Tora");

        //Execute base class funcitonality
        base.OnInteract();
    }

    //Method triggered from DragAndDrop
    public override void OnInteract(GameObject obj){

        ItemEnum type = obj.GetComponent<ItemType>().GetItemtype();

        //Decide ink file to play
        switch(type){
            case ItemEnum.TractorTire:
				_JSONDataContainer.SetPath("ToraTractorTire");
				base.OnInteract();
				break;  
            case ItemEnum.BikeTire:
				_JSONDataContainer.SetPath("ToraBikeTire");
				base.OnInteract();
				break;  
        }
        
        //Execute base class funcitonality
        //base.OnInteract(obj);
    }
}
