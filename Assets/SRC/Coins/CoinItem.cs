using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public Vector3 mainHeroPosition;
    public Vector3 startPosition;
    public float progress;
    Animation hatAnim;
    // Start is called before the first frame update
    void Start()
    {
        GameObject mainHero = GameObject.Find("MainHeroLocator");

        mainHeroPosition = mainHero.transform.position;
        startPosition = GetComponent<Transform>().position;
        hatAnim = mainHero.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.Lerp(startPosition, mainHeroPosition, progress);
        newPos.y = GetComponent<Transform>().position.y;

        GetComponent<Transform>().position = newPos;

        if (progress >= 1) {
            if (hatAnim) {
                 hatAnim.Play();
            }
            CoinManager.instance.IncrementCoins();
            Destroy(gameObject);
        }
    }
}
