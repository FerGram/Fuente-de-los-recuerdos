using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateDataContainer", menuName = "GameStateDataContainer", order = 54)]
public class GameStateDataContainer : ScriptableObject {

	[Header("General")]
	public bool firstTimePlaying = false;
	public ScenesEnum sceneToLoad = ScenesEnum._1_CarCrash;

	[Header("Car Crash Scene")]
	public bool firstDialoguePlayed = false;
	public bool carPieceInPlace = false;

	[Header("Bar")]
	public bool chessPiecesInPlace = false;
	public bool bottlesFilled = false;

	[Header("Plaza")]
    [Range(0,3)] public int fountainTrickleAmount = 0;

    [Header("Ending")]
    public bool isInCinematic = false;
    public bool nextMorningShown = false;
}

