using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenTextHandler : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] Text winText;
    [SerializeField] Text leaderboard;


    GameManager gameManager;

    float minutes;
    float seconds;

    float dataMinutes;
    float dataSeconds;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        PlayerData data =  saver.LoadPlayer();
        string name = data.name;
         float time = data.time;

        //timerText = GameObject.Find("Text");
        //winText = GameObject.Find("YouWinText");

        winText.text = "You Win " + gameManager.name;

        minutes = gameManager.completetimervalue / 60f;
        seconds = gameManager.completetimervalue % 60f;
        timerText.text = "You finished at: " + string.Format("{0:00}:{1:00}", minutes, seconds);


        dataMinutes = time / 60f;
        dataSeconds = time % 60f;

        leaderboard.text = name + " " + string.Format("{0:00}:{1:00}", dataMinutes, dataSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
