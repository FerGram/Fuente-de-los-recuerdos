using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueTriggerHikers : DialogueTrigger
{
    [SerializeField] int _lineToDisplay;

    private bool triggeredOnce = false;

    public override void OnTriggerEnter2D(Collider2D other){

        if (other.gameObject.tag == "Player" && !triggeredOnce){
            triggeredOnce = true;
            StartCoroutine(StartMonologue());
        }
    }

    IEnumerator StartMonologue(){

        switch(_lineToDisplay){
            case 0: if (!GameStateData.Instance.gameData.nextMorningShown) yield return new WaitForSeconds(5f);
                    _JSONDataContainer.SetPath("HikersInitial"); 
                    break;
            case 1: _JSONDataContainer.SetPath("HikersFarVillage"); break;
            case 2: _JSONDataContainer.SetPath("HikersInFountain"); break;

        }
        _JSONDataContainer.SetJSON(_inkJSON);
        _triggerDialogue.Raise();
        yield return null;
    }
}
