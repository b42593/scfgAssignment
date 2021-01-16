using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class eventsController : MonoBehaviour
{
    [SerializeField] InputField nameInputField;
    string name;


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

}
