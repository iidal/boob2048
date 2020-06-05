using System.Collections;
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
    [SerializeField] GameObject infoText;
    int points = 0;
    int multipliedPoints = 0;

    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] TextMeshProUGUI scoreBreakdownText;
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
        int goldTids = SpawnBoobs.instance.goldenTiddies;


        if (goldTids > 1)
        {
            scoreBreakdownText.text = points.ToString() + " points  * " + goldTids.ToString() + " point multiplier= <br> <size=35> " + (points * goldTids).ToString() + " points!";
            points = points * goldTids;
        }
        else
        {
            scoreBreakdownText.text = points.ToString() + " points!";
        }
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
        multipliedPoints = points*SpawnBoobs.instance.goldenTiddies;
        pointsText.text = multipliedPoints.ToString();
    }

    void LoadHighScore()
    {
        highScore = SaveLoad.LoadHighScore();
        highScoreText.text = "high score: <br>" + highScore.ToString();
    }

    public void ToggleInfo()
    {
        if (infoText.activeSelf)
        {
            infoText.SetActive(false);
        }
        else { infoText.SetActive(true); }
    }
}
