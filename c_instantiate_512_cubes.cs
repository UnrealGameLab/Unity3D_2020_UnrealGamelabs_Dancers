using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_instantiate_512_cubes : MonoBehaviour {
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[512];
    public float _maxScale;
    // Start is called before the first frame update
    void Start()
    {
       for (int i = 0; i < 512; i++)
        {
            GameObject _instanceSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position; // Cube is made to sit in tghe CENTER of the 
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCube" + i; // each instance of the CUBE is  renamed
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instanceSampleCube.transform.position = Vector3.forward * 100;
            _sampleCube[i] = _instanceSampleCube;
      
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i= 0; i <512; i++)
        {
            if (_sampleCube != null)
            //print("SAMPLE CUBE FOUND OK -------");// OK 
            {
                //_sampleCube[i].transform.localScale = new Vector3(10, (c_AudioPeer._samples[i] * _maxScale) + 2, 10);
                // OK uncomment if Required 
            }
        }
    }
}
