using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AnimationClip MissedBeat;
    public AnimationClip GameOver;
    public AnimationClip BeatCatched;
    private Animation _cameraanimation;

    public void Awake()
    {
        _cameraanimation = GetComponent<Animation>();
    }

    public void CameraMissedBeat()
    {
        _cameraanimation.clip = MissedBeat;
        _cameraanimation.Play();
    }

    public void CameraGameOver()
    {
        _cameraanimation.clip = GameOver;
        _cameraanimation.Play();
    }
    public void CameraBeatCatched()
    {
        _cameraanimation.clip = BeatCatched;
        _cameraanimation.Play();
    }

}
