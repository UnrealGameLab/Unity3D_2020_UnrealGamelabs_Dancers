using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script Source == https://www.youtube.com/watch?v=kzHNUT9q4JE
public class a_test_Fractals_Laser_1 : MonoBehaviour
{

    private LineRenderer laser_var;

    // Start is called before the first frame update
    // FART --- Uncomment below -- this is OK 
    // void Start()
    // {
    //     laser_var = GetComponent<LineRenderer>();
    // }

    // Update is called once per frame
    // FART --- Uncomment below -- this is OK 
    // void Update()
    // {
    //     laser_var.SetPosition(0,transform.position);
    //     RaycastHit hit; // FOO - Create a variable of Name = hit of Type = RaycastHit // RaycastHit --- SMALL C 
    //     if(Physics.Raycast(transform.position,transform.forward, out hit))
    //     {
    //         if(hit.collider)
    //         {
    //             laser_var.SetPosition(1,hit.point);
    //         }
    //     }
    //     //else laser_var.SetPosition(1,transform.forward*5000);
    //     //transform.position
    //     else laser_var.SetPosition(1,transform.position + (transform.forward*10000000));

    // }
}        

