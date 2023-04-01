using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinObject;
    public int radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
             Instantiate(coinObject, transform.position + Random.insideUnitSphere * radius, Quaternion.identity, transform);
        }
    }
}
