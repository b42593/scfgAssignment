using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    [SerializeField] bool isHorizontal = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isHorizontal)
        {
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 1f);
        }
        else 
        {
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1f, 0.9f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
