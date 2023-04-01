using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoloverItem : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float progress;
    public int moveOfset;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition;
        targetPosition.z = startPosition.z + moveOfset;
    }

    // Update is called once per frame
    void Update()
    {
        if (progress >= 1)
        {
            return;
        }

        GetComponent<Transform>().position = Vector3.Lerp(startPosition, targetPosition, progress);
    }
}
