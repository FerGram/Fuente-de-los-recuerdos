using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateDataContainer", menuName = "GameStateDataContainer", order = 54)]
public class GameStateDataContainer : ScriptableObject {
    
    [Header("General")]
    public bool playFirstTime = false;

    [Header("Car Crash Scene")]
    public bool firstDialoguePlayed = false;
    public bool carPieceInPlace = false;
}
