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

    [SerializeField] InputField nameInputField;
    public string name;

    /*
        [SerializeField] Canvas TimerCanvas;
        [SerializeField] Text timerText;
        public float timer = 0;
    */



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);



    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }


  

    public void goToFirstLevel()
    {
        name = nameInputField.text;
        if (name != "")
        {
            Debug.Log(name);
            SceneManager.LoadScene("Level1");
            
        }
    }

    public void goBackToMainMenu()
    {
        
         SceneManager.LoadScene("StartScene");
        
    }
    

}
