using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    public GameObject[] Levels;

    static private int _counter = 0;
    static public int currentLevelCounter
    {
        get
        {
            return _counter;
        }
        set
        {
            _counter = value;
        }
    }

    public void SetLevelActive()
    {
        foreach (GameObject level in Levels)
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                Button[] buttonsBlocked = Levels[i].GetComponentsInChildren<Button>();
                foreach (var item in buttonsBlocked)
                {
                    item.interactable = false;
                }
            }

            Button[] buttons = Levels[currentLevelCounter].GetComponentsInChildren<Button>();
            foreach (var item in buttons)
            {
                item.interactable = true;
            }
        }
        IterateCurrentCounter();
        Debug.Log("COUNTER" + currentLevelCounter);
    }
    public void IterateCurrentCounter()
    {
        currentLevelCounter++;
    }
    public void DecreaseCurrentCounter()
    {
        currentLevelCounter--;
    }

    void Awake()
    {
        SetLevelActive();
    }
}
