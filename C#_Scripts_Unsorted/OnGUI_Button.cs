using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGUI_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnGUI ()
    {
        if ( Input.GetKey( KeyCode.Escape ) )
        {
            GUILayout.BeginArea(new Rect(10, 10, 100, 100));
            GUILayout.Button("Click me");
            Debug.Log("FART----Space key is pressed.");
            GUILayout.EndArea();
        }
    }



    //  void OnGUI()
    // {
    //     if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Space.ToString())))
    //     {
    //         Debug.Log("Space key is pressed.");
    //         GUI.Button(new Rect(10f,10f,80f,20f), "K Pressed");
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
    //     if(Input.GetKeyDown("k"))
    //     {
    //         Debug.Log("YES----BUTTON---Within IF ");
    //  void OnGUI ()
    //             {

    //                 Debug.Log("YES----BUTTON---Within OnGUI ");
        
    //         // Make a background box
    //             //GUI.Box(new Rect(10,10,100,90), "K Pressed");
    //             GUI.Button(new Rect(0f,0f,800f,200f), "K Pressed");
    //             Debug.Log("YES----BUTTON");
    
    //             }                    
    //     }

    }


            
}
