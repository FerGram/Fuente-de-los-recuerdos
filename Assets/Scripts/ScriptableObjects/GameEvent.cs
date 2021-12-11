using System.Collections.Generic;
using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent", order = 51)]
public class GameEvent : ScriptableObject {
    
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void RegisterListener(GameEventListener listener){
        _listeners.Add(listener);
    }
    
    public void UnegisterListener(GameEventListener listener){
        _listeners.Remove(listener);       
        // if (this.name == "OnDialogueEnded") {
        //     string text = listener + "removed. " + "Listeners for OnDialogueEnded left: ";
        //     for (int i = 0; i < _listeners.Count; i++)
        //     {
        //         text += _listeners[i] + ", ";
        //     }
        //     Debug.Log(text);
        // }
    }

    public void Raise(){

        // string text = "Listeners for " + this.name + " are: ";
        for (int i = 0; i < _listeners.Count; i++)
        {
            // text += _listeners[i].gameObject.name + ", ";
            _listeners[i].RaiseResponse();
        }
        // text += " NUMBER OF LISTENERS: " + _listeners.Count;
        // Debug.Log(text);
    }
}
