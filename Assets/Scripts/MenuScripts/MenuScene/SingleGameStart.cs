using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    MapScreen, FireplaceScreen, ShopScreen, StartMenuScreen
}
public class SingleGameStart : MonoBehaviour 
{
    public GameObject mapScreen;
    public GameObject fireplace;
    public GameObject shopScreen;
    public GameObject startMenuScreen;

    public static MenuState TargetState = MenuState.MapScreen;

    public MapScreen deckScreenContent;
    public ShopManager shopScreenContent;

    private void Awake()
    {
        fireplace.SetActive(false);
        mapScreen.SetActive(false);
        shopScreen.SetActive(false);

        // activate only the UI that needs to be shown: 
        switch (TargetState)
        {
            case MenuState.MapScreen:
                mapScreen.SetActive(true);
                deckScreenContent.ShowScreen();
                break;
            case MenuState.ShopScreen:
                shopScreen.SetActive(true);
                shopScreenContent.ShowScreen();
                break;
            case MenuState.StartMenuScreen:
                startMenuScreen.SetActive(true);
                shopScreenContent.ShowScreen();
                break;
            //pozostaje te¿ kwestia ogarniêcia, co stanie siê tworzeniem kart - zamiana py³u na ksiê¿ycowe fragmenty do tworzenia legendarnych kart?
            //    break;
            default:
                break;
        }
    }
}
