using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Range(0, 10)]
    public float actionRadius = 1f;
    [HideInInspector] public List<Waypoint> neighbours;

    private int _waypointNum;

    public int GetWaypointNum(){
        return _waypointNum;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }
}
