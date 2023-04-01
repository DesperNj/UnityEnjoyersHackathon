using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public AudioSource _audioSource;
    public BeatListener listener;
    public float[] _samples = new float[512];
    public float[] _freqGroups = new float[8];
    public float[] _freqGroupsToListen;
    public UnityEvent beatStart;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();   

    }
    void Update()
    {
        SetSpectrumAudioSource();
        SetFrequinceGroups();
        float dfg = _samples[1] *10;
        if (_freqGroups[4] > 1f || _freqGroups[5] > 1f)
        {
            beatStart.Invoke();
        }
    }
    void SetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
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
