using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] GameObject obstaclePrefab;
    public List<Vector3> availableObstacles;

    [Header("Enemy")]
    [SerializeField] GameObject enemyAI;


    private GameObject obstacleParent;
    private GameObject enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        availableObstacles = new List<Vector3>();

        obstacleParent = new GameObject("Obstacles");
        obstacleParent.transform.position = new Vector3(0f, 0f);

        enemyParent = new GameObject("Enemies");
        enemyParent.transform.position = new Vector3(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Method to Randomize Spawn Locations
    public Vector3 RandomizeLocation()
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
    public void AddObstacles()
    {
        Vector3 position = RandomizeLocation();

        availableObstacles.Add(position);
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);

        obstacle.transform.SetParent(obstacleParent.transform);

    }

    //Method to scan using A* Pathfinding scanner
    public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }

    //Method to spawn AI
    public void SpawnAI()
    {
        GameObject enemy = Instantiate(enemyAI, RandomizeLocation(), Quaternion.identity);
        enemy.transform.SetParent(enemyParent.transform);
    }

    //Method to start AI movement script
    public void StartAI()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<customAIMoveScript>().enabled = true;
        }
    }
}
