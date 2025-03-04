using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;

    private bool isGameOver = false;
    private bool isGameWin = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HitEnd()
    {
        Win();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0; //k cho player lam dc gi nua trong luc nay
        gameOverUI.SetActive(true); //hien panel GameOver len
    }

    public void Win()
    {
        isGameWin = true;
        Time.timeScale = 0;
        winUI.SetActive(true);
    }

    public void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1; //cho phep player thao tac lai
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //khoi tao lai Scene Game
    }

    public void Continue()
    {
        int sceneHienTai = SceneManager.GetActiveScene().buildIndex;

        // Check if there is a next scene
        if (sceneHienTai < SceneManager.sceneCountInBuildSettings - 1)
        {
            // Load the next scene
            SceneManager.LoadScene(sceneHienTai + 1);
        }
        else
        {
            Debug.Log("Chưa có màn mới, cảm ơn đã chơi hết!");
        }

        Time.timeScale = 1; // Unfreeze the game
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }
}
