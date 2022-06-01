using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

// this class should be attached to the deck
// generates new cards and places them into the hand
public class PlayerDiscardPileVisual : MonoBehaviour 
{
    public AreaPosition owner;
    public float HeightOfOneCard;
    public Transform DiscardPileSpot;
    public DiscardPile discardpile;

    public List<GameObject> CardsInDiscardPileList = new List<GameObject>();


    private int _cardsInDiscardPile = 0;
    public int CardsInDiscardPile
    {
        get { return _cardsInDiscardPile; }

        set
        {
            _cardsInDiscardPile = value;
            transform.localPosition = new Vector3(0, 0, -HeightOfOneCard * value);
        }
    }
    void Start()
    {
        CardsInDiscardPile = GlobalSettings.Instance.Players[owner].discardpile.discardedCards.Count;
    }

    public void AddCard(GameObject card)
    {
        // we allways insert a new card as 0th element in CardsInHand List 
        CardsInDiscardPileList.Insert(0, card);
    }

    // remove a card GameObject from hand
    public void RemoveCard(GameObject card)
    {
        // remove a card from the list
        CardsInDiscardPileList.Remove(card);
    }

    // remove card with a given index from hand
    public void RemoveCardAtIndex(int index)
    {
        CardsInDiscardPileList.RemoveAt(index);
    }

    // get a card GameObject with a given index in hand
    public GameObject GetCardAtIndex(int index)
    {
        return CardsInDiscardPileList[index];
    }

    public void DiscardToDeck(int index)
    {
        Debug.Log("Trying to discard a card with index: CardsInDiscardPileList.Count: " + CardsInDiscardPile);

        GameObject cardToDeck = CardsInDiscardPileList[index].gameObject;
        //Sequence s = DOTween.Sequence();
        //// move cards to the deck
        //s.Append(cardToDeck.transform.DOMove(DiscardPileSpot.position, 0.4f));
        //s.Insert(0f, cardToDeck.transform.DORotate(new Vector3(0f, -179f, 0f), 0.7f));
        //s.OnComplete(() =>
        //{
        //    Destroy(cardToDeck.gameObject);
        //    Command.CommandExecutionComplete();
        //});
    }
}
