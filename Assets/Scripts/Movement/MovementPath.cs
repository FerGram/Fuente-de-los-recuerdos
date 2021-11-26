using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField] Color _circleColor;
    [SerializeField] Color _lineColor;

    public List<Waypoint> Waypoints {get; private set;}

    private void Awake() {

        Waypoints = new List<Waypoint>();

        //Get children and add them to Waypoints
        for (int i = 0; i < GetChildCount(); i++){

            Waypoints.Add(GetChildAtIndex(i).GetComponent<Waypoint>());
        }

        //Only the points in radius will be added to neighbours
        foreach (var point in Waypoints)
        {
            point.neighbours.Clear();

            foreach (var possibleNeighbour in Waypoints){

                if (point == possibleNeighbour) continue;
                if (Vector2.Distance(point.transform.position, possibleNeighbour.transform.position) 
                    < point.actionRadius + possibleNeighbour.actionRadius){
                    
                    if (!point.neighbours.Contains(possibleNeighbour)){
                        point.neighbours.Add(possibleNeighbour);
                    }
                }
                else if (point.neighbours.Contains(possibleNeighbour)) point.neighbours.Remove(possibleNeighbour);
            }
        }
    }

    public Transform GetChildAtIndex(int i){

        return transform.GetChild(i);
    }

    public int GetChildCount(){

        return transform.childCount;
    }
}
