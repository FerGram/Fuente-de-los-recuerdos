using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueTriggerInitialCrash : DialogueTrigger
{
    public override void OnTriggerEnter2D(Collider2D other){

        if (other.gameObject.tag == "Player" && !GameStateData.Instance.gameData.firstDialoguePlayed){

            StartCoroutine(StartMonologue());
        }
    }

    IEnumerator StartMonologue(){

        yield return new WaitForSeconds(2f);
        _JSONDataContainer.SetPath("InitialCrash");
        _JSONDataContainer.SetJSON(_inkJSON);
        _triggerDialogue.Raise();
    }

    //Event listener
    public void OnDialogueEnded(){

        GameStateData.Instance.gameData.firstDialoguePlayed = true;
        Destroy(gameObject);
    }
}
