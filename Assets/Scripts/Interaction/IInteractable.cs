using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnStartHover();
    void OnInteract();
    void OnEndHover();
}
