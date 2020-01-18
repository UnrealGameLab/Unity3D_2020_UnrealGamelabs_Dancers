using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_Script_OnKeyPressSounds_X : MonoBehaviour
{
    public AudioSource sound_of_key_X;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("x"))
        {
            sound_of_key_X.Play();
        }
        
    }
}
