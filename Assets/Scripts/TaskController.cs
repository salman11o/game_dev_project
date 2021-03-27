using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{

    public static TaskController instance;

    public event Action onClickEngine;
    public event Action onClickScreen;

    public event Action onEnterDoorArea;
    public event Action onExitDoorArea;

    public event Action onUnlockDoor;

    public void ExitDoorArea()
    {
        if (onEnterDoorArea != null)
        {
            onExitDoorArea();
        }
    }

    public void EnterDoorArea()
    {
        if (onEnterDoorArea != null)
        {
            onEnterDoorArea();
        }
    }

    public void ClickEngine()
    {
        if (onClickEngine != null)
        {
            onClickEngine();
        }

    }

    public void UnlockDoor()
    {
        if (onUnlockDoor != null)
        {
            onUnlockDoor();
        }

    }

    public void ClickScreen()
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
