using UnityEngine;
using System.Collections;

public enum CharClass{NormalEnemy, EliteEnemy, BossEnemy, Bras}

public class CharacterAsset : ScriptableObject 
{
	public CharClass Class;
	public string ClassName;
	public int MaxHealth = 100;
	public int CurrentHealth = 100;
	public Sprite AvatarImage;
    public Color32 ClassCardTint;
    public Color32 ClassRibbonsTint;
}
