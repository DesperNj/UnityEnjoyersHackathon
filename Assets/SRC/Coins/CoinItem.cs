using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public Vector3 mainHeroPosition;
    public Vector3 startPosition;
    public float progress;
    // Start is called before the first frame update
    void Start()
    {
        GameObject mainHero = GameObject.Find("MainHeroLocator");

        mainHeroPosition = mainHero.transform.position;
        startPosition = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = Vector3.Lerp(startPosition, mainHeroPosition, progress);

        if (progress >= 1) {
            CoinManager.instance.IncrementCoins();
            Destroy(gameObject);
        }
    }
}
