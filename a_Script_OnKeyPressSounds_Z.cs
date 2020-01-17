using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static a_Script_OnKeyPressSounds_X;
//print(a_Script_OnKeyPressSounds_X.GetType()); // TBD

public class a_Script_OnKeyPressSounds_Z : MonoBehaviour
{
    public AudioSource sound_of_key_Z;
    public AudioSource sound_of_key_K;


    // // Start is called before the first frame update
    // void Start()
    // {
    // }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("z"))
        {
            sound_of_key_Z.Play();
            
            //sound_of_key_K.mute;
            sound_of_key_K.mute = !sound_of_key_K.mute;
            // Above Code Source == https://docs.unity3d.com/ScriptReference/AudioSource-mute.html
        }

        if(Input.GetKeyDown("k"))
        {
            sound_of_key_K.Play();
            //sound_of_key_Z.mute;
            sound_of_key_Z.mute = !sound_of_key_Z.mute;
            // Above Code Source == https://docs.unity3d.com/ScriptReference/AudioSource-mute.html
            //  void OnGUI ()
            //     {
            //     // Make a background box
            //     //GUI.Box(new Rect(10,10,100,90), "K Pressed");
            //     GUI.Button(new Rect(0f,0f,80f,20f), "K Pressed");
            //     }
                    
        }




        
    }
}


