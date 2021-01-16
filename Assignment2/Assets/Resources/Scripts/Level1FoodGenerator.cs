using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1FoodGenerator : MonoBehaviour
{

    [Header("Obstacles")]
    public List<Transform> obstacles;

    //private GameObject waypointParent;
    private GameObject foodParent;
    private GameObject foodPrefab;


    private bool occupiedByObstacle = false;

    // Start is called before the first frame update
    void Start()
    {

        /*waypointParent = new GameObject("Waypoints");
        waypointParent.transform.position = new Vector3(0f, 0f);*/
        foodPrefab = Resources.Load<GameObject>("Prefabs/Food");

        foodParent = new GameObject("Food");
        foodParent.transform.position = new Vector3(0f, 0f);

        obstacleLocator();


        //StartCoroutine(TaskRun());
        StartCoroutine(foodGeneration());



    }

    // Update is called once per frame
    void Update()
    {
    }

    GameObject createFood(float xpos, float ypos)
    {
        return Instantiate(foodPrefab, new Vector3(xpos, ypos), Quaternion.identity);
    }


    void obstacleLocator() 
    {
        GameObject[] obstacle = GameObject.FindGameObjectsWithTag("Obstacle");
        //GameObject waypoint = Instantiate(waypointPrefab, position, Quaternion.identity);

        foreach (GameObject obstaclePostion in obstacle)
        {
            
            obstacles.Add(obstaclePostion.transform);
        }

        
        
    }


    IEnumerator foodGeneration()
    {
        bool alternatey = false;
        for (float ycoord = -9.5f; ycoord <= 9.5f; ycoord++)
        {
            //for each row
            for (float xcoord = -9.5f; xcoord <= 9.5f; xcoord++)
            {
               
                
                if (alternatey)
                {
                    if ((Mathf.Floor(xcoord) % 4 == 0))
                    {
                        GameObject food = createFood(xcoord, ycoord);
                        food.transform.SetParent(foodParent.transform);
                        food.GetComponent<SpriteRenderer>().color = Color.green;
                    }

                }
                else
                {
                    if ((Mathf.Floor(xcoord) % 4 == 0))
                    {
                        GameObject food = createFood(xcoord, ycoord);
                        food.transform.SetParent(foodParent.transform);
                        food.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
            alternatey = !alternatey;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}