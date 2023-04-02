using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public float beatAcceptPause = 0.1f;
    public float freqSensivity = 0f;
    private float _timeAfterBeatAccepted = 0.0f;

    public AudioSource _audioSource;
    public BeatListener listener;

    public float[] _samples = new float[512];
    public float[] _freqGroups = new float[8];
    public int[] _freqGroupsToListen;

    public UnityEvent beatStart;

    [DllImport("__Internal")]
    private static extern bool StartLipSampling(string name, float duration, int bufferSize);

    [DllImport("__Internal")]
    private static extern bool CloseLipSampling(string name);

    [DllImport("__Internal")]
    private static extern bool GetLipSamples(string name, float[] freqData, int size);


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();   

    }
    void Update()
    {
        _timeAfterBeatAccepted += Time.deltaTime;
        SetSpectrumAudioSource();
        SetFrequinceGroups();
        if (_timeAfterBeatAccepted < beatAcceptPause)
        {
            return;
        }
        for (int a =0; a< _freqGroupsToListen.Length; a++)
        {
            if (_freqGroups[_freqGroupsToListen[a]]  > freqSensivity)
            {
                _timeAfterBeatAccepted=0f; 
                beatStart.Invoke();
            }
        }
    }
    void SetSpectrumAudioSource()
    {
#if UNITY_EDITOR
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
#endif
#if UNITY_WEBGL && !UNITY_EDITOR


        StartLipSampling(name, _audioSource.clip.length, 256);

        //if audio stopped
        CloseLipSampling(name);

        //when getting the audio data (kinda like GetSpectrumData)
        GetLipSamples(name, _samples, _samples.Length);
#endif

    }
    void SetFrequinceGroups()
    {
        int count = 0;
        for (int i = 0; i < _freqGroups.Length; i++)
        {
            float avgFreq = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            for (int j = 0; j < sampleCount; j++)
            {
                avgFreq += _samples[count] * (count + 1);
                count++;
            }
            avgFreq /= count;
            _freqGroups[i] = avgFreq*10;
        }
    }
}
