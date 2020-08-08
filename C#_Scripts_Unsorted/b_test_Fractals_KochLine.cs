using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Own Comments own Code Changes == FOO_DHANK
// FOO_Changed_in_Video_Part_V --- 

[RequireComponent(typeof(LineRenderer))]
//public class KochLine : MonoBehaviour 
// this KochLine class doesnt derive from - MonoBehaviour , it derives from - KochGenerator
public class b_test_Fractals_KochLine : b_test_Fractals_KochGenerator
{
    LineRenderer _lineRenderer;
    
    // // FOO_DHANK  - this below wont work as LERP AMOUNT needs to OSCILLATE to create the LERP ANIMATIONS
    //public float _lerpAmount = 0.5f; // FOO_DHANK -- the small f -- suffix to convert DOUBLE to FLOAT
    [Range(0,1)]
    public float _lerpAmount;
    Vector3[] _lerpPosition;
    public float _generateMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true; // This overRides the options within the EDITOR >> INSPECTOR
        _lineRenderer.useWorldSpace = false;  // This overRides the options within the EDITOR >> INSPECTOR
        _lineRenderer.loop = true;  // This overRides the options within the EDITOR >> INSPECTOR
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);
       
    }


    // FOO_DHANKAR--- Starts Own Code --- FLASHING
    IEnumerator Flashing()
    // Flashing - is defined here as an IEnumerator.
    // Then above its called wihtin - StartCoroutine(
    {
        while(true)
        {
    
            yield return new WaitForSeconds(Random.Range(1,3));
            _lineRenderer.enabled = ! _lineRenderer.enabled;
    // _lerpAmount = Random.Range(0,1);
             // //Random new_random_value = new Random();
             // //int _lerpAmount = new_random_value.Next(0, 1);

             // //private static System.Random ran = new System.Random();
             // //private int _lerpAmount = ran.Next(0, 1);

             // print("----_lerpAmount--------");
             // print(_lerpAmount);

        }
    } // FOO_DHANKAR--- ENDS Own Code --- FLASHING



// public void changeLerp()
// {

// for ( int j = 0; j < 1; j++)
//                 {
//                     _lerpAmount = j;
//                     print("----_lerpAmount_AAAAA---");
//                     print(_lerpAmount);
//                     if(Input.GetKeyDown("q"))
//                     {
//                         _lerpAmount += 0.1f; // Increment by 0.1 
//                         print("----_lerpAmount_bbb---");
//                         print(_lerpAmount);

//                     }
//                     if(Input.GetKeyDown("a")) // Decrement  by 0.1 
//                     {
//                         _lerpAmount -= 0.1f;
//                         print("----_lerpAmount_cccc---");
//                         print(_lerpAmount);

//                     }

//                 }
// }





    // Update is called once per frame
    void Update()
    {

        for ( int j = 0; j < 1; j++)
        {
            _lerpAmount += 0.1f; // Increment by 0.1 
            print("----_lerpAmount_bbb---");
            print(_lerpAmount);

            if (6.8f <_lerpAmount )
            {
                _lerpAmount = 1.1f;
            }
        }

        

        //StartCoroutine(Flashing());
        if (_generationCount != 0)
        {
            //_lerpAmount = Random.Range(0,1); // FOO_DHANKAR- OWN CODE-- OK Works --- on each KeyDown either I(INWARDS) or O(OUTWARDS)
            // The above line assigns a RANDOM value to _lerpAmount
            
            for ( int i = 0; i < _position.Length; i++)
            {
                
                _lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], _lerpAmount);
            }
            if(_useBezierCurves)
                {
                    _bezierPosition = BezierCurve(_lerpPosition, _bezierVertexCount);
                    _lineRenderer.positionCount = _bezierPosition.Length;
                    _lineRenderer.SetPositions(_bezierPosition);
                }
            else
                {
                    _lineRenderer.positionCount = _lerpPosition.Length;
                    _lineRenderer.SetPositions(_lerpPosition);
                }
        }


        // FOO_Changed_in_Video_Part_V --- KeyBoard input KEYS commented out
        // if(Input.GetKeyUp(KeyCode.O))
        // {
        //     KochGenerate(_targetPosition, true , _generateMultiplier); // TRUE -- as we want to go OUTWARDS
        //     // Above -- _generateMultiplier -- for the FLOAT Height that the LINE SEGMENT will take as provided in the - UI Curve --->> AnimationCurve _generator
        //     _lerpPosition = new Vector3[_position.Length];
        //     _lineRenderer.positionCount = _position.Length;
        //     _lineRenderer.SetPositions(_position);
        //     //_lerpAmount =0; // FOO_DHANKAR- ORIGINAL

        //     //while(true)
        //         //{
        //             //yield return new WaitForSeconds(0.5f);
        //             // yield return new WaitForSeconds(Random.Range(1,3));
        //             // _lerpAmount = Random.Range(0,1); // FOO_DHANKAR-- OWN CODE
        //             // print("----_lerpAmount----OUTSIDE----");
        //             // print(_lerpAmount);
        //         //} // ENDS --- While-TRUE    
        // }



        //  if(Input.GetKeyUp(KeyCode.I))
        //  //print("=== PRESSED --- P ");
        // {
        //     KochGenerate(_targetPosition, false , _generateMultiplier); // FALSE -- as we want to go INWARDS
        //     _lerpPosition = new Vector3[_position.Length];
        //     _lineRenderer.positionCount = _position.Length;
        //     _lineRenderer.SetPositions(_position);
        //     //_lerpAmount =0; // // FOO_DHANKAR- ORIGINAL
        //     //_lerpAmount = Random.Range(0,1); // FOO_DHANKAR- OWN CODE
        //     // print("----_lerpAmount----INSIDE----");
        //     // print(_lerpAmount);

        // }
    }
}
