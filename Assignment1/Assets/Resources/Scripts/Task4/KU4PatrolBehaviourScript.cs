using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KU4PatrolBehaviourScript : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] GameObject waypointPrefab;
    public List<Transform> waypoints;


    [Header("Obstacles")]
    [SerializeField] GameObject obstaclePrefab;
    public List<Vector3> availableObstacles;


    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    public Transform objectToMove;


    private GameObject waypointParent;
    private GameObject obstacleParent;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Seeker>();

        waypointParent = new GameObject("Waypoints");
        waypointParent.transform.position = new Vector3(0f, 0f);


        obstacleParent = new GameObject("Obstacles");
        obstacleParent.transform.position = new Vector3(0f, 0f);

        StartCoroutine(TaskRun());

       

    }

    // Update is called once per frame
    void Update()
    {
    }

    //Method to Randomize Spawn Locations
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
        } while (availableObstacles.Contains(new Vector3(x, y)));

        return position;
    }

    //Method to spawn Obstacles
    private void AddObstacles()
    {
        Vector3 position = RandomizeLocation();

        availableObstacles.Add(position);
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);

        obstacle.transform.SetParent(obstacleParent.transform);

    }

    //Method to spawn waypoints
    private void AddWaypoints()
    {
        Vector3 position = RandomizeLocation();
        availableObstacles.Add(position);

       
        GameObject waypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
        waypoints.Add(waypoint.transform);
        waypoint.transform.SetParent(waypointParent.transform);
    }

    //Method to scan using A* Pathfinding scanner
    private void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
        Debug.Log("Scan Complete");
    }


    private void StartAI() 
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.GetComponent<customObstacleMoveScript>().enabled = true;
        }
    }

    //Coroutine to run all the tasks (spawn waypoints, spawn obstacles, scan, enable obstacle movement and start AI move coroutine)
    IEnumerator TaskRun()
    {
        for (int counter = 0; counter < 10; counter++)
        {
            AddWaypoints();
            yield return new WaitForSeconds(0.5f);
        }
        for (int counter = 0; counter < 5; counter++)
        {
            AddObstacles();
            yield return new WaitForSeconds(0.5f);
        }
        Scan();
        StartAI();

        StartCoroutine(moveAI());

        yield return null;
    }

    //Coroutine to move the AI to the waypoints
    IEnumerator moveAI() 
    {
         foreach (Transform waypointTransform in waypoints)
         {
             while (Vector3.Distance(objectToMove.position, waypointTransform.position) > 0.1f)
             {
                objectToMove.position = Vector3.MoveTowards(objectToMove.position, waypointTransform.position, 1f);
                pathToFollow = seeker.StartPath(objectToMove.position, waypointTransform.position);
                Scan();
                yield return new WaitForSeconds(0.5f);
                
            }

             yield return null;
         }
    }
}

/*In this script, I am first spawning the waypoints and assigning them to a list. Then the obstacles are spawned and they have their own script that handles movement which is turned on after a scan is completed.
  Afterwards the moveAi coroutine is started and here the AI will move from waypoint to another while dodging the obstacles.*/