using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemySnakeController : MonoBehaviour
{
    spawnEnemy enemySnake;

    LineRenderer inGamePathLine;

    public int targetCounter = 0;
    public int currentTarget;

    public int tailsize;

    bool displayLine = false;

    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    //a reference from the UI to the target
    GameObject target;

    Material mat;

    public GameObject[] targets;


    // Start is called before the first frame update
    void Start()
    {
        mat = Resources.Load<Material>("Material/PathMat");

        inGamePathLine = this.gameObject.GetComponent<LineRenderer>();
        inGamePathLine.material = mat;



        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.3f);
        inGamePathLine.widthCurve = curve;

        if (SceneManager.GetActiveScene().name == "Level2") 
        {
            
            inGamePathLine.startColor = new Color32(240, 255, 0, 100);
            inGamePathLine.endColor = new Color32(199, 207, 36, 100);
        }

        currentTarget = 1;

        Debug.Log(this.name);

        //the instance of the seeker attached to this game object
        seeker = GetComponent<Seeker>();

        enemySnake = Camera.main.GetComponent<spawnEnemy>();


        tailsize = enemySnake.snakeLength;

        targets = GameObject.FindGameObjectsWithTag("Player");
        target = targets[0];

        //generate the initial path
        pathToFollow = seeker.StartPath(transform.position, target.transform.position);


        //move the red robot towards the green enemy
        StartCoroutine(moveTowardsTarget(this.transform));
    }

    private void Update()
    {
        DisplayLineDestination(this.transform);

        if (Input.GetKey(KeyCode.Space)) 
        {
            displayLine = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            displayLine = false;
        }

    }


    //Coroutine that moves the enemy to the target
    IEnumerator moveTowardsTarget(Transform t)
    {

        while (true)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= 0.5f && currentTarget != targets.Length)
            {
                targetCounter++;
                currentTarget++;
                target = targets[targetCounter];
            }


            List<Vector3> posns = pathToFollow.vectorPath;
            Debug.Log("Positions Count: " + posns.Count);

            if (this.transform != null) 
            {
                for (int counter = 0; counter < posns.Count; counter++)
                {
                    if (posns[counter] != null)
                    {
                        while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                        {
                            t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                            //since the enemy is moving, I need to make sure that I am following him

                            pathToFollow = seeker.StartPath(t.position, target.transform.position);

                        

                            //wait until the path is generated
                            yield return seeker.IsDone();
                            //if the path is different, update the path that I need to follow
                            posns = pathToFollow.vectorPath;

                        
                            enemySnake.drawTail(enemySnake.snakeLength);
                            enemySnake.savePosition();


                            GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
                            yield return new WaitForSeconds(0.5f);
                        }
                    
                    }

                    if (currentTarget != targets.Length)
                    {
                        //keep looking for a path because if we have arrived the enemy will anyway move away
                        //This code allows us to keep chasing
                        pathToFollow = seeker.StartPath(t.position, target.transform.position);
                        yield return seeker.IsDone();
                        posns = pathToFollow.vectorPath;
                        yield return null;
                    }

                    else if (currentTarget == targets.Length)
                    {
                        targetCounter++;
                        //if target limit reach go to the final target
                    
                        pathToFollow = seeker.StartPath(t.position, target.transform.position);
                        yield return seeker.IsDone();
                        posns = pathToFollow.vectorPath;
                        yield return null;
                    }
                    yield return null;
                }
                yield return null;
            }
            yield return null;
        }
    }

    //Display Path Ingame
    private void DisplayLineDestination(Transform t) 
    {
        
        Vector3[] linePosns;
        if (displayLine)
        {
            if (pathToFollow.vectorPath.Count < 2)
            {
                return;
            }
            int i = 1;

            while (i < pathToFollow.vectorPath.Count)
            {
                inGamePathLine.positionCount = pathToFollow.vectorPath.Count;
                linePosns = pathToFollow.vectorPath.ToArray();
                for (int j = 0; j < linePosns.Length; j++)
                {
                    inGamePathLine.SetPosition(j, linePosns[j]);
                }

                i++;
            }
        }
        else 
        {
            for (int i = pathToFollow.vectorPath.Count - 1; i > 0; i--)
            {
                inGamePathLine.positionCount = i;

                
            }
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("MovingObstacle"))
        {
            Destroy(this.gameObject);
        }
    }




    /*In the custom move AI script, it is first finding the target and the seeker component. With a coroutine, using the seeker component that has been found, a path is generated from the enemy to the target and is stored in a list of positions.
    The enemy is then moved along this path with a delay of 0.5f and on each move the grid is scanned to update the hitbox of the enemy.*/
}
