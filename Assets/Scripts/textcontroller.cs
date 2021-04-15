using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textcontroller : MonoBehaviour
{

    public TextMeshProUGUI tutorial;

    // Start is called before the first frame update
    void Start()
    {
        tutorial.GetComponent<TextMeshProUGUI>();
        tutorial.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function for other scripts to call to change the text
    public void tutorialMessage (string text)
    {
        tutorial.GetComponent<TextMeshProUGUI>();
        tutorial.text = text;
    }

}
