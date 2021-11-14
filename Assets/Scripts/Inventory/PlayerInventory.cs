using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // [SerializeField] GameEvent _onItemPickUp;
    // [SerializeField] InventoryDataContainer _inventory;

    // private void OnTriggerEnter2D(Collider2D other) {
        
    //     PickUp pickUp = other.gameObject.GetComponent<PickUp>();

    //     if (pickUp != null){

    //         GameObject UIItem = pickUp.UIItem;

    //         if (UIItem != null)         _inventory.AddItem(UIItem);
    //         if (_onItemPickUp != null)  _onItemPickUp.Raise();

    //         Destroy(pickUp.gameObject);
    //     }
    // }

    // public void GiveItemToPlayer(GameObject UIItem){

    //     PickUp pickUp = UIItem.GetComponent<PickUp>();

    //     if (pickUp != null){

    //         if (UIItem != null)         _inventory.AddItem(UIItem);
    //         if (_onItemPickUp != null)  _onItemPickUp.Raise();

    //         Destroy(pickUp.gameObject);
    //     }
    // }

    
    // if (_inventory != null){

    //     for (int i = 0; i < _inventory.slots.Length; i++)
    //     {
    //         //Slot free
    //         if (!_inventory.isFull[i]){

    //             Instantiate(_UIItem, _inventory.slots[i].transform);
    //             _inventory.isFull[i] = true; 
    //             Destroy(gameObject);
    //             break;
    //         }
    //     }
    // }
}
