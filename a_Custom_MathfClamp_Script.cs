using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FOO -- Mathf.Clamp -- is CLAMP on Mathf - Math Float Values 

public class a_Custom_MathfClamp_Script : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    // }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,8f,9f),transform.position.z);
        // FOO - Clamp the Y Coordinate - between 8 and 9 Float 
        // Dont CLAMP X and Z Coordinates 

    }
}
