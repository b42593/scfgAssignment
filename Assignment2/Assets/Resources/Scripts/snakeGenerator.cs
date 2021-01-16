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


    GameObject playerBox, breadcrumbBox, pathParent, timerUI;

    List<positionRecord> pastPositions;

    int positionorder = 0;

    bool firstrun = true;

    // Start is called before the first frame update
    void Start()
    {
        playerBox = Instantiate(Resources.Load<GameObject>("Prefabs/SnakeHead"), new Vector3(0f, 0f), Quaternion.identity);

        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";


        breadcrumbBox = Resources.Load<GameObject>("Prefabs/SnakeBody");

        playerBox.GetComponent<SpriteRenderer>().color = Color.black;

        //move the box with the arrow keys
        playerBox.AddComponent<snakeheadController>();

        playerBox.name = "Black player box";

        pastPositions = new List<positionRecord>();

        drawTail(snakeLength);
    }


    void Update()
    {
        if (Input.anyKeyDown && !((Input.GetMouseButtonDown(0)
            || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) && !Input.GetKeyDown(KeyCode.X) && !Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("a key was pressed " + Time.time);

            savePosition();

            //draw a tail of length
            drawTail(snakeLength);
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


    void savePosition()
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

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);

    }


    void drawTail(int length)
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
                pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.green;

            }

        }

        if (firstrun)
        {

            
            for (int count = length; count > 0; count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
                float ycoord = count * -1;
                fakeBoxPos.Position = new Vector3(0f, ycoord);
                
                pastPositions.Add(fakeBoxPos);




            }
            firstrun = false;
            drawTail(length);
            
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
