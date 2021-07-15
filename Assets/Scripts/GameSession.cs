using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int coinLevelCount = 0;

    [SerializeField] float LevelLoadDelay = 3.0f;

    [SerializeField] Text livesText;
    [SerializeField] Text coinsText;


    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddOneToCoinCount()
    {
        coinLevelCount++;
        UpdateCoinsUI();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    private IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private IEnumerator ResetLevelWithDelay()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TakeLife()
    {
        playerLives--;
        StartCoroutine(ResetLevelWithDelay());
        UpdateLivesUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLivesUI();
        UpdateCoinsUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateLivesUI()
    {
        livesText.text = playerLives.ToString();
    }
    private void UpdateCoinsUI()
    {
        coinsText.text = coinLevelCount.ToString();
    }
}
