using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class AudioViz_Lights : MonoBehaviour {
	public int _band;
	public float _minIntensity, _maxIntensity;
	Light _light;

	// Use this for initialization
	void Start () {
		_light = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
    //     if(_light.intensity < (AudioPeer._audioBandBuffer[_band] * (_maxIntensity - _minIntensity)) + _minIntensity)
    //     {
    //         _light.intensity += .1f;
    //     }
    //     else if (_light.intensity > (AudioPeer._audioBandBuffer[_band] * (_maxIntensity - _minIntensity)) + _minIntensity)
    //     {
    //         _light.intensity -= .1f;
    //     }

    // }
           _light.intensity = (c_AudioPeer._audioBandBuffer [_band] * (_maxIntensity - _minIntensity)) + _minIntensity;
}
}
