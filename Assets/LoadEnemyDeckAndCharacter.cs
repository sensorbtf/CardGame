using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEnemyDeckAndCharacter : MonoBehaviour
{

    //public List<CardAsset> RandomizeCardsInList(List<CardAsset> inputList)
    //{
    //    var copy = new List<CardAsset>(inputList); // make a copy of the list:
    //    copy.Shuffle(); // shuffle the list so that we are always replacing different cards:
    //    for (var i = 0; i < 15; i++)
    //        copy[i] = GetRandomCard();
    //    return copy;
    //}

    //public void GetRandomCard()
    //{
    //    return CardCollection.Instance.allCardsArray[Random.Range(0, CardCollection.Instance.AllCards.Length)];
    //}

    void Awake()
    {
        Player p = GetComponent<Player>();
        if (BattleStartInfo.EnemyDeck != null)
        {
            if (BattleStartInfo.EnemyDeck.Character != null)
                p.charAsset = BattleStartInfo.EnemyDeck.Character;
            if (BattleStartInfo.EnemyDeck.Cards != null)
                p.deck.cards = new List<CardAsset>(BattleStartInfo.EnemyDeck.Cards);
        }
    }
}
