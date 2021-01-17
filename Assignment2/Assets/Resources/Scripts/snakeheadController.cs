using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeheadController : MonoBehaviour
{
    snakeGenerator mysnakegenerator;
    Level1FoodGenerator myfoodgenerator;

    [Header("Waypoints")]
    public List<Transform> waypoints;


    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    Transform objectToMove;


    private void Start()
    {

        waypoints = new List<Transform>();
        objectToMove = GameObject.FindGameObjectWithTag("Player").transform;

        seeker = GameObject.FindGameObjectWithTag("Player").GetComponent<Seeker>();

        mysnakegenerator = Camera.main.GetComponent<snakeGenerator>();
        myfoodgenerator = Camera.main.GetComponent<Level1FoodGenerator>();
        AddWaypoints();
        TaskRun();


    }

    // Update is called once per frame
    void Update()
    {
        

        /*
         if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
             transform.position -= new Vector3(1f,0);
         }
         if (Input.GetKeyDown(KeyCode.RightArrow))
         {
             transform.position += new Vector3(1f, 0);
         }
         if (Input.GetKeyDown(KeyCode.UpArrow))
         {
             transform.position += new Vector3(0, 1f);
         }
         if (Input.GetKeyDown(KeyCode.DownArrow))
         {
             transform.position -= new Vector3(0, 1f);
         }*/


    }


    private void AddWaypoints()
    {
        GameObject waypoint = GameObject.Find("Waypoint");
        waypoints.Add(waypoint.transform);
    }

    //Method to scan using A* Pathfinding scanner
    private void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
        Debug.Log("Scan Complete");
    }

    //Coroutine to run all the tasks (spawn waypoints, spawn obstacles, scan, enable obstacle movement and start AI move coroutine)
    void TaskRun()
    {
        StartCoroutine(moveAI());
    }


    //Coroutine to move the AI to the waypoints
    IEnumerator moveAI()
    {
        foreach (Transform waypointTransform in waypoints)
        {
            while (Vector3.Distance(objectToMove.position, waypointTransform.position) > 0.1f)
            {
                if (myfoodgenerator.foodGenFinished)
                {
                    objectToMove.position = Vector3.MoveTowards(objectToMove.position, waypointTransform.position, 1f);
                    pathToFollow = seeker.StartPath(objectToMove.position, waypointTransform.position);
                    Scan();
                    mysnakegenerator.savePosition();

                    //draw a tail of length
                    mysnakegenerator.drawTail(mysnakegenerator.snakeLength);
                }
                yield return new WaitForSeconds(0.5f);

            }

            yield return null;
        }
    }

}
