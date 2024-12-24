using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameSceneController_02 : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public PlayerController_02 rollerball;
    public Camera gameCamera;
    public GameObject coinContainer;
    public Text coinText;
    public Text timeText;

    public Text winResultText;
    public Text loseResultText;

    public GameObject pauseMenuUI;
    public GameObject gameWinUI;
    public GameObject gameLoseUI;

    private int coinAmount;
    private int coinsCollected;
    private float gameTimer = 30f;
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;

        rollerball.onCoinCollected = () =>
        {
            OnCoinCollected();
        };

        coinAmount = coinContainer.GetComponentsInChildren<CoinController_02>().Length;
        coinText.text = "Coins: 0/" + coinAmount;
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Reload();
        }

        CameraMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        gameTimer -= Time.deltaTime;

        if (coinsCollected==coinAmount && gameTimer > 0)
        {
            OnGameOver(true);
        }

        if (gameTimer <= 0)
        {
            gameTimer = 0;

            OnGameOver(false);
        }

        if (!isGameOver)
        {
            //gameText.text = "Score : " + score.ToString() + "\n Time: " + Mathf.Floor(gameTimer);
            timeText.text = "Time: " + Mathf.Floor(gameTimer);
        }
    }

    void OnCoinCollected()
    {
        coinsCollected++;
        coinText.text = "Coins: " + coinsCollected + "/" + coinAmount;
    }

    void OnGameOver(bool win)
    {
        isGameOver = true;

        if (win)
        {
            Debug.Log("Method Game Win - You Won");
            Time.timeScale = 0f;
            winResultText.text = "Score : " + coinsCollected;
            coinText.enabled = false;
            timeText.enabled = false;
            gameWinUI.SetActive(true);
        }
        else
        {
            Debug.Log("Method Game Lose - You Lost");
            Time.timeScale = 0f;
            loseResultText.text = "Score : " + coinsCollected;
            coinText.enabled = false;
            timeText.enabled = false;
            gameLoseUI.SetActive(true);
        }
    }

    void Reload()
    {
        SceneLoader.LoadScene(SceneName.RollerballBoardGame);
    }

    void CameraMovement()
    {
        gameCamera.transform.position = new Vector3(
            rollerball.transform.position.x,
            rollerball.transform.position.y + 7,
            rollerball.transform.position.z - 5
        );
    }

    void PauseResumeGame()
    {
        if (GameIsPaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        else
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }

    public void StartMenuButtonPressed()
    {
        SceneLoader.LoadScene(SceneName.StartMenu);
    }

    public void QuitGameButtonPressed()
    {
        Debug.Log("Quit Game/Exit Application");
        Application.Quit();
    }
}