//Copyright (c) 2019 Peter Olthof, Peer Play
//http://www.peerplay.nl, info @ peerplay.nl 
//--------------------------------------------
//This script can be used in commercial and non-commercial software
//Please credit either Peer Play, or Peter Olthof in the final product

// FOO-Video-2-- Audio Visualization - Unity/C# Tutorial [Part 2 - GetSpectrumData in Unity]
// FOO_Video_6_Ranged_Usable_Values  --
// FOO_Video_7_Get_AVG._Amplitude -- 

using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent (typeof (AudioSource))]
public class c_AudioPeer : MonoBehaviour {
	AudioSource _audioSource;
    float[] _samples = new float[512]; // FOO_made == public static float --- So that we can access from another SCRIPT 
	// Assets\a_Custom_Scripts\c_KochScripts\ParamCube.cs(27,85): error CS0122: 'c_AudioPeer._samples' 
	// is inaccessible due to its protection level...

	// Initially its a public static / then in Video  FOO_Video_6_Ranged_Usable_Values
	// Its changed to PRIVATE float - by removing public and static --- just by keeping it FLOAT 
    // Will listen to AUDIO Source every FRAME and get SPECTRUM DATA into this - ARRAY of FLOATS named _samples
    // 20,000 - 20K samples are produced --- with the LOWEST being SAMPLE - 0 -- which is BASS
    // 20K being highest FREQ - Shrill sounds -- which here will be 512 
    // 20K are reduced to 512 and stored in this ARRAY

    //Microphone input
    public AudioClip _audioClip;
    // public bool _useMicrophone;
    // public string _selectedDevice;

	//FFT values
	// private float[] _samplesLeft = new float[512];
	// private float[] _samplesRight = new float[512];

	float[] _freqBand = new float[8]; 
	// Initially its a public static / then in Video  FOO_Video_6_Ranged_Usable_Values
	// Its changed to PRIVATE float - by removing public and static --- just by keeping it FLOAT 
	// FOO CHANGED it back again --->> Assets\a_Custom_Scripts\c_KochScripts\ParamCube.cs(27,85): error CS0122: 'c_AudioPeer._samples' 
	// is inaccessible due to its protection level...

	float[] _bandBuffer = new float[8]; //FART -- Video PART-5- makes this == public static
	// Initially its a public static / then in Video  FOO_Video_6_Ranged_Usable_Values
	// Its changed to PRIVATE float - by removing public and static --- just by keeping it FLOAT 
	// BUFFER value used to BUFFER between MAX values of FREQ's 
	// whenever FREQ BAND > BAND BUFF ,  _bandBuffer is INCREASED to become EQUAL 
	// whenever BAND BUFF > FREQ BAND ,  _bandBuffer should DECREASE by amount  - _bufferDecrease -- to become EQUAL 

	private float[] _bufferDecrease = new float[8];
	float[] _freqBandHighest = new float[8]; // FOO_Video_6_Ranged_Usable_Values  -- initially creates a FLOAT -- not a private float
	public static float[] _audioBand = new float[8];// FOO_Video_6_Ranged_Usable_Values
	public static float[] _audioBandBuffer = new float[8];// FOO_Video_6_Ranged_Usable_Values == _audioBandBuffer == Refernced in the c_Kochline.cs in Video -5 
	
	//


	//private float[] _bandBuffer = new float[8]; 
	// private float[] _bufferDecrease = new float[8];
	// private float[] _freqBandHighest = new float[8];

	//audio band values
	// [HideInInspector]
	// public float[] _audioBand, _audioBandBuffer;


	//Amplitude variables
	// [HideInInspector]
	public static float _Amplitude, _AmplitudeBuffer; // FOO_Video_7_Get_AVG._Amplitude
	private float _AmplitudeHighest; // Highest Amplitude -- // FOO_Video_7_Get_AVG._Amplitude
	// AMPLITUDE --- Is generally VOLUME -- High means more sound - its plotted on the Y Axis when Freq is on X Axis 
	// AMPLITUDE_Tutorial  --- https://www.youtube.com/watch?v=-fJfBVfoyD4



	//stereo channels
	// public enum _channel {Stereo, Left, Right};
	// public _channel channel = new _channel ();


    //Audio64
    // float[] _freqBand64 = new float[64];
	// float[] _bandBuffer64 = new float[64];
	// float[] _bufferDecrease64 = new float[64];
	// float[] _freqBandHighest64 = new float[64];
	// //audio band64 values
	// [HideInInspector]
	// public float[] _audioBand64, _audioBandBuffer64;


    // Use this for initialization
    void Start ()
    {
        // _audioBand = new float[8];
		// _audioBandBuffer = new float[8];
		// _audioBand64 = new float[64];
		// _audioBandBuffer64 = new float[64];
		_audioSource = GetComponent<AudioSource> ();
		//AudioProfile (0.5f);


        //Microphone input
        // if (_useMicrophone)
        // {
        //     if (Microphone.devices.Length > 0)
        //     {
        //         _selectedDevice = Microphone.devices[0].ToString();
        //         _audioSource.clip = Microphone.Start(_selectedDevice, true, 1, AudioSettings.outputSampleRate);
        //         while (Microphone.GetPosition(_selectedDevice) <= 0)
        //         {
        //             System.Threading.Thread.Sleep(8);
        //         }

        //     }
        //     else
        //     {
        //         _useMicrophone = false;
        //     }
        // }
        // if (!_useMicrophone)
        // {
        //     _audioSource.clip = _audioClip;
        // }

        _audioSource.Play();
     //   _audioSource.time += 110;
    }

	// Update is called once per frame
	void Update ()
    {
        if (_audioSource.clip != null)
        {
            GetSpectrumAudioSource(); // Method - Defined below and called here to RUN
            MakeFrequencyBands();
            // MakeFrequencyBands64();
            BandBuffer(); // FOO_BandBuffer
            // BandBuffer64();
            CreateAudioBands(); // FOO_Video_6_Ranged_Usable_Values
            // CreateAudioBands64();
            GetAmplitude();
        }
    }


    // void AudioProfile(float audioProfile)
	// {
	// 	for (int i = 0; i < 8; i++) {
	// 		_freqBandHighest [i] = audioProfile;
	// 	}
    //     for (int i = 0; i < 64; i++)
    //     {
    //         _freqBandHighest64[i] = audioProfile;
    //     }
    //     _AmplitudeHighest = audioProfile;
    // }

	void GetAmplitude()
	{
		float _CurrentAmplitude = 0; // private float 
		float _CurrentAmplitudeBuffer = 0; // private float 
		for (int i = 0; i < 8; i++) {
			_CurrentAmplitude += _audioBand [i]; // looping over _audioBand's and adding them to  _CurrentAmplitude
			_CurrentAmplitudeBuffer += _audioBandBuffer [i]; // looping over _audioBandBuffer's and adding them to _CurrentAmplitudeBuffer
		}
		if (_CurrentAmplitude > _AmplitudeHighest) {
			_AmplitudeHighest = _CurrentAmplitude;
		}
		_Amplitude = _CurrentAmplitude / _AmplitudeHighest; // public float -- declared on top
		_AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest; // public float -- declared on top
	}

	void CreateAudioBands()
	// FOO_Video_6_Ranged_Usable_Values 
	{
		for (int i = 0; i < 8; i++) 
		{
			if (_freqBand [i] > _freqBandHighest [i]) {
				_freqBandHighest [i] = _freqBand [i]; 
				// current FreqBand is Greater than HighestFreqBand - then bring it down and make it Equal to HighestFreqBand
			}
			//_audioBand [i] = Mathf.Clamp((_freqBand [i] / _freqBandHighest [i]), 0, 1);
			_audioBand [i] = (_freqBand [i] / _freqBandHighest [i]); // FOO_Video_6_Ranged_Usable_Values
			//_audioBandBuffer [i] = Mathf.Clamp((_bandBuffer [i] / _freqBandHighest [i]), 0, 1);
			_audioBandBuffer [i] = (_bandBuffer [i] / _freqBandHighest [i]); // FOO_Video_6_Ranged_Usable_Values
		}
	}


	// void CreateAudioBands64()
	// {
	// 	for (int i = 0; i < 64; i++) 
	// 	{
	// 		if (_freqBand64 [i] > _freqBandHighest64 [i]) {
	// 			_freqBandHighest64 [i] = _freqBand64 [i];
	// 		}
	// 		_audioBand64 [i] = Mathf.Clamp((_freqBand64 [i] / _freqBandHighest64 [i]), 0, 1);
	// 		_audioBandBuffer64 [i] = Mathf.Clamp((_bandBuffer64 [i] / _freqBandHighest64 [i]), 0, 1);
	// 	}
	// }

	void GetSpectrumAudioSource() // FOO-Video-2-- Audio Visualization - Unity/C# Tutorial [Part 2 - GetSpectrumData in Unity]
	{
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Hanning); // FOO other Option is Blackman -- in the Tute he uses BLACKMAN
		// _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Hanning); // FOO other Option is Blackman -- in the Tute he uses BLACKMAN
		// _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Hanning); // These are best to REDUCE Leakage of SPECTRUM DATA
	}

	void BandBuffer() // FOO_BandBuffer -- 1st version of the FUNC
	{
		for (int g = 0; g < 8; ++g) {
			if (_freqBand [g] > _bandBuffer [g]) {
				_bandBuffer [g] = _freqBand [g]; // if - freqband > bandBuff - then just make -- _bandBuffer = _freqBand 
				_bufferDecrease [g] = 0.005f; // Instantiate -- _bufferDecrease with a very small FLOAT --
				//-- use this value as INIT value in IF Loop below 
			}

			if (_freqBand [g] < _bandBuffer [g]) {
				_bandBuffer [g] -= _bufferDecrease[g];
				_bufferDecrease [g] *= 1.2f; // this is called every FRAME , if freqBand < bandBuff
				// this here method -- void BandBuffer()  -- is part of Update() -- thus called every FRAME
			}
		}
	}


	// void BandBuffer() // FOO_BandBuffer -- Final version of the FUNC
	// {
	// 	for (int g = 0; g < 8; ++g) {
	// 		if (_freqBand [g] > _bandBuffer [g]) {
	// 			_bandBuffer [g] = _freqBand [g];
	// 			//_bufferDecrease [g] = 0.005f;
	// 		}

	// 		if ((_freqBand [g] < _bandBuffer [g]) && (_freqBand [g] > 0)) {
    //             //_bufferDecrease[g] = (_bandBuffer[g] - _freqBand[g]) / 8; 
    //             _bandBuffer[g] -= _bufferDecrease[g];
	// 		}
	// 	}
	// }

    // private void OnAudioFilterRead(float[] data, int channels)
    // {
    //     if (_useMicrophone) System.Array.Clear(data, 0, data.Length);
    // }



    // void BandBuffer64()
	// {
	// 	for (int g = 0; g < 64; ++g) {
	// 		if (_freqBand64 [g] > _bandBuffer64 [g]) {
	// 			_bandBuffer64 [g] = _freqBand64 [g];
	// 		}

	// 		if ((_freqBand64 [g] < _bandBuffer64 [g]) && (_freqBand64 [g] > 0)) {
    //             _bufferDecrease64[g] = (_bandBuffer64[g] - _freqBand64[g]) / 8;
    //             _bandBuffer64[g] -= _bufferDecrease64 [g];
	// 		}

	// 	}
	// }



	void MakeFrequencyBands()
	{

		/*
         * 22050 / 512 = 43hertz per sample
         * 
         * 20 - 60 hertz
         * 250 - 500 herz
         * 500 - 2000 hertz
         * 2000 - 4000 hertz
         * 4000 - 6000 hertz
         * 6000 - 20000 hertz
         * 
         * 0 - 2  = 86 hertz
         * 1 - 4  = 172 hertz   - 87 - 258
         * 2 - 8  = 344 hertz   - 259 - 602
         * 3 - 16 = 688 hertz   - 603 - 1290 
         * 4 - 32 = 1376 hertz  - 1291 - 2666
         * 5 - 64 = 2762 hertz  - 2667 - 5418
         * 6 -128 = 5504 hertz  - 5419 - 10922
         * 7 -256 = 11008 hertz - 10923 - 21930
         * here above -- 2,4,8,16,32,64,128,256 --- Sum upto == 510
		 we need - 512 
         */


		int count = 0;
		float somevariable = 0.0f; // FART - testing-- created a FLOAT VAR to store the value of and PRINT == AVERAGE >> _freqBand [i];

		for (int i = 0; i < 8; i++) {
			float average = 0;
			//print("value of i ----"+i); // prints every FRAME - 0-7 -- 8 taken above as mid param in For Loop - as we want 8 Buckets / BANDS of FREQ's
			int sampleCount = (int)Mathf.Pow (2, i) * 2; // FART -- detailed Explanation Bottom of this SCRIPT 
			//print("sampleCount----"+sampleCount); // prints these Upper Bound INT's every FRAME --- 2,4,8,16,32,64,128,256

			if (i == 7) {
				sampleCount += 2;
				//print("sampleCount----"+sampleCount); // prints -- 258
			}
			// As we need - 512 -- above adds +2 to the Last Value of SAMPLE COUNT 
			for (int j = 0; j < sampleCount; j++) {
				average += _samples [count] * (count + 1);
				count++;

				//if (channel == _channel.Stereo) {
					//average += (_samplesLeft [count] + _samplesRight [count]) * (count + 1);
				//}
				// if (channel == _channel.Left) {
				// 	average += _samplesLeft [count] * (count + 1);
				// }
				// if (channel == _channel.Right) {
				// 	average += _samplesRight [count] * (count + 1);
				// }
				//count++;

			}
			average /= count;

			_freqBand [i] = average * 10;
			somevariable = _freqBand [i];
			//print("_freqBand----------"+ _freqBand); // System.Single[]
			//print("_somevariable------"+ somevariable); //FART MAIN -- Different Values every FRAME --- this is getting passed to ---
			// --- transform.localScale--in the SCRIPT -- ParamCube
			//as the Y Coordinate that creates HEIGHT of the CUBE's - (c_AudioPeer._freqBand[band] * scaleMultiplier)
		}
	}

// _freqBand------System.Single[]
// UnityEngine.MonoBehaviour:print(Object)
// c_AudioPeer:MakeFrequencyBands() (at Assets/a_Custom_Scripts/c_KochScripts/c_AudioPeer.cs:275)
// c_AudioPeer:Update() (at Assets/a_Custom_Scripts/c_KochScripts/c_AudioPeer.cs:106)



	// void MakeFrequencyBands64()
	// {
	// 		int count = 0;
	// 		int sampleCount = 1;
	// 		int power = 0;
	// 		for (int i = 0; i < 64; i++) {
	// 			float average = 0;

	// 			if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56) {
	// 				sampleCount = (int)Mathf.Pow (2, power);
	// 				if (power == 3) {
	// 					sampleCount -= 2;
	// 				}
	// 				power++;
	// 			}

	// 			for (int j = 0; j < sampleCount; j++) {
	// 				if (channel == _channel.Stereo) {
	// 					average += (_samplesLeft [count] + _samplesRight [count]) * (count + 1);
	// 				}
	// 				if (channel == _channel.Left) {
	// 					average += _samplesLeft [count] * (count + 1);
	// 				}
	// 				if (channel == _channel.Right) {
	// 					average += _samplesRight [count] * (count + 1);
	// 				}
	// 				count++;

	// 			}

	// 			average /= count;
	// 			_freqBand64 [i] = average * 80;
	// 		}
	//}

}

// FART --- int sampleCount = (int)Mathf.Pow (2, i) * 2;
// on the right side -- (int)Mathf.Pow (2, i) * 2;
// in the Loop the INT value of -- i -- is passed range - 0 to 7 
// then 2 is raised to power of this INT == i  
// result multiplied by 2 
// result saved in -- sampleCount
// this way we get sampleCount values == print("sampleCount----"+sampleCount); // prints these Upper Bound INT's every FRAME --- 2,4,8,16,32,64,128,256
// 

