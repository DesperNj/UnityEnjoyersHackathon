using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Timeline;
using System.Numerics;
using Palmmedia.ReportGenerator.Core.Common;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
    public static SoundController instance = null;
    public int beatsCatched = 0;
    public float beatCatchRange = 0;
    public float beatTimeAfterPlay = 0;
    public UnityEvent beatCatched;
    public UnityEvent beatMissed;
    public GameObject backAudioPrefab;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Update()
    {
        beatTimeAfterPlay += Time.deltaTime;
    }
    public void BeatdCatched()
    {
        beatsCatched++;
        beatCatched.Invoke();
    }
    public void BeatMissed()
    {
        beatMissed.Invoke();
    }
    public void AcceptBeat()
    {
        beatTimeAfterPlay = 0f;
    }
    public bool TryCatchBeat()
    {
        if (beatTimeAfterPlay <= beatCatchRange)
        {
            BeatdCatched();
            return true;
        }
        BeatMissed();
        return false;
    }
    public void PlayAudio(AudioSource audio)
    {

    }

}
public class AdditioanSound
{
    public int beatsToStart = 0;
    public AudioClip[] audio;
    public void TryStart()
    {
        if (SoundController.instance.beatsCatched >= beatsToStart)
        {
            Instantiate()
        }
    }
}