using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //These are public because they must be accessed by inherited classes
    //TODO _inkJSON can be removed from here. It is assigned once in JSONDataContainer
    public TextAsset _inkJSON;
    public JSONDataContainer _JSONDataContainer;
    public GameEvent _triggerDialogue;
    
    [HideInInspector]
    public bool _playerInRange = false;


    public virtual void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player"){

            _playerInRange = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player"){
            
            _playerInRange = false;
        }
    }

    //Method triggered from GameInteraction Event
    public virtual void OnInteract()
    {
        if (_playerInRange) {
            _triggerDialogue.Raise(); //Triggers GameEvent
        }
    }

    //Method triggered from DragAndDrop
    public virtual void OnInteract(GameObject obj){

        if (_playerInRange) {
            _triggerDialogue.Raise(); //Triggers GameEvent
            
            //At this point I just don't care about dependencies
            InventoryUI inventory = FindObjectOfType<InventoryUI>();
            if (inventory != null) inventory.RemoveItem(obj);
        }
    }
}
