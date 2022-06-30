using UnityEngine;
using System.Collections;

public class PlayerTurnMaker : TurnMaker 
{
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        // dispay a message that it is player`s turn
        new ShowMessageCommand("Your Turn!", 1f).AddToQueue();
        int cardsDrawByStart = 3;
        p.deck.cards.Shuffle();
        for (int i = 0; i < cardsDrawByStart; i++)
        {
            p.DrawACard();
        }     
    }
}
