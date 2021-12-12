using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventoryDataContainer _inventory;
    [SerializeField] Transform[] _slots;

    private bool[] _hasAnItem;

    private void Awake() {
        
        _hasAnItem = new bool[_slots.Length];

        //If player has items between scenes, add them to the UI
        for (int i = 0; i < _inventory._inventoryItems.Count; i++)
        {
            if (i == _slots.Length) break;
            Instantiate(_inventory._inventoryItems[i], _slots[i].transform);
            
            _hasAnItem[i] = true;
        }
    }

    public void AddItemToUI(){

        if (_inventory != null){

            for (int i = 0; i < _slots.Length; i++)
            {
                //Slot free
                if (!_hasAnItem[i]){

                    GameObject go = Instantiate(_inventory.GetLast(), _slots[i].transform);
                    Vector3 intialScale = go.transform.localScale;
                    go.transform.localScale *= 0.25f;
                    go.transform.DOScale(intialScale, 0.25f).SetEase(Ease.InOutBack);
                    _hasAnItem[i] = true; 
                    break;
                }
            }
        }
    }

    public void RemoveItem(GameObject obj){

        if (_inventory != null){

            for (int i = 0; i < _slots.Length; i++)
            {
                //If the child of the slot is the gameobject that has to be removed
                if (_slots[i].GetChild(0).gameObject == obj){

                    _inventory.RemoveItemAt(i);
                    _hasAnItem[i] = false; 
                    Destroy(_slots[i].GetChild(0).gameObject);
                    break;
                }
            }
        }
    }
}
