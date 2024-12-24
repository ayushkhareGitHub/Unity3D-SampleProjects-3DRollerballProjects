using UnityEngine;
using System.Collections;

public static class SceneLoader
{
    //Method Load Scene based on enum
    public static void LoadScene(SceneName name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)name);
    }
}

public enum SceneName
{
    StartMenu,
    RollerballGame,
    RollerballBoardGame
}