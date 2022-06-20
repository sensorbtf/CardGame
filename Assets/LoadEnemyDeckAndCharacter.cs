using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEnemyDeckAndCharacter : MonoBehaviour
{
    void Awake()
    {
        Player p = GetComponent<Player>();

        if (MapScreen.Instance.choiceOfEnemy == 0) // when false = normal, when true = boss
        {
            Debug.LogWarning("LOADED NORMAL");
            if (BattleStartInfo.EnemyDeck.Character != null)
                p.charAsset = BattleStartInfo.EnemyDeck.Character;
            if (BattleStartInfo.EnemyDeck.Cards != null)
                p.deck.cards = RandomizeCardsInList(BattleStartInfo.EnemyDeck.Cards);
        }
        else
        {
            Debug.LogWarning("LOADEDElite");
            if (BattleStartInfo.EliteEnemyDeck.Character != null)
                p.charAsset = BattleStartInfo.BossEnemyDeck.Character;
            if (BattleStartInfo.EliteEnemyDeck.Cards != null)
                p.deck.cards = RandomizeCardsInList(BattleStartInfo.EliteEnemyDeck.Cards);
        }
    }
    public List<CardAsset> RandomizeCardsInList(List<CardAsset> inputList)
    {
        var copy = new List<CardAsset>(inputList); // make a copy of the list:
        copy.Shuffle(); // shuffle the list so that we are always replacing different cards:
        if (MapScreen.Instance.choiceOfEnemy == 0)
        {
            for (var i = 0; i < 3; i++)
                copy[i] = GetRandomCard();
        }
        else if (MapScreen.Instance.choiceOfEnemy == 1)
        {
                for (var i = 0; i < 2; i++)
                copy[0] = GetRandomCard(); // choosing only 2 cuz 2 card in list
        }
        else if (MapScreen.Instance.choiceOfEnemy == 2)
        {
                copy[0] = GetRandomCard(); // choosing only 1 cuz 1 card in list
        }
        return copy;
    }
    public CardAsset GetRandomCard() 
    {
        if (MapScreen.Instance.choiceOfEnemy == 0)
            return CardCollection.Instance.allCreaturesCardsArray[Random.Range(0, CardCollection.Instance.allCreaturesCardsArray.Length)];
        else if (MapScreen.Instance.choiceOfEnemy == 1)
        {
            return CardCollection.Instance.allEliteCreaturesCardsArray[Random.Range(0, CardCollection.Instance.allEliteCreaturesCardsArray.Length)];
        }
        else 
        {
            return CardCollection.Instance.allBossCreaturesCardsArray[Random.Range(0, CardCollection.Instance.allBossCreaturesCardsArray.Length)];
        }
    }
}
