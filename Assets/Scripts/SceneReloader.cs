using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneReloader: MonoBehaviour {

    public static SceneReloader Instance;

    private int ChancesForPackManipulator = 5;

    private void Awake()
    {
        Instance = this;
    }
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
        SceneManager.LoadScene("MapScene");
    }
    public void AfterWinningGame()
    {
        int chanceForShop = Random.Range(0, ChancesForPackManipulator);

        if (chanceForShop == 0)
        {
            ChancesForPackManipulator = 5;
            ShopManager.isGettingPack = true;

            SceneManager.LoadScene("ShopScene");
            SingleGameStart.TargetState = MenuState.ShopScreen;
        }
        else
        {
            ChancesForPackManipulator--;
            StartGame();
        }
    }
    public void AfterLosingGame()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogWarning("PlayerPrefs reseted after losing");
        SingleGameStart.TargetState = MenuState.StartMenuScreen;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void LoadRandomFightScene()
    {
        SceneManager.LoadScene("FightScene");
    }
    public void LoadDeckBuildingScene()
    {
        SceneManager.LoadScene("DeckBuildingScene");
        SingleGameStart.TargetState = MenuState.FireplaceScreen;
        MapScreen.Instance.SelectDeckForEditing();
    }
    public void ShowMapNodes()
    {
        OuterMapParentVisibility.Instance.ActivateMap();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
