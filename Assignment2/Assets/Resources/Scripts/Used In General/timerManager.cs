using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timerManager : MonoBehaviour
{
    GameManager gameManager;

    public bool timerStarted;

    float timerValue=0f;

    

    public string timerFinal;
    Text timerText;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //the text component attached to THIS object
        timerText = GetComponent<Text>();
        StartCoroutine(timer());


        
    }

    IEnumerator timer()
    {
        while (true)
        {
            if (timerStarted)
            {

                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    //measure the time
                    timerValue++;

                    float minutes = timerValue / 60f;
                    float seconds = timerValue % 60f;

                    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                    gameManager.completetimervalue = timerValue;
                }

                if (SceneManager.GetActiveScene().name == "Level2")
                {
                    timerValue = gameManager.completetimervalue++;
                    //measure the time
                    timerValue++;

                    float minutes = timerValue / 60f;
                    float seconds = timerValue % 60f;

                    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                    gameManager.completetimervalue = timerValue;
                }

                if (SceneManager.GetActiveScene().name == "Level3")
                {
                    timerValue = gameManager.completetimervalue++;
                    //measure the time
                    timerValue++;

                    float minutes = timerValue / 60f;
                    float seconds = timerValue % 60f;

                    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                    gameManager.completetimervalue = timerValue;
                }

                //code that is running every second
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //don't measure the time
                timerValue = 0f;
                timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
                yield return null;

            }

        }
    }


}
