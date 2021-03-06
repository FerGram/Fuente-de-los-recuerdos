using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragAndDrop : MonoBehaviour, IDragHandler, IDropHandler, IPointerDownHandler, 
                           IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 _slotPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        _slotPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        transform.position = _slotPos;

        GameObject dropObject = GetDropObject();

        if (dropObject != null){

            DialogueTrigger trigger = dropObject.GetComponent<DialogueTrigger>();

            if (trigger != null) trigger.OnInteract(gameObject);
        }
        TextMeshProUGUI objectName = FindObjectOfType<InventoryUI>().transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        objectName.text = "";
    }

    private GameObject GetDropObject(){

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);

        if (hit) return hit.collider.gameObject;
        return null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TextMeshProUGUI objectName = FindObjectOfType<InventoryUI>().transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        objectName.text = gameObject.name.TrimEnd("UI(Clone)".ToCharArray());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       TextMeshProUGUI objectName = FindObjectOfType<InventoryUI>().transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        objectName.text = "";
    }
}
