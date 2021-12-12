using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTriggerCar : DialogueTrigger
{
    //Method triggered from GameInteraction Event
    public override void OnInteract()
    {
        //Decide ink file to play
        _JSONDataContainer.SetJSON(_inkJSON);
        _JSONDataContainer.SetPath("CarDefault");
        
        //Execute base class funcitonality
        base.OnInteract();
    }

    //Method triggered from DragAndDrop
    public override void OnInteract(GameObject obj){

        ItemEnum type = obj.GetComponent<ItemType>().GetItemtype();

        //Decide ink file to play
        if (type == ItemEnum.CarWheel){
            _JSONDataContainer.SetPath("CarWheel");

            //Execute base class funcitonality
            base.OnInteract(obj);

            if (_playerInRange) GameStateData.Instance.gameData.carPieceInPlace = true;
        }

        if (type == ItemEnum.CarTire){
            _JSONDataContainer.SetPath("CarFull");

            //Execute base class funcitonality
            base.OnInteract(obj);
        }
        
    }
}
