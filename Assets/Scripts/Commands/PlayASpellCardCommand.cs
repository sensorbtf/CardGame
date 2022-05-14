using UnityEngine;
using System.Collections;

public class PlayASpellCardCommand: Command
{
    private CardLogic card;
    private Player p;
    //private ICharacter target;

    public PlayASpellCardCommand(Player p, CardLogic card)
    {
        this.card = card;
        this.p = p;
    }

    public override void StartCommandExecution()
    {
        Command.CommandExecutionComplete();
        // move this card to the spot
        p.PArea.handVisual.PlayASpellFromHand(card.UniqueCardID);
        //add card to discard pile to incrase size
        p.PArea.PDiscard.CardsInDiscardPile++;
        // do all the visual stuff (for each spell separately????)
    }
}
