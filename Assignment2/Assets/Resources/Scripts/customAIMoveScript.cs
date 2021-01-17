using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;


public class customAIMoveScript : MonoBehaviour
{
    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    //a reference from the UI to the green box
    GameObject target;

    //public List<Transform> obstacleNodes;


    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Target");

        Debug.Log(this.name);

        //the instance of the seeker attached to this game object
        seeker = GetComponent<Seeker>();


        //generate the initial path
        pathToFollow = seeker.StartPath(transform.position, target.transform.position);

        

        //move the red robot towards the green enemy
        StartCoroutine(moveTowardsTarget(this.transform));
    }




    //Coroutine that moves the enemy to the target
    IEnumerator moveTowardsTarget(Transform t)
    {

        while (true)
        {

            List<Vector3> posns = pathToFollow.vectorPath;
            Debug.Log("Positions Count: " + posns.Count);

            for (int counter = 0; counter < posns.Count; counter++)
            {
                if (posns[counter] != null) { 
                    while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                    {
                        t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                        //since the enemy is moving, I need to make sure that I am following him
                        pathToFollow = seeker.StartPath(t.position, target.transform.position);
                        //wait until the path is generated
                        yield return seeker.IsDone();
                        //if the path is different, update the path that I need to follow
                        posns = pathToFollow.vectorPath;
                        
                        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
                        yield return new WaitForSeconds(0.5f);
                    }

                }
                //keep looking for a path because if we have arrived the enemy will anyway move away
                //This code allows us to keep chasing
                pathToFollow = seeker.StartPath(t.position, target.transform.position);
                yield return seeker.IsDone();
                posns = pathToFollow.vectorPath;
                //yield return null;

            }
            yield return null;
        }
    }

    /*In the custom move AI script, it is first finding the target and the seeker component. With a coroutine, using the seeker component that has been found, a path is generated from the enemy to the target and is stored in a list of positions.
    The enemy is then moved along this path with a delay of 0.5f and on each move the grid is scanned to update the hitbox of the enemy.*/


}


