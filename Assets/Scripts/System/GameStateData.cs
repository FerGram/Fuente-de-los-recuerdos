using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class GameStateData : Singleton<GameStateData>
{
    public GameStateDataContainer gameData;

    [HideInInspector] public Story gameStory;
}
