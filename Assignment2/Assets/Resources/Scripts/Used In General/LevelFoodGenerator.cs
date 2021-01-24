using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelFoodGenerator : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI objectiveText;
    
    private GameObject foodParent;
    private GameObject foodPrefab;
    private GameObject food;
    private GameObject timerUI;

    private snakeHeadController snakeController;
    private ObstacleSpawnScript movingObstacleCreator;
    

    public bool foodGenFinished = false;

    // Start is called before the first frame update
    void Start()
    {

        //Objective Text
        if (!foodGenFinished && SceneManager.GetActiveScene().name == "Level1") 
        {
            objectiveText.text = "Collect 6 food and Head to target for completion";
        }
        if (!foodGenFinished && SceneManager.GetActiveScene().name != "Level1")
        {
            objectiveText.text = "Head to target for completion";
        }

        //Timer
        timerUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Timer"), new Vector3(0f, 0f), Quaternion.identity);
        timerUI.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 100);

        //the default value for the timer is started
        timerUI.GetComponentInChildren<timerManager>().timerStarted = true;

        foodPrefab = Resources.Load<GameObject>("Prefabs/Food");

        foodParent = new GameObject("Food");
        foodParent.transform.position = new Vector3(0f, 0f);

        snakeController = GameObject.FindGameObjectWithTag("Player").GetComponent<snakeHeadController>();
        snakeController.enabled = false;

        if (SceneManager.GetActiveScene().name == "Level3") 
        {
            movingObstacleCreator = Camera.main.GetComponent<ObstacleSpawnScript>();
        }
        
        StartCoroutine(foodGeneration());



    }

    // Update is called once per frame
    void Update()
    {
        if (foodGenFinished && SceneManager.GetActiveScene().name != "Level3")
        {   
            StopCoroutine(foodGeneration());
            objectiveText.text = "Start";
            snakeController.enabled = true;
        }
        else if(foodGenFinished && SceneManager.GetActiveScene().name == "Level3")
        {
            StopCoroutine(foodGeneration());
            movingObstacleCreator.enabled = true;

            if (movingObstacleCreator.spawningFinished) 
            {
                objectiveText.text = "Start";
                snakeController.enabled = true;
            }
        }
    }

    //GenerateFood
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

                        if (SceneManager.GetActiveScene().name == "Level1") 
                        {
                            food.GetComponent<SpriteRenderer>().color = new Color32(138, 83, 129, 100);
                            food.GetComponent<SpriteRenderer>().sortingOrder = -1;
                        }
                        else if (SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3")
                        {
                            food.GetComponent<snakeDetect>().enabled = true;
                            food.GetComponent<SpriteRenderer>().color = new Color32(6, 173, 219, 100);
                            food.GetComponent<SpriteRenderer>().sortingOrder = -1;
                        }
                    }
                }
                else
                {
                    if ((Mathf.Floor(xcoord) % 4 == 0))
                    {
                        GameObject food = createFood(xcoord, ycoord);
                        food.transform.SetParent(foodParent.transform);
                        if (SceneManager.GetActiveScene().name == "Level1")
                        {
                            food.GetComponent<SpriteRenderer>().color = new Color32(138, 83, 129, 100);
                            food.GetComponent<SpriteRenderer>().sortingOrder = -1;
                        }
                        else if (SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3")
                        {
                            food.GetComponent<snakeDetect>().enabled = true;
                            food.GetComponent<SpriteRenderer>().color = new Color32(6, 173, 219, 100);
                            food.GetComponent<SpriteRenderer>().sortingOrder = -1;
                        }
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