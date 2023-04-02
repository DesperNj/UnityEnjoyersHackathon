using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoloverItem : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float progress;
    public int moveOfset;
    public int maxProbability;
    bool needDelete = false;
    private float time = 0.0f;
    public float interpolationPeriod = 1.0f;

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
        if (progress >= 0.99) {

            time += Time.deltaTime;
            if (time >= interpolationPeriod) {
                time = 0.0f;
                TryToThrowCoin();
            }
            
            if (needDelete) {
                Destroy(gameObject);
            }
            return;
        }

        GetComponent<Transform>().position = Vector3.Lerp(startPosition, targetPosition, progress);
    }

    public void Gone()
    {
        startPosition = transform.position;
        targetPosition = startPosition;
        targetPosition.z = 0;
        progress = 0.0f;
        GetComponent<Animation>().Play();        
        needDelete = true;
    }

    void TryToThrowCoin()
    {
        if(Random.Range(0, maxProbability) == 1) {
            CoinManager.instance.SpawnCoin();
        }
    }
}
