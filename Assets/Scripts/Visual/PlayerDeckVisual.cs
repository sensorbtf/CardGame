using UnityEngine;
using System.Collections;
using DG.Tweening;

// this class should be attached to the deck
// generates new cards and places them into the hand
public class PlayerDeckVisual : MonoBehaviour
{
    public AreaPosition owner;
    public float HeightOfOneCard;

    void Start()
    {
        CardsInDeck = GlobalSettings.Instance.Players[owner].deck.cards.Count;
    }

    private int _cardsInDeck = 0;
    public int CardsInDeck
    {
        get { return _cardsInDeck; }

        set
        {
            _cardsInDeck = value;
            transform.localPosition = new Vector3(0, 0, -HeightOfOneCard * value);
        }
    }
}