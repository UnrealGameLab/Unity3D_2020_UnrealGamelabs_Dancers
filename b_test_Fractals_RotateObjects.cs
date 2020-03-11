using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_test_Fractals_RotateObjects : MonoBehaviour
{
    //1 
    public float rotationAmount;
    //public AudioSource sound_of_key_Z; // FOO - for Sounds 
    LineRenderer _lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(Flashing());

        // _lineRenderer.enabled = true; // This overRides the options within the EDITOR >> INSPECTOR
        // _lineRenderer.useWorldSpace = false;  // This overRides the options within the EDITOR >> INSPECTOR
        // _lineRenderer.loop = true;  // This overRides the options within the EDITOR >> INSPECTOR
        // //_lineRenderer.positionCount = _position.Length;
        //_lineRenderer.SetPositions(_position);
        
    }

    // Starts --- FLASHING
    IEnumerator Flashing()
    // Flashing - is defined here as an IEnumerator.
    // Then above its called wihtin - StartCoroutine(
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.00f,0.007f));
            _lineRenderer.enabled = ! _lineRenderer.enabled;
            //yield return new WaitForSeconds(Random.Range(0.15f,0.07f));
            //_lineRenderer.enabled = _lineRenderer.enabled;
        }
    }




    // Update is called once per frame
    void Update()
    {

    if(Input.GetKeyDown("z"))
        {
            //sound_of_key_Z.Play();
            
            
            //sound_of_key_K.mute = !sound_of_key_K.mute;
            // Above Code Source == https://docs.unity3d.com/ScriptReference/AudioSource-mute.html
        }
        transform.Rotate(0,rotationAmount * Time.deltaTime,0);
    }
}
