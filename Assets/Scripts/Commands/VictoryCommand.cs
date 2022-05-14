using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCommand : Command
{

    private Player winner;

    public VictoryCommand(Player looser)
    {
        this.winner = looser;
    }

    public override void StartCommandExecution()
    {
        winner.PArea.Portrait.CherryUp();
    }
}
