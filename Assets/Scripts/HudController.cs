using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public GameObject Hud;

    public GameObject KeyHudPrefab;
    GameObject KeyHud;
    
    // Start is called before the first frame update
    void Start()
    {
        TaskController.instance.onEnterDoorArea += KeyHudAppear;
        TaskController.instance.onExitDoorArea += KeyHudDisappear;
    }

    void KeyHudAppear() 
    {
        KeyHud = Instantiate(KeyHudPrefab, Vector3.zero, Quaternion.identity);
        KeyHud.transform.SetParent(Hud.transform, false);
    }

    void KeyHudDisappear()
    {
        Destroy(KeyHud);
    }

    public void PressKeyHud()
    {
        TaskController.instance.UnlockDoor();
    }

}
