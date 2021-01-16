using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodCollider : MonoBehaviour
{

    public bool isHittingObstacle = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) 
        {
            isHittingObstacle = true;
            Destroy(this.gameObject);
            isHittingObstacle = false;
        }
    }



}
