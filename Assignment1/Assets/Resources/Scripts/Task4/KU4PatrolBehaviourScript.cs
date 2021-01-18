using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KU4PatrolBehaviourScript : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] GameObject targetPrefab;

    [Header("Obstacles")]
    [SerializeField] GameObject obstaclePrefab;
    public List<Vector3> availableObstacles;


    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    public Transform objectToMove;


    private GameObject targetParent;
    private GameObject obstacleParent;

    // Start is called before the first frame update
    void Start()
    {
        targetParent = new GameObject("Targets");
        targetParent.transform.position = new Vector3(0f, 0f);


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

    private void AddTargets()
    {
        Vector3 position = RandomizeLocation();

        availableObstacles.Add(position);
        GameObject target = Instantiate(targetPrefab, position, Quaternion.identity);

        target.transform.SetParent(targetParent.transform);

    }



    //Method to spawn Obstacles
    private void AddObstacles()
    {
        Vector3 position = RandomizeLocation();

        availableObstacles.Add(position);
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
        obstacle.transform.SetParent(obstacleParent.transform);

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

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemy.GetComponent<customAIMoveScript>().enabled = true;
    }

    private void StartEnemy()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemy.GetComponent<customAIMoveScript>().enabled = true;
    }

    //Coroutine to run all the tasks (spawn waypoints, spawn obstacles, scan, enable obstacle movement and start AI move coroutine)
    IEnumerator TaskRun()
    {
        for (int counter = 0; counter < 10; counter++)
        {
            AddTargets();
            yield return new WaitForSeconds(0.5f);
        }
        
        for (int counter = 0; counter < 5; counter++)
        {
            AddObstacles();
            yield return new WaitForSeconds(0.5f);
        }
        Scan();
        StartAI();
        StartEnemy();

        yield return null;
    }
}

/*In this script, I am first spawning the waypoints and assigning them to a list. Then the obstacles are spawned and they have their own script that handles movement which is turned on after a scan is completed.
  Afterwards the moveAi coroutine is started and here the AI will move from waypoint to another while dodging the obstacles.*/