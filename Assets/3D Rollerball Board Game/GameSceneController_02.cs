using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject startGameUI;
    public GameObject pauseMenuUI;
    public GameObject gameWinUI;
    public GameObject gameLoseUI;

    private int coinAmount;
    private int coinsCollected;

    private float gameTimer = 25f;

    private bool isGameOver = false;
    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;

        rollerball.canMove = false;

        rollerball.onCoinCollected = OnCoinCollected;

        coinAmount = coinContainer.GetComponentsInChildren<CoinController_02>().Length;
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

        // 🔹 RESTART
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        // 🔹 PAUSE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        // 🔴 STOP LOGIC
        if (isGameOver || !gameStarted) return;

        CameraMovement();

        // 🔹 TIMER
        gameTimer -= Time.deltaTime;

        if (coinsCollected >= coinAmount && gameTimer > 0)
        {
            OnGameOver(true);
            return;
        }

        if (gameTimer <= 0)
        {
            gameTimer = 0;
            OnGameOver(false);
            return;
        }

        // 🔹 UI
        timeText.text = "Time: " + Mathf.Floor(gameTimer);
    }

    // ================= START =================

    void StartGame()
    {
        gameStarted = true;

        Time.timeScale = 1f;
        rollerball.canMove = true;

        startGameUI.SetActive(false);
    }

    // ================= COIN =================

    void OnCoinCollected()
    {
        if (isGameOver) return;

        coinsCollected++;

        coinText.text = "Coins: " + coinsCollected + "/" + coinAmount;
    }

    // ================= GAME OVER =================

    void OnGameOver(bool win)
    {
        if (isGameOver) return;

        isGameOver = true;

        Time.timeScale = 0f;

        rollerball.canMove = false;

        coinText.enabled = false;
        timeText.enabled = false;

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

    // ================= CAMERA =================

    void CameraMovement()
    {
        gameCamera.transform.position = new Vector3(
            rollerball.transform.position.x,
            rollerball.transform.position.y + 7,
            rollerball.transform.position.z - 5
        );
    }

    // ================= SYSTEM =================

    void Reload()
    {
        SceneLoader.LoadScene(SceneName.RollerballBoardGame);
    }

    void PauseResumeGame()
    {
        if (isGameOver || !gameStarted) return;

        if (GameIsPaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            rollerball.canMove = true;
            GameIsPaused = false;
        }
        else
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            rollerball.canMove = false;
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