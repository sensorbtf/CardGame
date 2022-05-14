using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    MapScreen, FireplaceScreen, ShopScreen
}
public class SingleGameStart : MonoBehaviour 
{
    public GameObject mapScreen;
    public GameObject fireplace;
    public GameObject shopScreen;

    public static MenuState TargetState = MenuState.MapScreen;

    public DeckSelectionScreen deckScreenContent;
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
                Debug.LogWarning("done");
                mapScreen.SetActive(true);
                deckScreenContent.ShowScreen();
                break;
            case MenuState.ShopScreen:
                shopScreen.SetActive(true);
                shopScreenContent.ShowScreen();
                break;
            //case MenuState.PackOpeningArea:
            //    shopScreen.SetActive(true);    <------------ po wygranej grze do "sklepu", gdzie wybierze si� 1 z 5 kart do kolekcji. Ogarn�� to w formie nie monet,
            //    a za darmo 1 pakiet + ew. za z�oto bardzo rzadko gromadzone
            //pozostaje te� kwestia ogarni�cia, co stanie si� tworzeniem kart - zamiana py�u na ksi�ycowe fragmenty do tworzenia legendarnych kart?
            //    break;
            default:
                break;
        }
    }
}
