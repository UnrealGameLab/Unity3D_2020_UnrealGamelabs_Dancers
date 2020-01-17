using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGUI_Script : MonoBehaviour
{

    public string GUI_TextValue = " "; //Empty String variable
    private bool drawGui = false; //Control for the GUI group layout
    //if ( Input.GetKeyDown( KeyCode.Escape ) )
    
void Update()
    {
            if(Input.GetKeyDown("z"))    
            {
                drawGui = true; 
                GUI_TextValue = "User Pressed PC-Keyboard Key = Z . Triggers Custom Animation for ARISSA only and changes Audio Track";
            }    

            if(Input.GetKeyDown("x"))    
            {
                drawGui = true; 
                GUI_TextValue = "User has Pressed the PC-Keyboard Key = X ";
            }

            if(Input.GetKeyDown("c"))    
            {
                drawGui = true; 
                GUI_TextValue = "User Pressed Key = C ";
            }

            if(Input.GetKeyDown("v"))    
            {
                drawGui = true; 
                GUI_TextValue = "User Pressed Key = V ";
            }    

            if(Input.GetKeyDown("b"))    
            {
                drawGui = true; 
                GUI_TextValue = "User Pressed Key = B ";
            }

            if(Input.GetKeyDown("n"))    
            {
                drawGui = true; 
                GUI_TextValue = "User Pressed Key = N ";
            }


            if(Input.GetKeyDown("k"))    
            {
                drawGui = true; 
                GUI_TextValue = "User Pressed Key = K ";
            }


    }

    void OnGUI()
    {
        if(drawGui == true)
         {
            GUILayout.BeginArea(new Rect(30, 30, 900, 500));
            GUILayout.Button(GUI_TextValue);
            GUILayout.Label("GUI-TextValue");
            GUILayout.EndArea();
        }
    }

}



    // BELOW OLD CODE 
    // void OnGUI ()
    // {
    //     if ( Input.GetKey( KeyCode.Escape ) )
    //     {
    //         GUILayout.BeginArea(new Rect(0f, 7f, 100, 100));
    //         GUILayout.Button("Click me");
    //         Debug.Log("FART----Space key is pressed.");
    //         GUILayout.EndArea();
    //     }
    // }




    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

