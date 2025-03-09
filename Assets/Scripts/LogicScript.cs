using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public int playerHP = 5;
    public Text scoreText;
    public Text hpText;
    public Text finalScore;
    public GameObject gameOverScreen;
    public GameObject startingScreen;
    public GameObject gameScreen;
    public GameObject creditsScreen;

    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void LoseHP(int hpToLose)
    {
        playerHP -= hpToLose;
        hpText.text = playerHP.ToString();
    }

    public void showCredits()
    {
        startingScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void startGame()
    {
        startingScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver()
    {
        finalScore.text = playerScore.ToString();
        gameScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
