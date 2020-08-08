// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger_script : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        if(Input.GetKeyDown("l")) // L - for Changing Levels 
        {
            //FadeToLevel(1); // Defined below .. Upgraded to == public void FadeToNextLevel()
            FadeToNextLevel(); // Now here - called >> FadeToNextLevel() -- without passing any PARAMS - as none is required
            //print("GOT LEFT MOUSE  ---"); // OK
            print("GOT Key = L -- Changing to Next Level ---"); // OK
        }
    }

// Using the below --- public void FadeToNextLevel() --- We can fade to the Next Level from any SCENE - 
// Dont Need to give the INT Index of Current or Next Scene 
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex +1);
        // FadeToLevel - defined below 
    }

    //public void FadeToLevel (string NameOfLevel) // STring Variable with Name of the LEVEL / SCENE
    public void FadeToLevel (int levelIndex) // INT Index = Variable with INDEX of the LEVEL / SCENE
    {
        levelToLoad = levelIndex; // The - levelIndex -- passed in a s an INT PARAM Above
        animator.SetTrigger("Trigger_FadeOut");
    }

    public void OnfadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
