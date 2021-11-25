using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCarWheel : PickUp
{
    public override void Start(){

        base.Start();
        if (GameStateData.Instance.gameData.carPieceInPlace) Destroy(gameObject);
    }
}
