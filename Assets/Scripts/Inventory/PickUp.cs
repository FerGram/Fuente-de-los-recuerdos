using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public InventoryDataContainer _inventory;
    public GameEvent _onItemPickUp;
    [Space]
    [SerializeField] GameObject UIItem;

    private bool _playerInRange = false;

    public virtual void Start() {
        if (_inventory._inventoryItems.Contains(UIItem)) Destroy(gameObject);
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

    public virtual void OnInteract(){

        if (_playerInRange){

            if (UIItem != null) _inventory.AddItem(UIItem);
            if (_onItemPickUp != null) _onItemPickUp.Raise();

            Destroy(gameObject);
        }
    }
}
