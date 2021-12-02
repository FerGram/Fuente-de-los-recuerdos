using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTriggerBarman : DialogueTrigger
{
    [Space]
    [SerializeField] ScenesEnum _minigameScene;

    //Method triggered from GameInteraction Event
    public override void OnInteract()
    {
        //Decide ink file to play
        _JSONDataContainer.SetJSON(_inkJSON);
		_JSONDataContainer.SetPath("Barman");
        
        //Execute base class funcitonality
        base.OnInteract();
    }

    //Method triggered from DragAndDrop
    public override void OnInteract(GameObject obj){

        ItemEnum type = obj.GetComponent<ItemType>().GetItemtype();

        //Decide ink file to play
        switch(type){
            case ItemEnum.GreenSquare: _JSONDataContainer.SetJSON(_inkJSON); break;  
            case ItemEnum.RedSquare: _JSONDataContainer.SetJSON(_inkJSON); break;  
        }
        
        //Execute base class funcitonality
        base.OnInteract(obj);
    }
}
