using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAplication : MonoBehaviour
{

    private int? a;

    // Update is called once per frame

    private void Start()
    {
        Debug.Log(a);
    }
    void Update()
    {
        if (Input.GetKey("escape")) 
        {
            Application.Quit();
        }

        
    }
}
