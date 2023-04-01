using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AcceptBeat()
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
