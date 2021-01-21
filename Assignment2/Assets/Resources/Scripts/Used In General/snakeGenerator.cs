using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class positionRecord
{
    //the place where I've been
    Vector3 position;
    //at which point was I there?
    int positionOrder;



    GameObject breadcrumbBox;

    public void changeColor()
    {
        this.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.black;
    }


    

    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
}


public class snakeGenerator : MonoBehaviour
{

    public int snakeLength;

    [Header ("Snake Spawn Position")]
    [SerializeField] float spawnX;
    [SerializeField] float spawnY;

    GameObject playerBox, breadcrumbBox, pathParent, snake;

    List<positionRecord> pastPositions;

    int positionorder = 0;

    bool firstrun = true;

    // Start is called before the first frame update
    void Start()
    {

        


        snake = new GameObject("Snake");
        snake.transform.position = new Vector3(0f, 0f);

        playerBox = Instantiate(Resources.Load<GameObject>("Prefabs/PlayerSnake/SnakeHead"), new Vector3(spawnX, spawnY), Quaternion.identity);

        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";


        breadcrumbBox = Resources.Load<GameObject>("Prefabs/PlayerSnake/SnakeBody");

        playerBox.GetComponent<SpriteRenderer>().color = Color.green;

        //move the snakehead automatically with pathing
        playerBox.AddComponent<snakeHeadController>();
        playerBox.GetComponent<snakeHeadController>().enabled = false;

        playerBox.name = "Player";

        playerBox.transform.SetParent(snake.transform);

        pastPositions = new List<positionRecord>();


        //draw a tail of length
        drawTail(snakeLength);
    }


    void Update()
    {
        if (playerBox.GetComponent<snakeHeadController>().enabled) 
        {
            if (Input.anyKeyDown && !((Input.GetMouseButtonDown(0)
                || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) && !Input.GetKeyDown(KeyCode.X) && !Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.Space))
            {
           
                savePosition();

                //draw a tail of length
                drawTail(snakeLength);

            }
        }

    }


    // Update is called once per frame

    bool boxExists(Vector3 positionToCheck)
    {
        //foreach position in the list

        foreach (positionRecord p in pastPositions)
        {

            if (p.Position == positionToCheck)
            {
                Debug.Log(p.Position + "Actually was a past position");
                if (p.BreadcrumbBox != null)
                {
                    Debug.Log(p.Position + "Actually has a red box already");
                    //this breaks the foreach so I don't need to keep checking
                    return true;
                }
            }
        }

        return false;
    }


    public void savePosition()
    {
        positionRecord currentBoxPos = new positionRecord();

        currentBoxPos.Position = playerBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(playerBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, playerBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 0);

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);

    }


    public void drawTail(int length)
    {
        clearTail();

        if (pastPositions.Count > length)
        {
            //nope
            //I do have enough positions in the past positions list
            //the first block behind the player
            int tailStartIndex = pastPositions.Count - 1;
            int tailEndIndex = tailStartIndex - length;


            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {

                Debug.Log(snakeblocks);

                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity);
                pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = new Color32(53, 144, 69 , 100);
                pastPositions[snakeblocks].BreadcrumbBox.transform.SetParent(snake.transform);


            }

        }

        if (firstrun)
        {
            int curLength = 0;

            for (curLength = 0; curLength < length; curLength++)
            {
                for (int count = curLength; count < length; count++)
                {
                    positionRecord fakeBoxPos = new positionRecord();
                    //float ycoord = count * -1;
                    // ycoord = spawnY - ycoord;
                    float ycoord = spawnY * -1;

                    fakeBoxPos.Position = new Vector3(spawnX, ycoord);

                    pastPositions.Add(fakeBoxPos);
                }
                drawTail(curLength);
            }
            


            firstrun = false;
            


        }

    }
    void clearTail()
    {
        foreach (positionRecord p in pastPositions)
        {
            
            Destroy(p.BreadcrumbBox);
        }
    }
   
}
