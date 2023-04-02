using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongoCat : MonoBehaviour
{
    public Sprite catDown;
    public Sprite catUp;
    public AnimationClip catIdleClip;
    public float timeToStartIdle = 1f;

    private SpriteRenderer sRenderer;
    private Animation animation;
    private float idleTime = 0f;
    private bool isInIdle = false;

    public void Update()
    {
        idleTime += Time.deltaTime;
        if(!isInIdle && idleTime >= timeToStartIdle)
        {
            SetIdle();
        }
    }
    public void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animation>();
    }
    public void CatUp()
    {
        idleTime = 0f;
        sRenderer.sprite = catUp;
    }
    public void CatDown()
    {
        animation.Stop();
        idleTime = 0f;
        sRenderer.sprite = catDown;
        Invoke(nameof(CatUp), 0.1f);
    }
    public void SetIdle()
    {
        isInIdle = !isInIdle;
        animation.clip = catIdleClip;
        animation.Play();
    }
}
