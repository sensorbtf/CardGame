using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DeckInScrollList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image AvatarImage;
    public Text NameText;
    public GameObject DeleteDeckButton;
    public DeckInfo savedDeckInfo;

    public void Awake()
    {
        DeleteDeckButton.SetActive(false);
    }
    public void EditThisDeck()
    {
        DeckBuildingScreen.Instance.EditDeck(savedDeckInfo);
    }

    public void DeleteThisDeck()
    {
        // TODO: Display the "Are you sure?" window
        DecksStorage.Instance.AllDecks.Remove(savedDeckInfo);
        Destroy(gameObject);
    }

    public void ApplyInfo (DeckInfo deckInfo)
    {
        AvatarImage.sprite = deckInfo.Character.AvatarImage;
        NameText.text = deckInfo.DeckName;
        savedDeckInfo = deckInfo;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        // show delete deck button
        DeleteDeckButton.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        // hide delete deck button
        DeleteDeckButton.SetActive(false);
    }
}
