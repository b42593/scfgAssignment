using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodCollider : MonoBehaviour
{
    snakeGenerator mysnakegenerator;
    public bool isHittingObstacle = false;

    // Start is called before the first frame update
    void Start()
    {
        mysnakegenerator = Camera.main.GetComponent<snakeGenerator>();
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
            mysnakegenerator.snakeLength += 1;
            Destroy(this.gameObject);
        }
    }



}
