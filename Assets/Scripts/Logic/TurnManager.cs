using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

// this class will take care of switching turns and counting down time until the turn expires
public class TurnManager : MonoBehaviour {

    // PUBLIC FIELDS
    public CardAsset CoinCard;

    // for Singleton Pattern
    public static TurnManager Instance;

    // PRIVATE FIELDS
    // reference to a timer to measure 
    private RopeTimer timer;

    // PROPERTIES
    private Player _whoseTurn;
    public Player whoseTurn
    {
        get
        {
            return _whoseTurn;
        }
        set
        {
            _whoseTurn = value;
            timer.StartTimer();

            GlobalSettings.Instance.EnableEndTurnButtonOnStart(_whoseTurn);

            TurnMaker tm = whoseTurn.GetComponent<TurnMaker>();
            // player`s method OnTurnStart() will be called in tm.OnTurnStart();
            tm.OnTurnStart();
            if (tm is PlayerTurnMaker)
            {
                whoseTurn.HighlightPlayableCards();
            }
            // remove highlights for opponent.
            whoseTurn.otherPlayer.HighlightPlayableCards(true);
        }
    }

    // METHODS
    void Awake()
    {
        Instance = this;
        timer = GetComponent<RopeTimer>();
    }

    void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        CardLogic.CardsCreatedThisGame.Clear();
        CreatureLogic.CreaturesCreatedThisGame.Clear();
        foreach (Player p in Player.Players)
        {
            p.ManaThisTurn = 4;
            p.ManaLeft = 4;
            p.LoadCharacterInfoFromAsset();
            p.TransmitInfoAboutPlayerToVisual();
            p.PArea.PDeck.CardsInDeck = p.deck.cards.Count;
            p.PArea.PDiscard.CardsInDiscardPile = p.discardpile.discardedCards.Count;
           
            // Can be used to show fighters/every attack? p.PArea.Portrait.transform.position = p.PArea.InitialPortraitPosition.position;
        }
        
        Sequence s = DOTween.Sequence();
        //s.Append(Player.Players[0].PArea.Portrait.transform.DOMove(Player.Players[0].PArea.PortraitPosition.position, 1f).SetEase(Ease.InQuad));
        //s.Insert(0f, Player.Players[1].PArea.Portrait.transform.DOMove(Player.Players[1].PArea.PortraitPosition.position, 1f).SetEase(Ease.InQuad));
        s.PrependInterval(3f);
        s.OnComplete(() =>
            {
                //Who start's game
                Player whoGoesFirst = Player.Players[0];
                Player whoGoesSecond = whoGoesFirst.otherPlayer;
         
                // draw 5 cards
                int initDraw = 3;
                whoGoesSecond.DrawACard(true);
                whoGoesFirst.DrawACard(true);
                for (int i = 0; i < initDraw; i++)
                {            
                    // second player draws a card
                    whoGoesSecond.DrawACard(true);
                    // first player draws a card
                    whoGoesFirst.DrawACard(true);
                }
                new StartATurnCommand(whoGoesFirst).AddToQueue();
            });
    }
    public void EndTurn()
    {
        Draggable[] AllDraggableObjects = GameObject.FindObjectsOfType<Draggable>();
        foreach (Draggable d in AllDraggableObjects)
            d.CancelDrag();
        // stop timer
        timer.StopTimer();
        // send all commands in the end of current player`s turn
        whoseTurn.OnTurnEnd();
        new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();
    }

    public void StopTheTimer()
    {
        timer.StopTimer();
    }

}

