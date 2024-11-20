using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameSceneController_03 : MonoBehaviour
{
    public PlayerController_03 rollerball;
    public Camera gameCamera;
    public GameObject coinContainer;
    public Text gameText;

    private int coinAmount;
    private int coinsCollected;
    private float gameTimer = 45f;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        rollerball.onCoinCollected = () =>
        {
            OnCoinCollected();
        };

        coinAmount = coinContainer.GetComponentsInChildren<CoinController_03>().Length;
        gameText.text = "Coins: 0/" + coinAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        gameCamera.transform.position = new Vector3(
            rollerball.transform.position.x,
            rollerball.transform.position.y + 7,
            rollerball.transform.position.z - 5
        );
        /*
        gameTimer -= Time.deltaTime;

        if (gameTimer <= 0)
        {
            gameTimer = 0;

            OnGameOver(false);
        }

        if (!isGameOver)
        {
            //gameText.text = "Score : " + score.ToString() + "\n Time: " + Mathf.Floor(gameTimer);
            gameText.text = "Time: " + Mathf.Floor(gameTimer);
        }*/
    }

    void OnCoinCollected()
    {
        coinsCollected++;
        gameText.text = "Coins: " + coinsCollected + "/" + coinAmount;
    }

    void OnGameOver(bool win)
    {
        isGameOver = true;

        if (win)
        {
            gameText.text = "You win!!";
        }
        else
        {
            gameText.text = "You lost, press R to reload";
        }
    }
}
