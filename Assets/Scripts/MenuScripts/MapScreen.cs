using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour {

    public GameObject ScreenContent;
    public DeckIcon[] DeckIcons;
    public HeroInfoPanel HeroPanelDeckSelection;

    public static MapScreen Instance;

    public bool choiceOfEnemy; // selecting boss/normal

    void Awake()
    {
        Instance = this;
        ShowDecks();
        // HideScreen(); solving the main menu
    }
    public void ShowDecks()
    {
        //// If there are no decks at all, show the character selection screen
        //if (DecksStorage.Instance.AllDecks.Count == 0)
        //{
        //    HideScreen();
        //    CharacterSelectionScreen.Instance.ShowScreen();
        //    return;
        //}

        // disable all deck icons first
        foreach (DeckIcon icon in DeckIcons)
        {
            icon.gameObject.SetActive(false);
            icon.InstantDeselect();
        }

        //for (int i = 0; i < DecksStorage.Instance.AllDecks.Count; i++)
        //{
        //    DeckIcons[i].ApplyLookToIcon(DecksStorage.Instance.AllDecks[i]);
        //    DeckIcons[i].gameObject.SetActive(true);
        //}
    }
    public void ShowScreen()
    {
        ScreenContent.SetActive(true);
        HeroPanelDeckSelection.OnOpen();
    }
    public void HideScreen()
    {
        ScreenContent.SetActive(false);
    }
    public void SelectDeck()
    {
        BattleStartInfo.SelectedDeck = DecksStorage.Instance.MainDeck[0];
    }
    public void SelectEnemyDeck()
    {
        choiceOfEnemy = false; Debug.LogWarning("Choice of normal: " + choiceOfEnemy);
        BattleStartInfo.EnemyDeck = DecksStorage.Instance.enemyDecks[0];
    }
    public void SelectBossEnemyDeck()
    {
        choiceOfEnemy = true; Debug.LogWarning("Choice of Boss: " + choiceOfEnemy);

        BattleStartInfo.BossEnemyDeck = DecksStorage.Instance.bossEnemyDecks[0];
    }
    public void SelectDeckForEditing()
    {
        Debug.LogWarning("Deck for editing selected" + DecksStorage.Instance.MainDeck.Count);
        DeckBuildingScreen.Instance.EditDeck(DecksStorage.Instance.MainDeck[0]);
    }
}
