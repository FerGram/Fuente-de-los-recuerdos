using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject _visualCue;
    [SerializeField] TextAsset _inkJSON;

    private bool _playerInRange = false;

    private void Awake() {

        _visualCue.SetActive(false);
    }

    private void Update() {
        
        if (_playerInRange) {
           
            _visualCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space)){ //TO-Do Change click on character

                DialogueManager.Instance.StartDialogue(_inkJSON); //How to decide which one to play if more than 1?
            }
        }
        else _visualCue.SetActive(false);
    }

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

    public void TriggerDialogue(){
        
    }
}
