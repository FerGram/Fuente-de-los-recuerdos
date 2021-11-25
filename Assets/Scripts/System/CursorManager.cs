using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = Input.mousePosition;

        // if (Input.GetMouseButtonDown(1)){
        //     GetComponent<ParticleSystem>().Play();
        //     Debug.Log(GetComponent<ParticleSystem>().isEmitting);
        // }
    }
}
