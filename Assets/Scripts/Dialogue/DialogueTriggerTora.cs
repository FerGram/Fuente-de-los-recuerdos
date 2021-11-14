using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerTora : DialogueTrigger
{
    //Method triggered from GameInteraction Event
    public override void OnInteract()
    {
        //Decide ink file to play
        _JSONDataContainer.SetJSON(_inkJSON[0]);
        
        //Execute base class funcitonality
        base.OnInteract();
    }

    //Method triggered from DragAndDrop
    public override void OnInteract(GameObject obj){

        ItemEnum type = obj.GetComponent<ItemType>().GetItemtype();

        //Decide ink file to play
        switch(type){
            case ItemEnum.GreenSquare: _JSONDataContainer.SetJSON(_inkObjectJSON[0]); break;  
            case ItemEnum.RedSquare:   _JSONDataContainer.SetJSON(_inkObjectJSON[1]); break;  
        }
        
        //Execute base class funcitonality
        base.OnInteract(obj);
    }
}
