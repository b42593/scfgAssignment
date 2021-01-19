using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1FoodGenerator : MonoBehaviour
{
    //private GameObject waypointParent;
    private GameObject foodParent;
    private GameObject foodPrefab;
    private GameObject food;

    private snakeHeadController snakeController;
    

    public bool foodGenFinished = false;

    // Start is called before the first frame update
    void Start()
    {

        foodPrefab = Resources.Load<GameObject>("Prefabs/Food");

        foodParent = new GameObject("Food");
        foodParent.transform.position = new Vector3(0f, 0f);

        snakeController = GameObject.FindGameObjectWithTag("Player").GetComponent<snakeHeadController>();
        


        StartCoroutine(foodGeneration());



    }

    // Update is called once per frame
    void Update()
    {
        if (foodGenFinished)
        {
            StopCoroutine(foodGeneration());
            snakeController.enabled = true;
        }
    }

    GameObject createFood(float xpos, float ypos)
    {
        return Instantiate(foodPrefab, new Vector3(xpos, ypos), Quaternion.identity);
    }

    IEnumerator foodGeneration()
    {
        bool alternatey = false;
        for (float ycoord = -9.5f; ycoord <= 9.5f; ycoord += 4)
        {
            //for each row
            for (float xcoord = -9.5f; xcoord <= 9.5f; xcoord++)
            {           
                   
                
                if (alternatey)
                {
                    if ((Mathf.Floor(xcoord) % 4 == 0))
                    {
                        food = createFood(xcoord, ycoord);
                        food.transform.SetParent(foodParent.transform);
                        food.GetComponent<SpriteRenderer>().color = new Color32(138, 83, 129, 100);
                        food.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    }
                    

                }
                else
                {
                    if ((Mathf.Floor(xcoord) % 4 == 0))
                    {
                        GameObject food = createFood(xcoord, ycoord);
                        food.transform.SetParent(foodParent.transform);
                        food.GetComponent<SpriteRenderer>().color = new Color32(138, 83, 129, 100);
                        food.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    }
                }

                
                yield return new WaitForSeconds(0.1f);
            }

            alternatey = !alternatey;
            yield return new WaitForSeconds(0.1f);
        }
        foodGenFinished = true;
        
        yield return null;
    }
}