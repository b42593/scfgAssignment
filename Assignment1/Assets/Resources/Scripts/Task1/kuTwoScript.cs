using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuTwoScript : MonoBehaviour
{

    [Header("Obstacles")]
    [SerializeField] GameObject obstaclePrefab;
    public List<Vector3> availableObstacles;

    private GameObject obstacleParent;

    // Start is called before the first frame update
    void Start()
    {
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

    //Method to spawn scan
    private void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }

    //Coroutine to run all the tasks (spawn obstacles, scan)
    IEnumerator TaskRun() 
    {
        for (int counter = 0; counter < 5; counter++) 
        {
            AddObstacles();
            yield return new WaitForSeconds(0.5f);
        }
        Scan();

        yield return null;
    }

    //Added random generated obstacles(PRESENT IN KU2 SCENE)
}
