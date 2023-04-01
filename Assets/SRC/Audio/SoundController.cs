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

public class SoundController : MonoBehaviour
{
    public static SoundController instance = null;
    public int beatsCatched = 0;
    public float beatCatchRange = 0;
    public float beatTimeAfterPlay = 0;
    public UnityEvent beatCatched;
    public UnityEvent beatMissed;
    public GameObject backAudioPrefab;
    public AdditioanSound[] additioanSounds;

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
        beatTimeAfterPlay += Time.deltaTime;
    }
    public void BeatdCatched()
    {
        beatsCatched++;
        beatCatched.Invoke();
        CheckAdditionalSounds();
    }
    public void CheckAdditionalSounds()
    {
        for (int a = 0; a < additioanSounds.Length; a++)
        {
            if (additioanSounds[a].CheckCanStartCondition())
            {
                additioanSounds[a].isInited = true;
                var prefab = Instantiate(backAudioPrefab);
                prefab.transform.parent = _transform;

                var audioS = prefab.GetComponent<AudioSource>();
                audioS.clip = additioanSounds[a].audio[0];
                audioS.volume = additioanSounds[a].volume;
                audioS.Play();
            }
        }
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