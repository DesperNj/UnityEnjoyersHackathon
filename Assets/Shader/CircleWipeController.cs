using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWipeController : MonoBehaviour
{
    public Shader shader;

    private Material material;

    [Range(0, 1.2f)]
    public float _radius = 0f;

    public float _horizontal = 16;

    public float _vertical = 9;

    // Start is called before the first frame update
    void Start()
    {
        material = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    public void Update() 
    {
        var radiusSpeed = Mathf.Max(_horizontal, _vertical);
        material.SetFloat("_Radius", _radius);
        material.SetFloat("_Horizontal", _horizontal);
        material.SetFloat("_Vertical", _vertical);
        material.SetFloat("_RadiusSpeed", radiusSpeed);
    }
}
