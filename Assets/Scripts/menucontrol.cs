using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void startGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public static void endGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
