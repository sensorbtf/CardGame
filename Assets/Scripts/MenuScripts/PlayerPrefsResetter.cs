using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsResetter : MonoBehaviour {

    // use this once to reset all the values to default.
    // after one use, uncheck this checkmark in the inspector.
    public bool DeleteAllFromPlayerPrefs = false;


    public static PlayerPrefsResetter Instance;

    // Use this for initialization
    void Awake () 
    {
        Instance = this;
        if (DeleteAllFromPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
            Debug.LogWarning("PlayerPrefs have been reset in script PlayerPrefsResetter");
        }
	}	
}
