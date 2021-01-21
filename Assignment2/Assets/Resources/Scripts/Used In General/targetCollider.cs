using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class targetCollider : MonoBehaviour
{
    GameManager gameManager;
    snakeGenerator mysnakegenerator;
    timerManager timer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            mysnakegenerator = Camera.main.GetComponent<snakeGenerator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Level1" && gameManager.friendlySnakeLength >= 6) 
            {
                SceneManager.LoadScene("Level2");
            }

            if (SceneManager.GetActiveScene().name == "Level2")
            {
                SceneManager.LoadScene("Level3");
            }

            if (SceneManager.GetActiveScene().name == "Level3")
            {
                saver.SavePlayer(gameManager);
                SceneManager.LoadScene("Win");
                gameManager.friendlySnakeLength = 0;
            }
        }

        
    }

}
