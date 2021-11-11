using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent _onStartHover;
    [SerializeField] UnityEvent _onInteract;
    [SerializeField] UnityEvent _onEndHover;

    public void OnStartHover()
    {
        if (_onStartHover != null) _onStartHover.Invoke();
    }

    public void OnInteract()
    {
       if (_onInteract != null)  _onInteract.Invoke();
    }

    public void OnEndHover()
    {
        if (_onEndHover != null) _onEndHover.Invoke();
    }

    
}