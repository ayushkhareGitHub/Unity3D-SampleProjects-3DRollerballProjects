using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController_01 : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public PlayerController_01 player;
    
    public Text gameText;
    public Text winResultText;
    public Text loseResultText;

    public GameObject pauseMenuUI;
    public GameObject gameWinUI;
    public GameObject gameLoseUI;

    public GameObject coinPrefab;
    private int score = 0;
    private float gameTimer = 30f;    
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;

        player.onCollectCoin = () =>
        {
            OnCollectionCoin();
        };

        CoinSpawner();
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        gameTimer -= Time.deltaTime;

        if (gameTimer <= 0)
        {
            gameTimer = 0;

            OnGameOver(false);
        }

        if (!isGameOver)
        {
            gameText.text = "Time: " + Mathf.Floor(gameTimer);
        }
    }

    void OnCollectionCoin()
    {
        score += 100;

        if (score == 1000)
        {
            OnGameOver(true);
        }
    }

    void OnGameOver(bool win)
    {
        isGameOver = true;

        if(win)
        {
            Debug.Log("Method Game Win - You Won");
            Time.timeScale = 0f;
            winResultText.text = "Score : " + score.ToString();
            gameText.enabled = false;
            gameWinUI.SetActive(true);
        }
        else
        {
            Debug.Log("Method Game Lose - You Lost");
            Time.timeScale = 0f;
            loseResultText.text = "Score : " + score.ToString();
            gameText.enabled = false;
            gameLoseUI.SetActive(true);
        }
    }

    void CoinSpawner()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject coinObject = GameObject.Instantiate<GameObject>(coinPrefab);
            coinObject.transform.position = new Vector3(
                Random.Range(-8, 8),
                coinObject.transform.position.y,
                Random.Range(-8, 8)
            );
        }
    }

    void Reload()
    {
        SceneLoader.LoadScene(SceneName.RollerballGame);
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
