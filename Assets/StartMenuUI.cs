using UnityEngine;
using System.Collections;

public class StartMenuUI : MonoBehaviour
{
    float delayBetweenScreens = 1.5f;

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void RollerballGameButtonPressed()
    {
        // Call the Game Scene
        StartCoroutine(NextSceneDelay(SceneName.RollerballGame));
    }

    public void RollerballBoardGameButtonPressed()
    {
        // Call the Game Scene
        StartCoroutine(NextSceneDelay(SceneName.RollerballBoardGame));
    }

    public void QuitGameButtonPressed()
    {
        Debug.Log("Quit Game/Exit Application");
        Application.Quit();
    }

    IEnumerator NextSceneDelay(SceneName scene)
    {
        yield return new WaitForSeconds(delayBetweenScreens);
        SceneLoader.LoadScene(scene); // Use the SceneName enum
    }
}