using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 7)]
    public string[] sentences;
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(){ //TO-DO: make singleton
    
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
