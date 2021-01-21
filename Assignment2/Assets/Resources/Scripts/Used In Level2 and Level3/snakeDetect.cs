using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeDetect : MonoBehaviour
{
    spawnEnemy enemySnake;

    [Header("Set Timer to Spawn Snake. Leave Timer at 0 !")]
    [SerializeField] float timerMax;
    [SerializeField] float timer;

    public float xPos;
    public float yPos;

    public bool timeToSpawn = false;
    public bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        enemySnake = Camera.main.GetComponent<spawnEnemy>();

        xPos = this.gameObject.transform.position.x;
        yPos = this.gameObject.transform.position.y;

        timer = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 10;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 30f, Color.red);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 30f, layerMask);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 30f, Color.red);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 30f, layerMask);


        if (hitUp)
        {
            Debug.Log("Hit: " + hitUp.collider.name);       
            triggered = true;
        }

        if ( hitLeft)
        {
            Debug.Log("Hit: " + hitLeft.collider.name);
            triggered = true;
        }


        if (triggered)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timeToSpawn = true;
                timer = 0;
                enemySnake.enabled = true;
                triggered = false;
            }

            

        }

    }
    
}
