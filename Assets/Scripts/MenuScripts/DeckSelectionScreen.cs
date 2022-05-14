﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectionScreen : MonoBehaviour {

    public GameObject ScreenContent;
    public DeckIcon[] DeckIcons;
    public HeroInfoPanel HeroPanelDeckSelection;

    public static DeckSelectionScreen Instance;

    void Awake()
    {
        Instance = this;
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

        for (int i = 0; i < DecksStorage.Instance.AllDecks.Count; i++)
        {
            DeckIcons[i].ApplyLookToIcon(DecksStorage.Instance.AllDecks[i]);
            DeckIcons[i].gameObject.SetActive(true);
        }
    }

    public void ShowScreen()
    {
        ScreenContent.SetActive(true);
        ShowDecks();
        HeroPanelDeckSelection.OnOpen();
    }

    public void HideScreen()
    {
        ScreenContent.SetActive(false);
    }
}
