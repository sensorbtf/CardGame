using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardACardToDeckCommand : Command
{
    private Player p;
    private int index;
    public DiscardACardToDeckCommand(Player p, int index)
    {
        this.p = p;
        this.index = index;
    }
    public override void StartCommandExecution()
    {
        p.PArea.handVisual.DiscardACardToDeck(index);
    }
}
