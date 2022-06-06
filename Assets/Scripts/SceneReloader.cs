using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneReloader: MonoBehaviour {

    public static void ReloadScene()
    {
        IDFactory.ResetIDs();
        IDHolder.ClearIDHoldersList();
        Command.OnSceneReload();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SingleGameStart.TargetState = MenuState.MapScreen;
        SceneManager.LoadScene("GameStartScreen");
    }
    public void AfterWinningGame()
    {
        SingleGameStart.TargetState = MenuState.ShopScreen;
        SceneManager.LoadScene("GameStartScreen");
    }
    public void AfterLosingGame()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogWarning("PlayerPrefs reseted after losing");
        SingleGameStart.TargetState = MenuState.StartMenuScreen;
        SceneManager.LoadScene("MainMenuScreen");
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    //Random Scene Have to have proper indexation

    public void LoadRandomFightScene()
    {
        int index = Random.Range(1, 2);
        SceneManager.LoadScene(index);
    }
    public void LoadRandomBossFightScene()
    {
        int index = Random.Range(2, 3);
        SceneManager.LoadScene(index);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
