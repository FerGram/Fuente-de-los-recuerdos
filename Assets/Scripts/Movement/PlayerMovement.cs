using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _waypointStop = 0.2f;
    [SerializeField] MovementPath _path;

    private Waypoint _current;
    private Animator _animator;
    private IInteractable _interactable;

    private void Start() {

        //Get the closest waypoint to player and set it as current waypoint
        _current = _path.Waypoints[0];

        for(int i = 1; i < _path.Waypoints.Count; i++)
        {
            if (Vector2.Distance(transform.position, _path.Waypoints[i].transform.position) < 
                Vector2.Distance(transform.position, _current.transform.position)){
                    _current = _path.Waypoints[i];
            }
        }
        transform.position = _current.transform.position;

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !GameStateData.Instance.gameData.isInCinematic){

            CalculateMovement();
        }
    }

    public void CalculateMovement(){

        Vector2 mousePos = GetMouseInWorldCoords();
        Waypoint target = SetTarget(mousePos);

        _interactable = GetInteractable();

        if (target != null)
        {

            Stack<Waypoint> path = BFS(target);

            if (path != null && path.Count > 1)
            {
                StopAllCoroutines();
                StartCoroutine(MovementRoutine(path, target));
            }
        }
    }

    public void CalculateMovement(Waypoint destination){

        Waypoint target = destination;

        _interactable = GetInteractable();

        if (target != null)
        {

            Stack<Waypoint> path = BFS(target);

            if (path != null && path.Count > 1)
            {
                StopAllCoroutines();
                StartCoroutine(MovementRoutine(path, target));
            }
        }
    }

    private IInteractable GetInteractable(){

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);

        if (hit) return hit.collider.gameObject.GetComponent<IInteractable>();
        return null;
    }

    private Vector3 GetMouseInWorldCoords()
    {
        //Get coordinates of mouse pos in world coordinates
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    IEnumerator MovementRoutine(Stack<Waypoint> path, Waypoint target){

        path.Pop(); //Current waypoint position not needed
        Waypoint nextPoint = path.Pop();

        while(target != _current){

            if (Vector2.Distance(transform.position, nextPoint.transform.position) < _waypointStop){
                _current = nextPoint;
                if (path.Count != 0) nextPoint = path.Pop();
            }
            MovePlayer(nextPoint.transform.position);
            yield return null;
        }
        SetAnimatorParam(0);
        if (_interactable != null) _interactable.OnInteract();
    }

    private void MovePlayer(Vector3 waypointPos){

        transform.position = Vector2.MoveTowards(transform.position, waypointPos, 
            Time.deltaTime * _speed);

        //Flip X
        if (waypointPos.x - transform.position.x < 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1,
                                                         transform.localScale.y, 
                                                         transform.localScale.z);
        }
        else if (waypointPos.x - transform.position.x > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                                                         transform.localScale.y,
                                                         transform.localScale.z);
        }


        if (_animator != null){
            SetAnimatorParam(1);
        }
    }

    private Waypoint SetTarget(Vector3 mousePos){

        Waypoint closestWaypoint = _path.Waypoints[0];
        foreach (var point in _path.Waypoints)
        {
            if (Vector3.Distance(mousePos, point.transform.position) < 
                Vector3.Distance(mousePos, closestWaypoint.transform.position)) closestWaypoint = point;
        }
        return closestWaypoint;
    }

    private Stack<Waypoint> BFS(Waypoint target){

        Queue<Waypoint> search = new Queue<Waypoint>();
        List<Waypoint> visited = new List<Waypoint>();
        Dictionary<Waypoint, Waypoint> parents = new Dictionary<Waypoint, Waypoint>();
        Waypoint current;
        search.Enqueue(_current);
        

        while(search.Count != 0){
            
            current = search.Dequeue();
            if (current == target) return SetMovementPath(parents, current);

            foreach (var neighbour in current.neighbours)
            {
                if (!search.Contains(neighbour) && !visited.Contains(neighbour)) {

                    search.Enqueue(neighbour);
                    parents.Add(neighbour, current);
                }
            }
            visited.Add(current);
        }
        return null;
    }

    private Stack<Waypoint> SetMovementPath(Dictionary<Waypoint, Waypoint> parents, Waypoint endpoint){

        Stack<Waypoint> path = new Stack<Waypoint>();
        Waypoint current = endpoint;
        path.Push(current);

        while(parents.ContainsKey(current)){

            path.Push(parents[current]);
            current = parents[current];
        }
        return path;
    }

    private void SetAnimatorParam(float x){
        _animator.SetFloat("x", x);
    }
}
