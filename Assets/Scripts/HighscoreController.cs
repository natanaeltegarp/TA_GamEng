using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighscoreController : MonoBehaviour
{
    public Text timeText;
    public Text highscoreText;

    private float startTime;
    private bool isRunning;
    private TimeSpan timePlaying;
    private float highscore;

    public Transform player; // Referensi ke pemain
    public Transform enemy; // Referensi ke musuh
    public float detectionRadius = 1.5f; // Radius deteksi untuk mencapai pemain

    void Start()
    {
        isRunning = true;
        startTime = Time.time;
        highscore = PlayerPrefs.GetFloat("Highscore", float.MaxValue);
        UpdateHighscoreUI();
    }

    void Update()
    {
        if (isRunning)
        {
            timePlaying = TimeSpan.FromSeconds(Time.time - startTime);
            string timePlayingStr = timePlaying.ToString("hh':'mm':'ss");
            timeText.text = timePlayingStr;

            if (HasReachedPlayer())
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        isRunning = false;

        if (timePlaying.TotalSeconds > highscore)
        {
            highscore = (float)timePlaying.TotalSeconds;
            PlayerPrefs.SetFloat("Highscore", highscore);
            PlayerPrefs.Save();
        }

        UpdateHighscoreUI();
    }

    bool HasReachedPlayer()
    {
        float distance = Vector3.Distance(player.position, enemy.position);
        return distance < detectionRadius;
    }

    void UpdateHighscoreUI()
    {
        TimeSpan highscoreTimeSpan = TimeSpan.FromSeconds(highscore);
        string highscoreStr = highscoreTimeSpan.ToString("hh':'mm':'ss");
        highscoreText.text = highscoreStr;
    }
}
