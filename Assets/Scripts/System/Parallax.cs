using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //A _parallaxEffect value of 0 means it stays in position and 1 moves with the camera (slow). -1 means fast

    [SerializeField] GameObject _camera;
    [SerializeField] [Range(-1,1)] float _parallaxEffect;

    private float _length;
    private float _startPos;
    
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = _camera.transform.position.x * _parallaxEffect;
        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);
    }
}
