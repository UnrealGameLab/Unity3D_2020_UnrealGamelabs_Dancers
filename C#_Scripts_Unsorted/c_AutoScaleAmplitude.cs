using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_AutoScaleAmplitude : MonoBehaviour
{

    //public int _band_1; // Not required 

    public float _startScale, _maxScale;
    public bool _useBuffer;
    Material _material;
    public float _red, _green, _blue; // FOO_Not used -- cant get Emissions Working 
    // Use this for initialization
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!_useBuffer)
        {
            // below commmented Code own experi -- this above void Update() -- not same as -- void Update() in script ParamCube--that uses a  [_band]
            // transform.localScale = new Vector3((c_AudioPeer._Amplitude [_band_1] * _maxScale) + _startScale, (c_AudioPeer._Amplitude  [_band_1]* _maxScale) + _startScale, (c_AudioPeer._Amplitude * _maxScale) + _startScale);
            // Color _color = new Color(_red * c_AudioPeer._Amplitude [_band_1], _green * c_AudioPeer._Amplitude [_band_1], _blue * c_AudioPeer._Amplitude [_band_1]);
            //transform.localScale = new Vector3(transform.localScale.x,(c_AudioPeer._Amplitude  * _maxScale) + _startScale, (c_AudioPeer._Amplitude  * _maxScale) + _startScale, (c_AudioPeer._Amplitude * _maxScale) + _startScale,transform.localScale.z);
            transform.localScale = new Vector3((c_AudioPeer._Amplitude  * _maxScale) + _startScale , transform.localScale.y, transform.localScale.z);
            //transform.localScale = new Vector3(transform.localScale.x, (c_AudioPeer._audioBandBuffer[_band] * scaleMultiplier) + startScale, transform.localScale.z);
            //transform.localScale = new Vector3(transform.localScale.x, (c_AudioPeer._audioBandBuffer[_band] * scaleMultiplier) + startScale,
            
            // Color _color = new Color(_red * c_AudioPeer._Amplitude , _green * c_AudioPeer._Amplitude , _blue * c_AudioPeer._Amplitude );
            // _material.SetColor("_EmissionColor", _color); // FOO_Not used -- cant get Emissions Working 
        }
        if (_useBuffer)
        {
            // below commmented Code own experi -- this above void Update() -- not same as -- void Update() in script ParamCube--that uses a  [_band]
            // transform.localScale = new Vector3((c_AudioPeer._AmplitudeBuffer [_band_1]* _maxScale) + _startScale, (c_AudioPeer._AmplitudeBuffer [_band_1]* _maxScale) + _startScale, (c_AudioPeer._AmplitudeBuffer * _maxScale) + _startScale);
            // Color _color = new Color(_red * c_AudioPeer._AmplitudeBuffer[_band_1], _green * c_AudioPeer._AmplitudeBuffer [_band_1], _blue * c_AudioPeer._AmplitudeBuffer [_band_1]);
            // transform.localScale = new Vector3(transform.localScale.x,(c_AudioPeer._AmplitudeBuffer * _maxScale) + _startScale, (c_AudioPeer._AmplitudeBuffer * _maxScale) + _startScale, (c_AudioPeer._AmplitudeBuffer * _maxScale) + _startScale);
            transform.localScale = new Vector3((c_AudioPeer._Amplitude  * _maxScale) + _startScale , transform.localScale.y, transform.localScale.z);
            // Color _color = new Color(_red * c_AudioPeer._AmplitudeBuffer, _green * c_AudioPeer._AmplitudeBuffer , _blue * c_AudioPeer._AmplitudeBuffer );
            // _material.SetColor("_EmissionColor", _color); // FOO_Not used -- cant get Emissions Working 
        }
    }
}
