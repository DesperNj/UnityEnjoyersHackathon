using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatListener : MonoBehaviour
{
    public AnimationClip audioDist;
    public AnimationClip enviromentBeatCatched;
    private Animation animation;
    public bool defaultAnim = false; //TEMPORARY
    private void Awake()
    {
        animation = GetComponent<Animation>();
    }
   
    public void ResetPitch()
    {
        GetComponent<AudioSource>().pitch = 1.0f;
    }
    public void BeatCatched()
    {
        if (!defaultAnim)
        {
            return;
        }
        var transform = GetComponent<Transform>();
        transform.localScale = new Vector3(1.0f, 0.50f, 1.0f);
        Invoke(nameof(ResetScale), 0.1f);
    }
    public void BeatMissedSoundDist()
    {
        animation.clip = audioDist;
        animation.Play();
    }
    public void ResetScale()
    {
        var transform = GetComponent<Transform>();
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void EnviromentBeatCatched()
    {
        animation.clip = enviromentBeatCatched;
        animation.Play();
    }
}
