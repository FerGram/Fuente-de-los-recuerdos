using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class GameStateData : Singleton<GameStateData>
{
    public GameStateDataContainer gameData;

    [HideInInspector] public Story gameStory;

    //NOTE about below function:
    //Every scene there's a new DialogueDisplay instance and you can't be binding every time these functions
    //because the gamestory object is persistent and also because INK doesn't allow you to do so.
    //If you bind the function in scene1, change to scene2 and then call the binded function, you'd be actually
    //calling the function you binded to the scene1 DialogueDisplay instance (which is now null) and not the current 
    //scene one.

    //Solution: Bind them once (when the gamestory object is created) and have the callbacks here, then call the 
    //correct DialogueDisplay instance
    public void BindStoryFunctions(){

        gameStory.BindExternalFunction("startMinigame", (int minigame) =>
        { EnterMinigameResponse(minigame);});
        gameStory.BindExternalFunction("startCinematic", () =>
        { LoadCinematic();});
        gameStory.BindExternalFunction("startEndCinematic", () =>
        { LoadEndCinematic();});
    }

    private void EnterMinigameResponse(int minigame){
        DialogueDisplay dd = FindObjectOfType<DialogueDisplay>();
        if (dd != null) dd.EnterMinigameResponse(minigame);
    }

    private void LoadCinematic(){
        DialogueDisplay dd = FindObjectOfType<DialogueDisplay>();
        if (dd != null) dd.LoadCinematic();
    }

    private void LoadEndCinematic(){
        // DialogueDisplay dd = FindObjectOfType<DialogueDisplay>();
        // if (dd != null) dd.LoadEndCinematic();
    }

    private void EnableUI(int minigame){
        DialogueDisplay dd = FindObjectOfType<DialogueDisplay>();
        if (dd != null) dd.EnableUI(minigame);
    }

    public void AssignMinigameEvent(){

        if (MinigameEvents.current != null){

            MinigameEvents.current.onUnloadMinigame += EnableUI;
        }
    }
}
