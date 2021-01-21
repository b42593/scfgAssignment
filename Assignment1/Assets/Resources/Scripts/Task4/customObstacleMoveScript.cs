using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customObstacleMoveScript : MonoBehaviour
{

    [Header("Waypoints")]
    [SerializeField] GameObject waypointPrefab;
    public List<Transform> waypoints;

    KU4PatrolBehaviourScript patroller;

    public Transform objectToMove;

    private GameObject waypointParent;

    public int waypointCounter = 0;


    // Start is called before the first frame update
    void Start()
    {

        patroller = GameObject.Find("GameManager").GetComponent<KU4PatrolBehaviourScript>();


        waypointParent = new GameObject("Waypoints");
        waypointParent.transform.position = new Vector3(0f, 0f);

        StartCoroutine(TaskRun());

    }

    private void Update()
    {
        
    }

    // Method to randomize locations
    private Vector3 RandomizeLocation()
    {
        Vector3 position;
        int x;
        int y;

        do
        {
            x = Random.Range(-49, 49);
            y = Random.Range(-49, 49);

            position = new Vector3(x, y);
        } while (patroller.availableObstacles.Contains(new Vector3(x, y)));

        return position;
    }

    //Method to spawn waypoints
    private void AddWaypoints()
    {
        Vector3 position = RandomizeLocation();
        patroller.availableObstacles.Add(position);


        GameObject waypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
        waypoints.Add(waypoint.transform);
        waypoint.transform.SetParent(waypointParent.transform);
       
    }

    //Run the task
    IEnumerator TaskRun()
    {
        for (int counter = 0; counter < 2; counter++)
        {
            AddWaypoints();
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(moveOBS());

        
    }


    //Coroutine to move the obstacles to the waypoints
    IEnumerator moveOBS()
    {
        foreach (Transform waypointTransform in waypoints)
        {
            while (Vector3.Distance(objectToMove.position, waypointTransform.position) > 0.1f)
            {
                objectToMove.position = Vector3.MoveTowards(objectToMove.position, waypointTransform.position, 1f);
                
                yield return new WaitForSeconds(0.5f);

            }
            waypointCounter++;

            if (waypointCounter >= waypoints.Count)
            {
                StopCoroutine(moveOBS());

                waypointCounter = 0;
                StartCoroutine(moveOBS());
            }

            yield return null;
        }
    }

}


/*In the custom OBstacle Move script, it is first finding the target and the seeker component. With a coroutine, using the seeker component that has been found, a path is generated from the obstacle to the target and is stored in a list of positions.
    The obstacle is then moved along this path with a delay of 0.5f and on each move the grid is scanned to update the hitbox of the obstacle.*/
