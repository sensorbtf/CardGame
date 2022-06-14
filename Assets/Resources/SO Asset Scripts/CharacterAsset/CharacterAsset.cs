using UnityEngine;
using System.Collections;

public enum CharClass{NormalEnemyV1, BossEnemyV1, Bras}

public class CharacterAsset : ScriptableObject 
{
	public CharClass Class;
	public string ClassName;
	public int MaxHealth = 100;
	public Sprite AvatarImage;
    public Color32 ClassCardTint;
    public Color32 ClassRibbonsTint;
}
