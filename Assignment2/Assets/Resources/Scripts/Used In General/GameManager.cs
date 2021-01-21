using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public float completetimervalue = 0f;


    public int friendlySnakeLength = 0;

    public static GameManager Instance { get; private set; }

    
    public string name;

    

    

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);



    }
     

}
