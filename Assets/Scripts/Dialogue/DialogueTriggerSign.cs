using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTriggerSign : DialogueTrigger
{
    [SerializeField] int _lineToPlay = 0;
    //Method triggered from GameInteraction Event
    public override void OnInteract()
    {
        //Decide ink file to play
        _JSONDataContainer.SetJSON(_inkJSON);
        
        switch(_lineToPlay){
            case 0: _JSONDataContainer.SetPath("SignDefault"); break;
            case 1: _JSONDataContainer.SetPath("SignHikers"); break;
            case 2: _JSONDataContainer.SetPath("PickedUpCarWheel"); break; //Pos aqui se queda me cago en dios
        }
        
        
        //Execute base class funcitonality
        base.OnInteract();
    }

    //Method triggered from DragAndDrop
    public override void OnInteract(GameObject obj){
		ItemEnum type = obj.GetComponent<ItemType>().GetItemtype();

		//Decide ink file to play
		switch (type)
		{
			default: _JSONDataContainer.SetPath("ItemToSign"); break;
		}

		//Execute base class funcitonality
		base.OnInteract();
	}
}
