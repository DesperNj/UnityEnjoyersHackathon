using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Timeline;
using System.Numerics;
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
    [ReadOnly(true)]
    public int beatsAccepted = 0;

    public UnityEvent musicEnded;

    public float beatCatchTimeRange = 0;
    public UnityEvent beatCatched;
    public UnityEvent beatMissed;
    public UnityEvent beatIgnored;
    public GameObject backAudioPrefab;
    public AdditioanSound[] additioanSounds;
    public AudioSource audioS;
    public AudioClip winAudio;
    public AnimationClip musicDist;

    public bool disableAdditionalSounds = false;
    private bool catchingLock = false;
    private float beatTimeAfterStart = 0;
    private int currentCatchTry = 0;
    private int catchTries = 1;
    private Transform _transform;
    private bool additionalSoundStarted = false;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            _transform = GetComponent<Transform>();
            audioS = GetComponent<AudioSource>();
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
        Debug.Log("BeatCatched - " + beatsCatched);
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
                component.audioDist = musicDist;

                var audioS = prefab.GetComponent<AudioSource>();
                audioS.clip = additioanSounds[a].audio[0];
                audioS.volume = additioanSounds[a].volume;
                audioS.loop = false;
                audioS.Play();
                Invoke(nameof(PlayWinAudio), audioS.clip.length);
                additionalSoundStarted = true;
                beatMissed.AddListener(component.BeatMissedSoundDist);
            }
        }
    }
    public void BeatMissed()
    {
        beatMissed.Invoke();
        beatsMissed++;
        Debug.Log("BeatMissed - " + beatsMissed);
    }
    public void AcceptBeat()
    {
        if (!catchingLock)
        {
            beatIgnored.Invoke();
        }
        beatsAccepted++;
        beatTimeAfterStart = 0f;
        currentCatchTry = 0;
        catchingLock = false;
    }
    public void TryCatchBeat()
    {
        if (catchingLock)
        {
            BeatMissed();
            return;
        }
        if (beatTimeAfterStart <= beatCatchTimeRange)
        {
            BeatdCatched();
        }
        else
        {
            if (currentCatchTry == catchTries)
            {
                BeatMissed();
            }
            else
            {
                Invoke(nameof(TryCatchBeat), 0.1f * currentCatchTry);
                currentCatchTry++;
                return;
            }
        }
        catchingLock = true;
    }
    public void PlayWinAudio()
    {
        audioS.clip = winAudio;
        audioS.pitch = 1.4f;
        audioS.loop = false;
        audioS.Play();
        Invoke(nameof(TrackEnded), audioS.clip.length/2);
    }
    public void TrackEnded()
    {
        musicEnded.Invoke();
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