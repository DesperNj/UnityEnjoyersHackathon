using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongoCat : MonoBehaviour
{
    public Sprite catDown;
    public Sprite catUp;
    private SpriteRenderer sRenderer;
    public void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }
    public void CatUp()
    {
        sRenderer.sprite = catUp;
    }
    public void CatDown()
    {
        sRenderer.sprite = catDown;
        Invoke(nameof(CatUp), 0.1f);
    }
}
