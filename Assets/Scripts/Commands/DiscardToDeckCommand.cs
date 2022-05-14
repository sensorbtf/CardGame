using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardToDeckCommand : Command
{
    private Player p;
    private int index;

    public DiscardToDeckCommand(Player p, int index)
    {
        this.p = p;
        this.index = index;
    }
    public override void StartCommandExecution()
    {
        p.PArea.PDiscard.DiscardToDeck(index); 
        p.PArea.PDiscard.CardsInDiscardPile--;
        p.PArea.PDeck.CardsInDeck++;
    }
}

