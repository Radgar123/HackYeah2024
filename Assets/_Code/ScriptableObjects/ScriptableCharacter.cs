using System.Collections;
using System.Collections.Generic;
using _Code.Scripts.LemurSystems;
using Sirenix.OdinInspector;
using UnityEngine;



[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Character/characterData", order = 1)]
public class ScriptableCharacter : ScriptableObject
{
    [Header("Character Params")]
    public CharacterType characterType;
    public SpecialAbility specialAbility;
    
    public string characterName;
    
    public int energy;
    public int attack;

    [Header("Shops Params")]
    public int characterCost;
    public int characterSell;

    [Header("Special ability")] 
    [Tooltip("Multiplies by hardcoded value (from GD as on trello) and adds it to the attack value")] 
    public int multiplier;

    [Header("UI Popup")] 
    public string abilityName;
    public string abilityDescription;
}

public enum CharacterType
{
    Player,
    Enemy
}

public enum SpecialAbility
{
    None,
    FriendsPower,
    FriendsEnergy,
    AttackDebuff,
    EnergyDebuff,
    HeBig
}