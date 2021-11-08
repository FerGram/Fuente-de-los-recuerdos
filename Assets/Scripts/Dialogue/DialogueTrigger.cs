using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset _inkJSON;
    [SerializeField] JSONDataContainer _JSONDataContainer;
    [SerializeField] GameEvent _triggerDialogue;

    private bool _playerInRange = false;


    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player"){

            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player"){
            
            _playerInRange = false;
        }
    }

    //Method triggered from GameInteraction Event
    public void OnInteract()
    {
        if (_playerInRange) {
    
            _JSONDataContainer.SetJSON(_inkJSON);
            _triggerDialogue.Raise(); //Triggers GameEvent
        }
    }
}
