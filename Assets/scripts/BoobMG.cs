using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoobMG : MonoBehaviour
{
    public static BoobMG instance;
    public bool isGameOver = false;

    public int points = 0;
    [SerializeField] Text pointsText;
    [SerializeField] GameObject GameOverScreen;
    AudioSource managerAS;
    [SerializeField] AudioClip loseSound;
    void Start()
    {
        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }
        GameOverScreen.SetActive(false);
        managerAS = GetComponent<AudioSource>();
    }

    public void StartGame(){
        SpawnBoobs.instance.SpawnBoob(0);
    }

    public void GameOver(){
        Debug.Log("game over");
        isGameOver = true;
        GameOverScreen.SetActive(true);
        managerAS.PlayOneShot(loseSound);
    }

    public void PlayAgain(){
        SceneManager.LoadScene(0);
    }

    public void AddPoints(int amount){
        points += amount;
        pointsText.text = points.ToString();
    }

}
