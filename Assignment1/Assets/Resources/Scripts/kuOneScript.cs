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

    public void SpawnAI()
    {
        GameObject enemy = Instantiate(enemyAI, RandomizeLocation(), Quaternion.identity);
        enemy.transform.SetParent(enemyParent.transform);
    }

    public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }


    public void StartAI()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<customAIMoveScript>().enabled = true;
        }
    }


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
}
