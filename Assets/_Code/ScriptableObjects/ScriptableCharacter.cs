using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;



[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Character/characterData", order = 1)]
public class ScriptableCharacter : ScriptableObject
{
    [Header("Character Params")]
    public CharacterType characterType;
    
    public string characterName;
    
    public int energy;
    public int attack;
    
    public float attackSpeed;
    public float dodgeRatio;
    
    [Header("Shops Params")]
    public int characterCost;
    public int characterSell;
}

public enum CharacterType
{
    Player,
    Enemy
}
