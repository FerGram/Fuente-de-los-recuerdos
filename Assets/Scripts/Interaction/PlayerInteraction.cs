using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable _currentTarget;

    private void Update()
    {
        RaycastForInteractable();

        if (Input.GetMouseButtonDown(1) && _currentTarget != null){
            
            _currentTarget.OnInteract();
        }
    }

    private void RaycastForInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (hit){

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null){

                //The current object is the same as the currentTarget so don't do anything
                if (_currentTarget == interactable) return;

                //Havent hovered over an interactable yet
                else if (_currentTarget == null){

                    _currentTarget = interactable;
                    _currentTarget.OnStartHover();

                }
                //Hover over a different object than previous one in the same frame (very rare to occur)
                else {
                    _currentTarget.OnEndHover();
                    _currentTarget = interactable;
                    _currentTarget.OnStartHover();
                }
            }
            //If something is hit but is not interactable (act as if nothing was hit)
            else{
                if (_currentTarget != null){

                    _currentTarget.OnEndHover();
                    _currentTarget = null;
                }
            }
        }
        //If nothing is hit
        else{
            if (_currentTarget != null){

                _currentTarget.OnEndHover();
                _currentTarget = null;
            }
        }    
    }
}
