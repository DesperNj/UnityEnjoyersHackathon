using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LivesController : MonoBehaviour
{
    public static LivesController instance = null;
    public UnityEvent gameOver;
    public int livesOnStart;
    public int curentLives;
    public int beatIgnorAllowed;
    int beatIgnored = 0;
    public float gameOverDelay;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curentLives = livesOnStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeatCatched()
    {
        beatIgnored = 0;
    }

    public void BeatIgnored()
    {
        beatIgnored++;

        if(beatIgnored > beatIgnorAllowed)
        {
            LivesLost();
            beatIgnored = 0;
        }
    }

    public void LivesLost()
    {
        if(curentLives == 0) {
            return;
        }

        curentLives--;
        UpdateLivesUI(false);

        if (curentLives == 0) {
            gameOver.Invoke();
            Invoke("LoadMenuScene", gameOverDelay);
        }
    }

    void UpdateLivesUI(bool active)
    {
        if (curentLives >= 0 && curentLives < transform.childCount)
        {
            Transform child = transform.GetChild(curentLives);
            if (child) {
                child.gameObject.active = active;
            }
        }
    }

    public void IncrementLives()
    {
        if (curentLives == livesOnStart)
        {
            return;
        }

        UpdateLivesUI(true);
        curentLives++;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
