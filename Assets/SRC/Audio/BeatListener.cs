using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatListener : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (SoundController.instance.TryCatchBeat())
            {
                BeatCatched();
            }
            else
            {
                var lossAnim = GetComponent<Animation>();
                if (lossAnim)
                {
                    lossAnim.Play();
                }
              
            //    Invoke(nameof(ResetPitch), 0.5f);
            }
        }
    }
    public void ResetPitch()
    {
        GetComponent<AudioSource>().pitch = 1.0f;
    }
    public void BeatCatched()
    {
        var transform = GetComponent<Transform>();
        transform.localScale = new Vector3(1.0f,0.50f,1.0f);
        Invoke(nameof(ResetScale), 0.1f);
    }
    public void ResetScale()
    {
        var transform = GetComponent<Transform>();
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
