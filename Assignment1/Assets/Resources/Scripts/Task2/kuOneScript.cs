using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuOneScript : MonoBehaviour
{
    public List<Vector3> availableObstacles;

    [Header("Enemy")]
    [SerializeField] GameObject enemyAI;

    private GameObject enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = new GameObject("Enemies");
        enemyParent.transform.position = new Vector3(0f, 0f);
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

    //Method to spawn AI
    private void SpawnAI()
    {
        GameObject enemy = Instantiate(enemyAI, RandomizeLocation(), Quaternion.identity);
        enemy.transform.SetParent(enemyParent.transform);
    }

    //Method to scan
    private void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }

    //Method to start AI
    private void StartAI()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<customAIMoveScript>().enabled = true;
        }
    }

    //Coroutine to run all the tasks (spawn AI, scan and enable AI movement)
    IEnumerator TaskRun()
    {
        for (int counter = 0; counter < 10; counter++)
        {
            SpawnAI();
            yield return new WaitForSeconds(0.5f);
        }
        Scan();
        StartAI();

        yield return null;
    }


    //Added random generated enemyAI and made them head towards the same target. They avoid hitting each other especially when nearing the target.(PRESENT IN KU1 SCENE)


    /*A* Path finding implements this feature by using the Pathfinder to generate the grid by scanning the playspace dictated by the width and depth nodes and making the set obstacle layer unwalkable.
    Afterwards using the Seeker, AI Lerp and Destination Setter, we can assign a target for the enemy to follow which will generate a path on runtime to that target. 
    The enemy will follow that path while going around any obstacle it comes across.*/

}
