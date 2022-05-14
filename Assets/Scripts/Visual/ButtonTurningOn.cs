using UnityEngine.UI;
using UnityEngine;

public class ButtonTurningOn : MonoBehaviour
{
    private Player player;

    void Start()
    {
        var endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();
        if (player.hand.CardsInHand.Count >= 4)
        { 
        endTurnButton.interactable = true;
        }
    }
}
