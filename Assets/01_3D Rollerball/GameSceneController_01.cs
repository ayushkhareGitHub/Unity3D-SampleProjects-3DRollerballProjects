using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController_01 : MonoBehaviour
{
    public GameObject coinPrefab;

    public Text gameText;
    
    public PlayerController_01 player;

    private int score = 0;

    private float gameTimer = 45f;
    
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        player.onCollectCoin = () =>
        {
            OnCollectionCoin();
        };

        for(int i = 0; i < 10; i++)
        {
            GameObject coinObject = GameObject.Instantiate<GameObject>(coinPrefab);
            coinObject.transform.position = new Vector3(
                Random.Range(-8, 8),
                coinObject.transform.position.y,
                Random.Range(-8, 8)
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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
            gameText.text = "You win!!";
        }
        else
        {
            gameText.text = "You lost, press R to reload";
        }
    }
}
