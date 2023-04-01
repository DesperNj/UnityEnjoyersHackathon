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
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        GameObject foloversContainer = GameObject.Find("FoloversContainer");
        int count = foloversContainer.transform.childCount;
       
        if (count <= 0) { 
            return; 
        }

        int index = Random.Range(0, count);
        Vector3 startPosition = foloversContainer.transform.GetChild(index).transform.position;

        Instantiate(coinObject, startPosition, Quaternion.identity, transform);
    }
}
