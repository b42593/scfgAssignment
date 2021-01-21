using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class foodCollider : MonoBehaviour
{
    GameManager gameManager;
    spawnEnemy enemySpawner;
    public bool isHittingObstacle = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3") 
        {
            enemySpawner = Camera.main.GetComponent<spawnEnemy>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) 
        {
            isHittingObstacle = true;
            Destroy(this.gameObject);
            isHittingObstacle = false;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.friendlySnakeLength += 1;
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemySpawner.snakeLength += 1;
            Destroy(this.gameObject);
        }
    }



}
