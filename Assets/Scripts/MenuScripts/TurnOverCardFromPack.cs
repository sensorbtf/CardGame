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
   // public DeckBuilder deckBuilder;

    private bool turnedOver = true;

    void Awake()
    {
        InitialScale = transform.localScale.x;
        manager = GetComponent<OneCardManager>();
    }

    void OnMouseDown()
    {
        var card = manager.cardAsset;
        if (turnedOver == true)
        {
            turnedOver = false;
            // ustawić tak, żeby po wyborze pozostałe karty się cofały (selected? OneCardManager?)
            transform.DOScale(InitialScale * scaleFactor, 0.5f);

            CardCollection.Instance.QuantityOfEachCard[card]++;
            //deckBuilder.AddCard(card);
            ShopManager.Instance.OpeningArea.NumberOfCardsOpenedFromPack++;
        }
        else
        {
            turnedOver = true;

            transform.DOScale(InitialScale, 0.5f);

            CardCollection.Instance.QuantityOfEachCard[card]--;
            ShopManager.Instance.OpeningArea.NumberOfCardsOpenedFromPack--;
        }
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
