using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    [SerializeField] bool isHorizontal = false;

    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        x = this.gameObject.transform.localScale.x;
        y = this.gameObject.transform.localScale.y;



        if (x != 1 && y == 1)
        {
            isHorizontal = true;
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1f, 0.9f);
        }
        else if (x == 1 && y != 1)
        {
            isHorizontal = false;
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 1f);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
