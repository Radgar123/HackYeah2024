using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Code.Scripts.LemurSystems
{
    public class LemurManager : MonoBehaviour
    {
        [Tooltip("Visible only for debug purpose")]
        public ScriptableCharacter scriptableCharacter;

        [Header("UI Elements")] 
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private TextMeshProUGUI attackText;

        private void Start()
        {
            UpdateUI();
        }

        public float DealDamage
        {
            set
            {
                scriptableCharacter.energy -= value;
                if (scriptableCharacter.energy <= 0) KillMe();
                UpdateUI();
            }
        }

        public float AddEnergy
        {
            set
            {
                scriptableCharacter.energy += value;
                UpdateUI();
            }
        }

        public float MultiplyEnergy
        {
            set
            {
                scriptableCharacter.energy *= value; 
                UpdateUI();
            }
        }

        public float AddAttack
        {
            set
            {
                scriptableCharacter.attack += value; 
                UpdateUI();
            }
        }

        public float MultiplyAttack
        {
            set
            {
                scriptableCharacter.attack *= value; 
                UpdateUI();
            }
        }

        public int GetMyID()
        {
            switch (scriptableCharacter.characterType)
            {
                case CharacterType.Player:
                    return FindMyId(CharactersManager.Instance.GetPlayerLemurs());
                    break;
                case CharacterType.Enemy:
                    return FindMyId(CharactersManager.Instance.GetEnemyLemurs());
                    break;
                default:
                    Debug.LogError($"Error while returning ");
                    return -1;
            }
        }

        private int FindMyId(List<LemurManager> allCharacters)
        {
            for (int i = 0; i < allCharacters.Count; i++)
            {
                if (object.ReferenceEquals(allCharacters[i].gameObject, gameObject))
                {
                    return i;
                }
            }

            Debug.LogError("Nie znaleziono ID obiektu");
            return -1;
        }

        public void SpecialAbility()
        {
            List<LemurManager> playersLemurs = CharactersManager.Instance.GetPlayerLemurs();
            List<LemurManager> enemyLemurs = CharactersManager.Instance.GetEnemyLemurs();
            List<LemurManager> restOfLemurs = new List<LemurManager>();

            switch (scriptableCharacter.specialAbility)
            {
                case global::SpecialAbility.None:
                    Debug.Log("No ability");
                    break;

                case global::SpecialAbility.FriendsPower:
                    Debug.Log("FriendsPower");
                    foreach (LemurManager playersLemur in playersLemurs)
                    {
                        if (!object.ReferenceEquals(playersLemur, this))
                        {
                            restOfLemurs.Add(playersLemur);
                        }
                    }

                    //add buff to all of yours available allies
                    foreach (LemurManager lemurManager in restOfLemurs)
                    {
                        lemurManager.AddAttack = 1 * lemurManager.scriptableCharacter.multiplier;
                    }

                    break;

                case global::SpecialAbility.FriendsEnergy:
                    Debug.Log("FriendsEnergy");
                    foreach (LemurManager playersLemur in playersLemurs)
                    {
                        if (!object.ReferenceEquals(playersLemur, this))
                        {
                            restOfLemurs.Add(playersLemur);
                        }
                    }

                    int lemurNo = Random.Range(0, restOfLemurs.Count - 1);
                    restOfLemurs[lemurNo].AddEnergy =
                        2 * restOfLemurs[lemurNo].scriptableCharacter.multiplier;
                    break;

                case global::SpecialAbility.AttackDebuff:
                    foreach (LemurManager lemurManager in playersLemurs)
                    {
                        lemurManager.MultiplyAttack = 0.75f * lemurManager.scriptableCharacter.multiplier;
                    }

                    break;

                case global::SpecialAbility.EnergyDebuff:
                    //Zeby zadziałało to Ablility enemy musi wykonać się pierwsze
                    int lemurNumber = Random.Range(0, playersLemurs.Count - 1);
                    playersLemurs[lemurNumber].KillMe();
                    break;

                case global::SpecialAbility.HeBig:
                    foreach (LemurManager enemyLemur in enemyLemurs)
                    {
                        if (object.ReferenceEquals(enemyLemur, this))
                        {
                            enemyLemur.MultiplyEnergy = 1.1f * enemyLemur.scriptableCharacter.multiplier;
                        }
                    }

                    break;
                default:
                    Debug.LogError("Unsupported ability");
                    return;
            }
        }

        public void KillMe()
        {
            Destroy(gameObject);
        }

        private void UpdateUI()
        {
            string e = scriptableCharacter.energy.ToString("0");
            string a = scriptableCharacter.attack.ToString("0");
            
            energyText.text = $"Energy: {e}";
            attackText.text = $"Attack: {a}";
        }
    }
}