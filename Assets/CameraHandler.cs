using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement _player;

    void Update()
    {
        Vector3 playerPos = _player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);        
    }
}
