using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        Debug.Log("Hello");
        
        // if (other.tag == "NPC"){

        //     other.GetComponent<DialogueTrigger>().TriggerDialogue();
        // }
    }
}
