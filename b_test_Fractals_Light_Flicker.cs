// C# Script Source -- YouTube == https://www.youtube.com/watch?v=WRVETgdB-qw

using System.Collections;
// FOO -- here above the Collections includes -->> IEnumerator
// IEnumerator -- Supports a simple iteration over a non-generic collection.

using System.Collections.Generic;
using UnityEngine;

public class a_test_Fractals_Light_Flicker : MonoBehaviour
{
    Light testLight;
    public float minWaitTime; 
    // FOO - by making these Public here - we ensure we get Text Boxes to 
    // input user defined -  values on the Editor GUI. 
    public float maxWaitTime;


    // Start is called before the first frame update
    void Start()
    {
        testLight = GetComponent<Light>();
        StartCoroutine(Flashing());
        
    }

    IEnumerator Flashing()
    // Flashing - is defined here as an IEnumerator.
    // Then above its called wihtin - StartCoroutine(
    {
        while(true)
        {
            //yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime));
            testLight.enabled = ! testLight.enabled;
        }
    }
}
