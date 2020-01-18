using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MAIN Code Source == https://www.youtube.com/watch?v=wdOk5QXYC6Y&t=931s
// MAIN Code Source == Own JIRA --- within SHEET == 
// MAIN Code Source == ANIMATION MAIN --- Like Unity CHAN 

// FOO -- This script Animates --- Arissa_Macarena
public class a_Animate_Arissa : MonoBehaviour

    {
        // 1
        public Animator anim_arissa;

        // FOO_Dhank -- Doesnt Work -- https://docs.unity3d.com/Manual/gui-Layout.html
        // string str_Arissa_FloorDance = "User has Hit Key = C .Triggered Animation = Arissa_FloorDance - MIDWAY";
        //public static System.String str_Arissa_FloorDance = "User has Hit Key = C .Triggered Animation = Arissa_FloorDance - MIDWAY";

        // Testing OnGUI -- Within Arissa Animation Script 
        public string GUI_TextValue = " "; //Empty String variable
        private bool drawGui = false; //Control for the GUI group layout
        

        // Start is called before the first frame update
        void Start()
        {
            anim_arissa = GetComponent<Animator>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown("z"))
            {
                print("Hit_Key= Z, RANDOMIZED= Arissa_Macarena /Arissa_FloorDance ");
                int randInt = Random.Range(0,3); // if Range(0,3) Range between - 0,1,2 - Both 0 and 2 Included but 3 EXCLUDED
                //print("The randInt is ------" +randInt); // OK

                if (randInt == 0)
                {
                    anim_arissa.Play("Arissa_Macarena",-1,0f);
                    //print("RANDOMIZED Arissa_Macarena - Begin at START - randInt is -"+ randInt); //OK Not required
                    drawGui = true; 
                    GUI_TextValue = "RANDOMIZED_ANIMATION Arissa_Macarena - Begins at START ";
                }

                else if (randInt == 1)
                {
                    anim_arissa.Play("Arissa_Macarena",-1,0.5f);
                    //print("RANDOMIZED Arissa_Macarena - Begin at MID - randInt is -"+ randInt); //OK Not required
                    drawGui = true; 
                    GUI_TextValue = "RANDOMIZED_ANIMATION Arissa_Macarena - Begins at MID ";
                }

                //else (randInt == 2) // Not correct 
                //else
                else if (randInt == 2)
                {
                    anim_arissa.Play("Arissa_Macarena",-1,1f);
                    //print("RANDOMIZED Arissa_Macarena - Begin at END - randInt is -"+ randInt); //OK Not required
                    drawGui = true; 
                    GUI_TextValue = "RANDOMIZED_ANIMATION Arissa_Macarena - Begins at END ";
                }
                
                
                // here above the PARAM == -1 means the - BASE LAYER in the Unity Editor --- 
                // here above == 0f , is the START of the ANIMATION
                // here above == 0.5f , is the MIDDLE of the ANIMATION
                // here above == 1f , is the END of the ANIMATION
            }

            if(Input.GetKeyDown("x"))
            {
                print("Hit Key === X ... starting == Arissa_FloorDance");
                anim_arissa.Play("Arissa_FloorDance",-1,0f);
                // here above the PARAM == -1 means the - BASE LAYER in the Unity Editor --- 
                // here above == 0f , is the START of the ANIMATION
                // here above == 1f , is the END of the ANIMATION
                drawGui = true; 
                GUI_TextValue = "ANIMATION Starting = Arissa_FloorDance ";
            }

            if(Input.GetKeyDown("c"))
            {
                print("Hit Key === C ... starting == Arissa_FloorDance -- MIDWAY");
                anim_arissa.Play("Arissa_FloorDance",-1,0.5f);
                // here above the PARAM == -1 means the - BASE LAYER in the Unity Editor --- 
                // here above == 0.5f , is the MIDDLE of the ANIMATION
                drawGui = true; 
                GUI_TextValue = "User Hit Key= C. ANIMATION Starting = Arissa_FloorDance _ MIDWAY ";
            }
            // Arissa_DrunkRunFwd
            if(Input.GetKeyDown("v"))
            {
                print("User has Hit Key = V .Triggered Animation = Arissa_DrunkRunFwd");
                anim_arissa.Play("Arissa_DrunkRunFwd",-1,0f);
                // here above the PARAM == -1 means the - BASE LAYER in the Unity Editor --- 
                drawGui = true; 
                GUI_TextValue = "User Hit Key= V. ANIMATION Starting = Arissa_FwdRun ";
            }
            //Arissa_Samba
            if(Input.GetKeyDown("b"))
            {
                print("User has Hit Key = B .Triggered Animation = Arissa_Samba");
                anim_arissa.Play("Arissa_Samba",-1,0f);
                drawGui = true; 
                GUI_TextValue = "User Hit Key= B. ANIMATION Starting = Arissa_Samba ";
            }
            //Arissa_HipHop
            if(Input.GetKeyDown("n"))
            {
                print("User has Hit Key = N .Triggered Animation = Arissa_HipHop");
                anim_arissa.Play("Arissa_HipHop",-1,0f);
                drawGui = true; 
                GUI_TextValue = "User Hit Key= N. ANIMATION Starting = Arissa_HipHop ";
            }

            if(Input.GetMouseButtonDown(0))
            // here - GetMouseButtonDown(0) -- ZERO is Left Mouse Button
            {
                anim_arissa.Play("Arissa_Macarena",-1,0.75f);
                // here above the PARAM == -1 means the - BASE LAYER in the Unity Editor --- 
                // here above == 0.75f , is the MIDDLE of the ANIMATION

                    //  void OnGUI () 
                    //  {
                        //GUILayout.Label(new Rect (0,0,100,50),str_Arissa_FloorDance);
                        //GUI.Label (new Rect (0,0,100,50), theDebugLog);
                     //}
                
            }

        }// ENDS --- void Update()

            void OnGUI()
                {
                    if(drawGui == true)
                    {
                        GUILayout.BeginArea(new Rect(60, 500, 500, 50));
                        GUI.contentColor = Color.red; // FOO - Check
                        GUILayout.Button(GUI_TextValue);
                        GUILayout.Label("-#--ANIMATION ALERT--#-");
                        GUILayout.EndArea();
                    }
                } // ENDS -- void OnGUI()

}
