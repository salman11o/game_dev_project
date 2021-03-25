using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{

    public static TaskController instance;

    public event Action onClickEngine;
    public event Action onClickDoor;
    public event Action onClickScreen;


    void ClickEngine()
    {
        if (onClickEngine != null)
        {
            onClickEngine();
        }

    }

    void ClickDoor()
    {
        if (onClickDoor != null)
        {
            onClickDoor();
        }

    }

    void ClickScreen()
    {
        if (onClickScreen != null)
        {
            onClickScreen();
        }

    }

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
