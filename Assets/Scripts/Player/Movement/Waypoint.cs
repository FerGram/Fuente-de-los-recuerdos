using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Range(0, 5)]
    public float actionRadius = 1f;
    public List<Waypoint> neighbours;

    private int _waypointNum;

    // private void Start() {
        
    //     for (int i = 0; i < transform.parent.childCount; i++){

    //         if (transform.parent.GetChild(i) == transform) _waypointNum = i;
    //     }

    // }

    public int GetWaypointNum(){
        return _waypointNum;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }
}