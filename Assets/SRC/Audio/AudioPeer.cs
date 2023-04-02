using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
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

    public bool writeCurrentBeat = false;
    public bool saveBeat = false;

    public TextAsset lastSavedBeat;
    private BeatSaver beatSaver;
    private int currentSavedBeat = 0;
    public UnityEvent beatStart;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        beatSaver = new BeatSaver();
        beatSaver.savedBeatTimings = new List<float>();

        if (writeCurrentBeat)
        {
            Invoke(nameof(SaveBeat), _audioSource.clip.length);
        }
        else
        {
            beatSaver = JsonUtility.FromJson<BeatSaver>(lastSavedBeat.ToString());
         }
    }
    void Update()
    {
        _timeAfterBeatAccepted += Time.deltaTime;
#if UNITY_EDITOR && !UNITY_WEBGL
        if (saveBeat)
        {
            SaveBeat();
            saveBeat = false;
        }
        SetSpectrumAudioSource();
        SetFrequinceGroups();
        if (_timeAfterBeatAccepted < beatSaver.savedBeatTimings[currentSavedBeat])
        {
            return;
        }
        for (int a = 0; a < _freqGroupsToListen.Length; a++)
        {
            if (_freqGroups[_freqGroupsToListen[a]] > freqSensivity)
            {
                beatStart.Invoke();
                _timeAfterBeatAccepted = 0f;
            }
        }


#endif
#if UNITY_WEBGL
        if (_timeAfterBeatAccepted < beatSaver.savedBeatTimings[currentSavedBeat])
        {
            return;
        }
        beatStart.Invoke();
        _timeAfterBeatAccepted = 0f;
        currentSavedBeat++;

#endif
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
            _freqGroups[i] = avgFreq * 10;
        }
    }

    public void SaveBeat()
    {
        var fileToSave = JsonUtility.ToJson(beatSaver);
        System.IO.File.WriteAllText(Application.dataPath + "/LastSavedBeat.json", fileToSave);
    }

    public void WriteBeat()
    {
        beatSaver.savedBeatTimings.Add(_timeAfterBeatAccepted);
    }
}


[Serializable]
public class BeatSaver
{
    public List<float> savedBeatTimings;

}
