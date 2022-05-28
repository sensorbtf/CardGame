using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuildingScreen : MonoBehaviour
{

    public GameObject ScreenContent;
    public GameObject ReadyDecksList;
    public GameObject CardsInDeckList;
    public DeckBuilder BuilderScript;
    public ListOfDecksInCollection ListOfReadyMadeDecksScript;
    public CollectionBrowser CollectionBrowserScript;
    public CharacterSelectionTabs TabsScript;
    public bool ShowReducedQuantitiesInDeckBuilding = true;

    public static DeckBuildingScreen Instance;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        HideScreen();
    }

    public void EditDeck(DeckInfo savedDeckInfo)
    {
        // switch collection to editing mode, display the deck list on the right
        // the easiest way is: 
        // 0) hide screen
        HideScreen();
        // 1) make sure that it is for the same character and load the same deck name. 
        BuilderScript.BuildADeckFor(savedDeckInfo.Character);
        BuilderScript.DeckName.text = savedDeckInfo.DeckName;
        // 2) populate it with the same cards that were in this deck.
        foreach (CardAsset asset in savedDeckInfo.Cards)
            BuilderScript.AddCard(asset);
        // 3) delete the deck that we are editing from DecksStorage
        DecksStorage.Instance.AllDecks.Remove(savedDeckInfo);
        // 4) when we press "Done", this deck with changes will be added as a new deck

        // apply character class and activate tab.
        TabsScript.SetClassOnClassTab(savedDeckInfo.Character);
        CollectionBrowserScript.ShowCollectionForDeckBuilding(savedDeckInfo.Character);
        // TODO: save the index of this deck not to make it shift to the end of the list of decks and add it to the same place.

        ShowScreenForDeckBuilding();
    }


    public void ShowScreenForCollectionBrowsing()
    {
        ScreenContent.SetActive(true);
        ReadyDecksList.SetActive(true);
        CardsInDeckList.SetActive(false);
        BuilderScript.InDeckBuildingMode = false;
        ListOfReadyMadeDecksScript.UpdateList();

        CollectionBrowserScript.AllCharactersTabs.gameObject.SetActive(true);
        CollectionBrowserScript.OneCharacterTabs.gameObject.SetActive(false);
        Canvas.ForceUpdateCanvases();

        CollectionBrowserScript.ShowCollectionForBrowsing();
    }

    public void ShowScreenForDeckBuilding()
    {
        ScreenContent.SetActive(true);
        ReadyDecksList.SetActive(false);
        CardsInDeckList.SetActive(true);

        CollectionBrowserScript.AllCharactersTabs.gameObject.SetActive(false);
        CollectionBrowserScript.OneCharacterTabs.gameObject.SetActive(true);
        Canvas.ForceUpdateCanvases();
        // TODO: update the tab to say the name of the character class that we are building a deck for, update the script on the tab.
    }

    public void BuildADeckFor(CharacterAsset asset)
    {
        ShowScreenForDeckBuilding();
        CollectionBrowserScript.ShowCollectionForDeckBuilding(asset);
        DeckBuildingScreen.Instance.TabsScript.SetClassOnClassTab(asset);
        BuilderScript.BuildADeckFor(asset);
    }

    public void HideScreen()
    {
        ScreenContent.SetActive(false);
        CollectionBrowserScript.ClearCreatedCards();
    }
}
