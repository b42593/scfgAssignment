using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using UnityEngine.SceneManagement;

public class snakeHeadController : MonoBehaviour
{
    GameManager gameManager;
    snakeGenerator mysnakegenerator;

    public int tailsize;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mysnakegenerator = Camera.main.GetComponent<snakeGenerator>();
       
        tailsize = gameManager.friendlySnakeLength;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(1f, 0);
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
        }

        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
            SceneManager.LoadScene("Death");
        }

        if (col.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Death");
        }
        if (col.gameObject.CompareTag("MovingObstacle"))
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Death");
        }
    }





    /*In the custom move AI script, it is first finding the target and the seeker component. With a coroutine, using the seeker component that has been found, a path is generated from the enemy to the target and is stored in a list of positions.
    The enemy is then moved along this path with a delay of 0.5f and on each move the grid is scanned to update the hitbox of the enemy.*/


}


