using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] InventoryDataContainer _inventory;
    [SerializeField] GameEvent _onItemPickUp;
    [Space]
    [SerializeField] GameObject UIItem;

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

    public void OnInteract(){

        if (_playerInRange){

            if (UIItem != null) _inventory.AddItem(UIItem);
            if (_onItemPickUp != null) _onItemPickUp.Raise();

            Destroy(gameObject);
        }
    }
}
