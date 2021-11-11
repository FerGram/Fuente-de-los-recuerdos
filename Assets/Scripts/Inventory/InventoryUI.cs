using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventoryDataContainer _inventory;
    [SerializeField] Transform[] _slots;

    private bool[] _hasAnItem;

    private void Awake() {
        
        _hasAnItem = new bool[_slots.Length];
    }

    public void AddItemToUI(){

        if (_inventory != null){

            for (int i = 0; i < _slots.Length; i++)
            {
                //Slot free
                if (!_hasAnItem[i]){

                    Instantiate(_inventory.GetLast(), _slots[i].transform);
                    _hasAnItem[i] = true; 
                    break;
                }
            }
        }
    }
}
