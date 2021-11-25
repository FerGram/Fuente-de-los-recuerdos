using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(1)){
            // GetComponent<ParticleSystem>().Play();
            // Debug.Log(GetComponent<ParticleSystem>().isEmitting);
            StartCoroutine(Click());
        }
    }
    IEnumerator Click(){

        RectTransform rt = GetComponent<RectTransform>();
        rt.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        yield return new WaitForSeconds(0.1f);
        rt.localScale = new Vector3(1f, 1f, 1f);
    }
}
