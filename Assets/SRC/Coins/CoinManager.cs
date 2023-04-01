using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance = null;
    public GameObject coinObject;
    public int radius;
    int coinsCatched = 0;

    private void Awake()
    {
        if (!instance) {
            instance = this;
        }
    }

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

    public void SpawnCoin()
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

    public void IncrementCoins()
    {
        coinsCatched++;
    }

    public int GetCatchedCoins()
    {
        return coinsCatched;
    }
}
