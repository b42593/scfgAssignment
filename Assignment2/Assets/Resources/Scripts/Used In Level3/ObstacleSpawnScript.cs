using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnScript : MonoBehaviour
{

    [Header("Obstacles")]
    [SerializeField] GameObject movingObstaclePrefab;
    public List<Vector3> availableObstacles;

    int choice = 0;

    public bool horizontal = false;
    public bool vertical = false;

    public bool spawningFinished = false;

    private GameObject movingObstacleParent;


    // Start is called before the first frame update
    void Start()
    {
        movingObstacleParent = new GameObject("Moving Obstacles");
        movingObstacleParent.transform.position = new Vector3(0f, 0f);

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
        } while (availableObstacles.Contains(new Vector3(x, y)));

        return position;
    }

    //Method to spawn Obstacles
    private void AddObstacles()
    {
        Vector3 position = RandomizeLocation();

        availableObstacles.Add(position);
        GameObject obstacle = Instantiate(movingObstaclePrefab, position, Quaternion.identity);

        choice = Random.Range(0, 2);
        if (choice == 0)
        {
            horizontal = true;
            obstacle.transform.localScale = new Vector3(3, 1);
        }
        else 
        {
            vertical = true;
            obstacle.transform.localScale = new Vector3(1, 3);
        }  
        obstacle.transform.SetParent(movingObstacleParent.transform);

    }

    //Method to scan using A* Pathfinding scanner
    public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
        Debug.Log("Scan Complete");
    }

    //Method to Start the obstacle movement
    private void StartAI()
    {
        GameObject[] movingObstacles = GameObject.FindGameObjectsWithTag("MovingObstacle");
        foreach (GameObject obstacle in movingObstacles)
        {
            obstacle.GetComponent<ObstacleMoveScript>().enabled = true;

            if (obstacle.GetComponent<ObstacleMoveScript>().timeToMove) 
            {
                spawningFinished = true;
            }
        }
    }

    //Coroutine to run all the tasks (spawn obstacles, scan, enable obstacle movement and start AI move coroutine)
    IEnumerator TaskRun()
    {
        for (int counter = 0; counter < 5; counter++)
        {
            AddObstacles();
            yield return new WaitForSeconds(0.5f);
        }
        Scan();
        StartAI();
        

        yield return null;
    }


}
