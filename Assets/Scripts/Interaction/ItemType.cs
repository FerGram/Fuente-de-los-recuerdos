using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemEnum{
    TractorTire,
    BikeTire,
    CarWheel,
    CarTire,
	ChessPieces
}

public class ItemType : MonoBehaviour
{
    [SerializeField] ItemEnum _itemType;

    public ItemEnum GetItemtype() => _itemType;
}
