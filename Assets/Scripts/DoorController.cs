using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DoorController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        TaskController.instance.onUnlockDoor += Unlock;
    }

    void Unlock() 
    {
        Debug.Log("unlocking");
        this.GetComponent<Tilemap>().ClearAllTiles();
    }
}
