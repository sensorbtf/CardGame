using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEnemyDeckAndCharacter : MonoBehaviour
{
    void Awake()
    {
        Player p = GetComponent<Player>();

        if (MapScreen.Instance.choiceOfEnemy == false) // when false = normal, when true = boss
        {
            Debug.LogWarning("LOADED NORMAL");
            if (BattleStartInfo.EnemyDeck.Character != null)
                p.charAsset = BattleStartInfo.EnemyDeck.Character;
            if (BattleStartInfo.EnemyDeck.Cards != null)
                p.deck.cards = RandomizeCardsInList(BattleStartInfo.EnemyDeck.Cards);
        }
        else
        {
            Debug.LogWarning("LOADEDBOSS");
            if (BattleStartInfo.BossEnemyDeck.Character != null)
                p.charAsset = BattleStartInfo.BossEnemyDeck.Character;
            if (BattleStartInfo.BossEnemyDeck.Cards != null)
                p.deck.cards = RandomizeCardsInList(BattleStartInfo.BossEnemyDeck.Cards);
        }
    }
    public List<CardAsset> RandomizeCardsInList(List<CardAsset> inputList)
    {
        var copy = new List<CardAsset>(inputList); // make a copy of the list:
        copy.Shuffle(); // shuffle the list so that we are always replacing different cards:
        if (MapScreen.Instance.choiceOfEnemy == false)
        {
            for (var i = 0; i < 3; i++)
                copy[i] = GetRandomCard();
        }
        else
        {
                copy[0] = GetRandomCard(); // choosing only 1 cuz 1 card in list
        }
        return copy;
    }
    public CardAsset GetRandomCard() 
    {
        if (MapScreen.Instance.choiceOfEnemy == false)
            return CardCollection.Instance.allCreaturesCardsArray[Random.Range(0, CardCollection.Instance.allCreaturesCardsArray.Length)];
        else
            return CardCollection.Instance.allBossCreaturesCardsArray[Random.Range(0, CardCollection.Instance.allBossCreaturesCardsArray.Length)];
    }

}
