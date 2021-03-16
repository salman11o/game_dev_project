using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float speed = 6.0f;

    void LateUpdate () 
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, interpolation);

        this.transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
