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
using UnityEngine.UIElements;
using System.ComponentModel;

public class SoundController : MonoBehaviour
{
    public static SoundController instance = null;
    [ReadOnly(true)]
    public int beatsCatched = 0;
    [ReadOnly(true)]
    public int beatsMissed = 0;
    public float beatCatchTimeRange = 0;
    public UnityEvent beatCatched;
    public UnityEvent beatMissed;
    public GameObject backAudioPrefab;
    public AdditioanSound[] additioanSounds;

    public bool disableAdditionalSounds = false;
    private bool catchingLock = false;
    private float beatTimeAfterStart = 0;
    private Transform _transform;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            _transform = GetComponent<Transform>();
            CheckAdditionalSounds();
        }
    }

    void Update()
    {
        beatTimeAfterStart += Time.deltaTime;
    }
    public void BeatdCatched()
    {
        beatsCatched++;
        beatCatched.Invoke();
        CheckAdditionalSounds();
    }
    public void CheckAdditionalSounds()
    {
        if (disableAdditionalSounds)
        {
            return;
        }
        for (int a = 0; a < additioanSounds.Length; a++)
        {
            if (additioanSounds[a].CheckCanStartCondition())
            {
                additioanSounds[a].isInited = true;
                var prefab = Instantiate(backAudioPrefab);
                var component = prefab.AddComponent<BeatListener>();
                prefab.transform.parent = _transform;

                var audioS = prefab.GetComponent<AudioSource>();
                audioS.clip = additioanSounds[a].audio[0];
                audioS.volume = additioanSounds[a].volume;
                audioS.Play();
              //  beatMissed.AddListener(component.MissBeat);
            }
        }
    }
    public void BeatMissed()
    {
        beatMissed.Invoke();
        beatsMissed++;
    }
    public void AcceptBeat()
    {
        beatTimeAfterStart = 0f;
        catchingLock = false;
    }
    public bool TryCatchBeat()
    {
        if (catchingLock)
        {
            catchingLock = !catchingLock;
            return false;
        }
        if (beatTimeAfterStart <= beatCatchTimeRange)
        {
            BeatdCatched();
            return true;
        }
        else
        {
            Invoke(nameof(TryCatchBeat), beatCatchTimeRange);
            BeatMissed();
        }
        return catchingLock = true;
    }
    public void PlayAudio(AudioSource audio)
    {

    }

}
[System.Serializable]
public class AdditioanSound
{
    public int beatsToStart = 0;
    public AudioClip[] audio;
    public bool isInited = false;
    public float volume = 1f;
    public bool CheckCanStartCondition()
    {
        return SoundController.instance.beatsCatched >= beatsToStart && !isInited;
    }
}