using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FOO_Changed_in_Video_Part_V -- Changes done in Fifth Video 
// https://www.youtube.com/watch?v=btlgbfIluAE
// Koch Fractals - Unity/C# Tutorial [Part 5 - Lerping on Audio Frequencies]
//


[RequireComponent(typeof(LineRenderer))]
//public class KochLine : MonoBehaviour 
// this KochLine class doesnt derive from - MonoBehaviour , it derives from - KochGenerator
public class c_Kochline : c_Kochgenerator
{
    LineRenderer _lineRenderer;
    //[Range(0,1)]
    //public float _lerpAmount; // Commented Out in Video 5 --- i commented out earlier to test my Lerping 
    // FOO_Changed_in_Video_Part_V --
    
    Vector3[] _lerpPosition;
    public float _generateMultiplier;

    [Header ("Audio_Dhankar")]
    public c_AudioPeer _audioPeer; // FART --- name is that of my own C# script 
    public int _audioBand;


    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true; // This overRides the options within the EDITOR >> INSPECTOR
        _lineRenderer.useWorldSpace = false;  // This overRides the options within the EDITOR >> INSPECTOR
        _lineRenderer.loop = true;  // This overRides the options within the EDITOR >> INSPECTOR
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);
        //FART Uncomment below -- 
        // FOO_Changed_in_Video_Part_V
        _lerpPosition = new Vector3[_position.Length]; //FOO_Changed_in_Video_Part_V -- _lerpPosition -- added to START 
    }

    
    // FART --- Uncomment ==void Update()- Commented out 01-feb-20 --for own EXPERI without the LerpAmount == _lerpAmount
    // Update is called once per frame
    void Update()
    {
        if (_generationCount != 0)
        {
            for ( int i = 0; i < _position.Length; i++)
            {
                //_lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], _lerpAmount);
                _lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], c_AudioPeer._audioBandBuffer[_audioBand]); // So which buffer do we want -- 
                // We want the buffer corresponding to the -== _audioBand --->> _audioBandBuffer[_audioBand]
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
        //     _lerpAmount =0;
        // }
        //  if(Input.GetKeyUp(KeyCode.I))
        //  //print("=== PRESSED --- P ");
        // {
        //     KochGenerate(_targetPosition, false , _generateMultiplier); // FALSE -- as we want to go INWARDS
        //     _lerpPosition = new Vector3[_position.Length];
        //     _lineRenderer.positionCount = _position.Length;
        //     _lineRenderer.SetPositions(_position);
        //     _lerpAmount =0;
        // }
    
    //}




}
}

