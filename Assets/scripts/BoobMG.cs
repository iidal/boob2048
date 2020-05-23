﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BoobMG : MonoBehaviour
{
    public static BoobMG instance;
    public bool isGameOver = false;

    int highScore = 0;
    [SerializeField] TextMeshProUGUI highScoreText;
    int points = 0;

    [SerializeField] Text pointsText;
    [SerializeField] GameObject GameOverScreen;
    AudioSource managerAS;
    [SerializeField] AudioClip loseSound;
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        GameOverScreen.SetActive(false);
        managerAS = GetComponent<AudioSource>();
        LoadHighScore();
    }

    public void StartGame()
    {
        SpawnBoobs.instance.SpawnBoob(0);
    }

    public void GameOver()
    {
        Debug.Log("game over");
        isGameOver = true;
        GameOverScreen.SetActive(true);
        managerAS.PlayOneShot(loseSound);


        if (points > highScore)
        {
            SaveLoad.SaveHighScore(points);
        }
        LoadHighScore();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void AddPoints(int amount)
    {
        points += amount;
        pointsText.text = points.ToString();
    }

    void LoadHighScore()
    {
        highScore = SaveLoad.LoadHighScore();
        highScoreText.text = highScore.ToString();
    }

}
