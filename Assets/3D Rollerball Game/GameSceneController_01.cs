using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController_01 : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public PlayerController_01 player;

    public Text coinText;
    public Text gameText;

    public Text winResultText;
    public Text loseResultText;

    public GameObject startGameUI;
    public GameObject pauseMenuUI;
    public GameObject gameWinUI;
    public GameObject gameLoseUI;

    public GameObject coinPrefab;

    private int coinAmount;
    private int coinsCollected;

    private float gameTimer = 25f;

    private bool isGameOver = false;
    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;

        player.onCollectCoin = OnCollectionCoin;

        CoinSpawner();

        coinAmount = FindObjectsOfType<CoinController_01>().Length;
        coinText.text = "Coins: 0/" + coinAmount;

        startGameUI.SetActive(true);
    }

    void Update()
    {
        // 🔹 START GAME
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        // 🔹 Restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        // 🔹 Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        // 🔴 STOP after game over OR before start
        if (isGameOver || !gameStarted) return;

        // 🔹 Timer
        gameTimer -= Time.deltaTime;

        if (gameTimer <= 0)
        {
            gameTimer = 0;
            OnGameOver(false);
            return;
        }

        // 🔹 UI Update
        gameText.text = "Time: " + Mathf.Floor(gameTimer);
    }

    void StartGame()
    {
        gameStarted = true;

        Time.timeScale = 1f;

        startGameUI.SetActive(false);
    }

    void OnCollectionCoin()
    {
        if (isGameOver || !gameStarted) return;

        coinsCollected++;

        coinText.text = "Coins: " + coinsCollected + "/" + coinAmount;

        if (coinsCollected >= coinAmount)
        {
            OnGameOver(true);
        }
    }

    void OnGameOver(bool win)
    {
        if (isGameOver) return;

        isGameOver = true;

        Time.timeScale = 0f;

        gameText.enabled = false;
        coinText.enabled = false;

        if (win)
        {
            winResultText.text = "Coins: " + coinsCollected;
            gameWinUI.SetActive(true);
        }
        else
        {
            loseResultText.text = "Coins: " + coinsCollected;
            gameLoseUI.SetActive(true);
        }
    }

    void CoinSpawner()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject coinObject = Instantiate(coinPrefab);
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
        if (isGameOver || !gameStarted) return;

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
        Application.Quit();
    }
}