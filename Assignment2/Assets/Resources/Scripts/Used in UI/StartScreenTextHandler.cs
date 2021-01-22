using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenTextHandler : MonoBehaviour
{
    [SerializeField] InputField nameInputField;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToFirstLevel()
    {
        gameManager.playerName = nameInputField.GetComponent<InputField>().text;
        if (gameManager.playerName != "")
        {
            Debug.Log(gameManager.playerName);
            SceneManager.LoadScene("Level1");

        }
    }

}
