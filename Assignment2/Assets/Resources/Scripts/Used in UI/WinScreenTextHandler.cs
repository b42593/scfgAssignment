using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScores 
{
    public string playerN;
    public float timeN;
}



public class WinScreenTextHandler : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] Text winText;
    [SerializeField] Text leaderboard1;
    [SerializeField] Text leaderboard2;
    [SerializeField] Text leaderboard3;

    PlayerScores p;

    GameManager gameManager;

    float minutes;
    float seconds;

    float dataMinutes;
    float dataSeconds;

    public string[] names;
    public float[] times;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        names =  PlayerPrefsX.GetStringArray("names", "" , 1);
        times =  PlayerPrefsX.GetFloatArray("times", 0f , 1);

        if (names[0] == "")
        {
            names[0] = gameManager.playerName;
            times[0] = gameManager.completetimervalue;

            PlayerPrefsX.SetStringArray("names", names);
            PlayerPrefsX.SetFloatArray("times", times);

            winText.text = "You Win " + gameManager.playerName; ;
            

            timerText.text = "You finished at: " + gameManager.completetimervalue;


            leaderboard1.text = names[0] + " " + times[0];
            leaderboard2.text = "No Users yet entered";
            leaderboard3.text = "No Users yet entered";

        }
        else 
        {
            p = new PlayerScores();

            List<PlayerScores> playerScore = new List<PlayerScores>();

            for (int x = 0; x < names.Length; x++) 
            {
                p = new PlayerScores();
                p.playerN = names[x];
                p.timeN = times[x];

                playerScore.Add(p);
                playerScore.Sort((p1, p2) => p1.timeN.CompareTo(p2.timeN));
            }

            p = new PlayerScores();
            p.playerN = gameManager.playerName;
            p.timeN = gameManager.completetimervalue;

            playerScore.Add(p);

            System.Array.Resize(ref names, names.Length + 1);
            System.Array.Resize(ref times, times.Length + 1);

            names[names.Length - 1] = gameManager.playerName;
            times[times.Length - 1] = gameManager.completetimervalue;

            PlayerPrefsX.SetStringArray("names", names);
            PlayerPrefsX.SetFloatArray("times", times);

            winText.text = "You Win " + p.playerN;

            timerText.text = "You finished at: " + p.timeN;
            

            if (names.Length <= 2)
            {
                playerScore.Sort((p1, p2) => p1.timeN.CompareTo(p2.timeN));

                leaderboard1.text = playerScore[0].playerN + " " + playerScore[0].timeN;
                leaderboard2.text = playerScore[1].playerN + " " + playerScore[1].timeN;
                leaderboard3.text = "No Users yet entered";
            }

            if (names.Length > 2)
            {
                playerScore.Sort((p1, p2) => p1.timeN.CompareTo(p2.timeN));

                leaderboard1.text = playerScore[0].playerN + " " + playerScore[0].timeN;
                leaderboard2.text = playerScore[1].playerN + " " + playerScore[1].timeN;
                leaderboard3.text = playerScore[2].playerN + " " + playerScore[2].timeN;
            }

            
            
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }
}
