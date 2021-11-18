using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryDataContainer", menuName = "InventoryDataContainer", order = 53)]
public class InventoryDataContainer : ScriptableObject
{

    public List<GameObject> _inventoryItems;

    public void AddItem(GameObject go)
    {

        _inventoryItems.Add(go);
    }

    public void RemoveItem(GameObject go)
    {

        _inventoryItems.Remove(go);
    }

    public GameObject RemoveItemAt(int i)
    {
        int count = _inventoryItems.Count;
        if (count == 0) return null;

        GameObject obj = _inventoryItems[i];
        _inventoryItems.RemoveAt(i);
        return obj;
    }

    public GameObject GetLast()
    {

        int count = _inventoryItems.Count;

        if (count == 0) return null;
        return _inventoryItems[count - 1];
    }

    public bool HasItem(GameObject go)
    {

        if (_inventoryItems.Contains(go)) return true;
        return false;
    }

    public bool HasItemByName(string goName)
    {

        foreach (GameObject item in _inventoryItems)
        {
            if (item.name == goName) return true;
        }
        return false;
    }

    public GameObject GetItemByName(string goName)
    {

        foreach (GameObject item in _inventoryItems)
        {
            if (item.name == goName) return item;
        }
        return null;
    }
}
