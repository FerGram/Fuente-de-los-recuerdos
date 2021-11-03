using System.Collections.Generic;
using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent", order = 51)]
public class GameEvent : ScriptableObject {
    
    List<GameEventListener> _listeners = new List<GameEventListener>();

    public void RegisterListener(GameEventListener listener){

        _listeners.Add(listener);
    }
    
    public void UnegisterListener(GameEventListener listener){

        _listeners.Remove(listener);
    }

    public void Raise(){

        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].RaiseResponse();
        }
    }
}
