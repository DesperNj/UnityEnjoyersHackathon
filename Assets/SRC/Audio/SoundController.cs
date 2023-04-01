using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Timeline;
using System.Numerics;
using Palmmedia.ReportGenerator.Core.Common;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

public class SoundController : MonoBehaviour
{
    public static SoundController instance = null;
    public int beatsCatched = 0;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {
    }
    public void BeatdCatched()
    {
        beatsCatched++;
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
            
        }
    }
}