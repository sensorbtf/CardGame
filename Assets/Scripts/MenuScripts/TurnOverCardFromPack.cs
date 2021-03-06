using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnOverCardFromPack : MonoBehaviour {

    public Image Glow;

    private float InitialScale;
    private float scaleFactor = 1.2f;
    private OneCardManager manager;
    private PackOpeningArea packOpeningArea;

    void Awake()
    {
        InitialScale = transform.localScale.x;
        manager = GetComponent<OneCardManager>();
        packOpeningArea = GetComponent<PackOpeningArea>();
    }

    void OnMouseDown()
    {
        var card = manager.cardAsset;
        var deckToAddTheCard = DecksStorage.Instance.AllDecks[0]; // selecting first deck

        transform.DOScale(InitialScale * scaleFactor, 0.5f);

        CardCollection.Instance.QuantityOfEachCard[card]++; // adding to collection 

        ShopManager.Instance.OpeningArea.NumberOfCardsOpenedFromPack++;

        deckToAddTheCard.Cards.Add(card); // adding to only one deck
        DecksStorage.Instance.SaveDecksIntoPlayerPrefs();

        PackOpeningArea.Instance.Done();

    }
    void OnMouseEnter()
    {
        Glow.DOColor(ShopManager.Instance.OpeningArea.GlowColorsByRarity[manager.cardAsset.Rarity], 0.5f);
    }

    void OnMouseExit()
    {
        Glow.DOColor(Color.clear, 0.5f);
    }
}
