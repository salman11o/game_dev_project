using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal; 

public class LightsController : MonoBehaviour
{

    Light2D light2d;

    // Called every few seconds based on a random timer in Update()
    IEnumerator Flicker() {

        for (;;)
        {
            int wait_time = Random.Range(5,12);
            float flicker_time = Random.Range(0.3f, 0.5f);

            for (int i = 0; i < 10; i++) {
                this.light2d.intensity = 0.1f;
                yield return new WaitForSeconds(flicker_time);

                this.light2d.intensity = 0.6f;
                yield return new WaitForSeconds(flicker_time);

            }
            
            yield return new WaitForSeconds(wait_time);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flicker());
        this.light2d = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
