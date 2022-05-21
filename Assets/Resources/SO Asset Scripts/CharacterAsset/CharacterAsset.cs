using UnityEngine;
using System.Collections;

public enum CharClass{ Elf, Monk, Brass}

public class CharacterAsset : ScriptableObject 
{
	public CharClass Class;
	public string ClassName;
	public int MaxHealth = 30;
	public Sprite AvatarImage;
    public Color32 ClassCardTint;
    public Color32 ClassRibbonsTint;
}
