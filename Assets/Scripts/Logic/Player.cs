using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICharacter
{
    // PUBLIC FIELDS
    // int ID that we get from ID factory
    public int PlayerID;
    // a Character Asset that contains data about this Hero
    public CharacterAsset charAsset;
    // a script with references to all the visual game objects for this player
    public PlayerArea PArea;

    // REFERENCES TO LOGICAL STUFF THAT BELONGS TO THIS PLAYER
    public Deck deck;
    public Hand hand;
    public Table table;
    public DiscardPile discardpile;
    private PlayerDiscardPileVisual discardpileVisual;

    // a static array that will store both players, should always have 2 players
    public static Player[] Players;

    // this value used exclusively for our coin spell
    private int bonusManaThisTurn = 0; // dodac spelle zwiekszajace energie

    // PROPERTIES 
    // this property is a part of interface ICharacter
    public int ID
    {
        get{ return PlayerID; }
    }

    // opponent player
    public Player otherPlayer
    {
        get
        {
            if (Players[0] == this)
                return Players[1];
            else
                return Players[0];
        }
    }

    // total mana crystals that this player has this turn
    private int manaThisTurn;
    public int ManaThisTurn
    {
        get{ return manaThisTurn;}
        set
        {
            if (value < 0)
                manaThisTurn = 0;
            else if (value > PArea.ManaBar.Crystals.Length)
                manaThisTurn = PArea.ManaBar.Crystals.Length;
            else
                manaThisTurn = value;
            //PArea.ManaBar.TotalCrystals = manaThisTurn;
            new UpdateManaCrystalsCommand(this, manaThisTurn, manaLeft).AddToQueue();
        }
    }

    // full mana crystals available right now to play cards
    private int manaLeft;
    public int ManaLeft
    {
        get
        { return manaLeft;}
        set
        {
            if (value < 0)
                manaLeft = 0;
            else if (value > PArea.ManaBar.Crystals.Length)
                manaLeft = PArea.ManaBar.Crystals.Length;
            else
                manaLeft = value;
            
            //PArea.ManaBar.AvailableCrystals = manaLeft;
            new UpdateManaCrystalsCommand(this, ManaThisTurn, manaLeft).AddToQueue();
            if (TurnManager.Instance.whoseTurn == this)
                HighlightPlayableCards();
        }
    }
    private int health;
    public  int Health
    {
        get { return health;}
        set
        {
            if (value > charAsset.CurrentHealth)
                health = charAsset.CurrentHealth;
            else
                health = value;
            if (value <= 0)
                Die(); 
        }
    }

    // CODE FOR EVENTS TO LET CREATURES KNOW WHEN TO CAUSE EFFECTS
    public delegate void VoidWithNoArguments();
    //public event VoidWithNoArguments CreaturePlayedEvent;
    //public event VoidWithNoArguments SpellPlayedEvent;
    //public event VoidWithNoArguments StartTurnEvent;
    public event VoidWithNoArguments EndTurnEvent;


    // ALL METHODS
    void Awake()
    {
        // find all scripts of type Player and store them in Players array
        // (we should have only 2 players in the scene)
        Players = GameObject.FindObjectsOfType<Player>();
        // obtain unique id from IDFactory
        PlayerID = IDFactory.GetUniqueID();
    }

     void FixedUpdate()
    {
        // if enemy have no cards = die
        var cardsInHand = hand.CardsInHand.Count;
        var cardsInDeck = deck.cards.Count;
        var creaturesOnTable = table.CreaturesOnTable.Count;
        if (cardsInHand <= 0 && cardsInDeck <= 0 && creaturesOnTable <= 0)
            Victory();
    }

    public virtual void OnTurnStart()
    {
          // add one mana crystal to the pool;
        ManaLeft = ManaThisTurn;
        foreach (CreatureLogic cl in table.CreaturesOnTable)
        
            cl.OnTurnStart();
    }

    public void OnTurnEnd()
    {
        if (EndTurnEvent != null)
            EndTurnEvent.Invoke();
        ManaThisTurn -= bonusManaThisTurn;
        bonusManaThisTurn = 0;
    
        var cardsinhand = hand.CardsInHand.Count;
        for (int i = 0; i < cardsinhand; i++)
        {
            DiscardACardAtIndex(0);
        }

        if (deck.cards.Count <= 5)
        {
            var disc = discardpile.discardedCards.Count;
            for (int i = 0; i < disc; i++)
            {
                FromDiscardPileToDeck(0);
            }
            deck.cards.Shuffle();
        }
        GetComponent<TurnMaker>().StopAllCoroutines();
    }

    // STUFF THAT OUR PLAYER CAN DO

    // get mana from coin or other spells 
    public void GetBonusMana(int amount)
    {
        bonusManaThisTurn += amount;
        ManaThisTurn += amount;
        ManaLeft += amount;
    }

    // draw a single card from the deck
    public void DrawACard(bool fast = false)
    {
        if (deck.cards.Count > 0)
        {
            if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
            {
                // 1) logic: add card to hand
                CardLogic newCard = new CardLogic(deck.cards[0], this);
                hand.CardsInHand.Insert(0, newCard);
                // 2) logic: remove the card from the deck
                deck.cards.RemoveAt(0);
                // 2) create a command
                new DrawACardCommand(hand.CardsInHand[0], this, fast, fromDeck: true).AddToQueue();
            }
        }
    }
    // get card NOT from deck (a token or a coin)
    public void GetACardNotFromDeck(CardAsset cardAsset)
    {
        if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
        {
            // 1) logic: add card to hand
            CardLogic newCard = new CardLogic(cardAsset, this);
            newCard.owner = this;
            hand.CardsInHand.Insert(0, newCard);
            // 2) send message to the visual Deck
            new DrawACardCommand(hand.CardsInHand[0], this, fast: true, fromDeck: false).AddToQueue();
        }
    }

    // 2 METHODS FOR PLAYING SPELLS
    // 1st overload - takes ids as arguments
    // it is cnvenient to call this method from visual part
    public void PlayASpellFromHand(int SpellCardUniqueID, int TargetUniqueID)
    {
        if (TargetUniqueID < 0)
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], null);
        else if (TargetUniqueID == ID)
        {
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], this);
        }
        else
        {
            // target is a creature
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], CreatureLogic.CreaturesCreatedThisGame[TargetUniqueID]);
        }      
    }

    // 2nd overload - takes CardLogic and ICharacter interface - 
    // this method is called from Logic, for example by AI
    public void PlayASpellFromHand(CardLogic playedCard, ICharacter target)
    {
        ManaLeft -= playedCard.CurrentManaCost;
        // cause effect instantly:
        if (playedCard.effect != null)
            playedCard.effect.ActivateEffect(playedCard.ca.SpecialSpellAmount, target);
        else
        {
            Debug.LogWarning("No effect found on card " + playedCard.ca.name);
        }
        // no matter what happens, move this card to PlayACardSpot
        new PlayASpellCardCommand(this, playedCard).AddToQueue();
        //add to discard pile
        discardpile.discardedCards.Insert(0, playedCard);
        // remove this card from hand
        hand.CardsInHand.Remove(playedCard);
    }

    // METHODS TO PLAY CREATURES 
    // 1st overload - by ID
    public void PlayACreatureFromHand(int UniqueID, int tablePos)
    {
        PlayACreatureFromHand(CardLogic.CardsCreatedThisGame[UniqueID], tablePos);
    }

    // 2nd overload - by logic units
    public void PlayACreatureFromHand(CardLogic playedCard, int tablePos)
    {
        ManaLeft -= playedCard.CurrentManaCost;
        // create a new creature object and add it to Table
        CreatureLogic newCreature = new CreatureLogic(this, playedCard.ca);
        table.CreaturesOnTable.Insert(tablePos, newCreature);
        // 
        new PlayACreatureCommand(playedCard, this, tablePos, newCreature.UniqueCreatureID).AddToQueue();
        // cause battlecry Effect
        if (newCreature.effect != null)
            newCreature.effect.WhenACreatureIsPlayed();
        // remove this card from hand
        
        hand.CardsInHand.Remove(playedCard);
        HighlightPlayableCards();
    }
    public void Victory()
    {
        charAsset.CurrentHealth = Health;

        PArea.ControlsON = false;
        otherPlayer.PArea.ControlsON = false;
        TurnManager.Instance.StopTheTimer();
        new VictoryCommand(this).AddToQueue();
    }
    public void Die()
        {
            // game over
            // block both players from taking new moves 
            PArea.ControlsON = false;
            otherPlayer.PArea.ControlsON = false;
            TurnManager.Instance.StopTheTimer();
            new GameOverCommand(this).AddToQueue();
        }

    // METHOD TO SHOW GLOW HIGHLIGHTS
    public void HighlightPlayableCards(bool removeAllHighlights = false)
    {
        //Debug.Log("HighlightPlayable remove: "+ removeAllHighlights);
        foreach (CardLogic cl in hand.CardsInHand)
        {
            GameObject g = IDHolder.GetGameObjectWithID(cl.UniqueCardID);
            if (g!=null)
                g.GetComponent<OneCardManager>().CanBePlayedNow = (cl.CurrentManaCost <= ManaLeft) && !removeAllHighlights;
        }

        foreach (CreatureLogic crl in table.CreaturesOnTable)
        {
            GameObject g = IDHolder.GetGameObjectWithID(crl.UniqueCreatureID);
            if(g!= null)
                g.GetComponent<OneCreatureManager>().CanAttackNow = (crl.AttacksLeftThisTurn > 0) && !removeAllHighlights;
        }   
    }
    // START GAME METHODS
    public void LoadCharacterInfoFromAsset()
    {
        Health = charAsset.CurrentHealth;
        // change the visuals for portrait, hero power, etc...
        PArea.Portrait.charAsset = charAsset;
        PArea.Portrait.ApplyLookFromAsset();
    }

    public void TransmitInfoAboutPlayerToVisual()
    {
        PArea.Portrait.gameObject.AddComponent<IDHolder>().UniqueID = PlayerID;
        if (GetComponent<TurnMaker>() is AITurnMaker)
        {
            // turn off turn making for this character
            PArea.AllowedToControlThisPlayer = false;
        }
        else
        {
            // allow turn making for this character
            PArea.AllowedToControlThisPlayer = true;
        }
    }

    public void DiscardACardAtIndex(int index)
    {
        // check that there is a card with this index
        if (index < hand.CardsInHand.Count)
        {
            discardpile.discardedCards.Insert(index, hand.CardsInHand[index]);
            hand.CardsInHand.RemoveAt(index);
            new DiscardACardCommand(this, index).AddToQueue();
        }
        Command.CommandExecutionComplete();
    }

    //// shuffle and redraw from discard pile
    public void FromDiscardPileToDeck(int index)
    {
        if (discardpile.discardedCards.Count == 0) return;
        if (index < discardpile.discardedCards.Count)
        {
            deck.cards.Insert(index, discardpile.discardedCards[index].ca);
            //var TheCard = discardpile.CardsInDiscardPile[index].ca;
            //deck.cards.Add(TheCard);      
            new DiscardToDeckCommand(this, index).AddToQueue();
            discardpile.discardedCards.RemoveAt(index);
        }
        Command.CommandExecutionComplete();
    }
    public void DiscardACardToDeck(int index)
    {
        // check that there is a card with this index
        if (index < hand.CardsInHand.Count)
        {
            deck.cards.Insert(index, hand.CardsInHand[index].ca);
            hand.CardsInHand.RemoveAt(index);
            new DiscardACardCommand(this, index).AddToQueue();
        }
        else
        {
            // this is the case when you want to discard, but the hand is empty
        }
    }
}
