using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoveScript : MonoBehaviour
{

    [Header("Waypoints")]
    [SerializeField] GameObject waypointPrefab;
    public List<Transform> waypoints;

    ObstacleSpawnScript obsSpawner;

    private GameObject waypointParent;

    public Transform objectToMove;

    public int waypointCounter = 0;

    public bool timeToMove = false;


    // Start is called before the first frame update
    void Start()
    {

        objectToMove = this.transform;

        obsSpawner = Camera.main.GetComponent<ObstacleSpawnScript>();

        waypointParent = new GameObject("Waypoints");
        waypointParent.transform.position = new Vector3(0f, 0f);


        StartCoroutine(TaskRun());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to randomize locations
    private Vector3 RandomizeLocation()
    {
        Vector3 position;
        float x;
        float y;

        do
        {
            x = Random.Range(-5.5f, 5.5f);
            y = Random.Range(-6.5f, 6.5f);

            position = new Vector3(x, y);
        } while (obsSpawner.availableObstacles.Contains(new Vector3(x, y)));

        return position;
    }


    //Method to spawn waypoints
    private void AddWaypoints()
    {
        Vector3 position = RandomizeLocation();
        obsSpawner.availableObstacles.Add(position);


        GameObject waypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
        waypoints.Add(waypoint.transform);
        waypoint.transform.SetParent(waypointParent.transform);

    }

    //Method to scan using A* Pathfinding scanner
    public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
        Debug.Log("Scan Complete");
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
        timeToMove = true;
    }

    //Coroutine to move the obstacles to the waypoints
    IEnumerator moveOBS()
    {
        foreach (Transform waypointTransform in waypoints)
        {
            while (Vector3.Distance(objectToMove.position, waypointTransform.position) > 0.1f)
            {
                objectToMove.position = Vector3.MoveTowards(objectToMove.position, waypointTransform.position, 1f);
                Scan();
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
