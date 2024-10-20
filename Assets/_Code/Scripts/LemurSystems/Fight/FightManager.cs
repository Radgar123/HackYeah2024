﻿using System;
using System.Collections;
using System.Collections.Generic;
using _Code.Scripts.LemurSystems.FightUI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Code.Scripts.LemurSystems.Fight
{
    public class FightManager : MonoBehaviour
    {
        [SerializeField] private CharactersManager charactersManager;
        [SerializeField] private FightUIAnimations fightUIAnimations;

        public UnityEvent winEvent;
        public UnityEvent loseEvent;


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);
            StartFight();
        }

        [Button]
        public void StartFight()
        {
            ApplyPlayerAbility();
            ApplyEnemyAbility();
            StartCoroutine(MainFight());
        }
        
        private IEnumerator MainFight()
        {
            KilledType attack;
            
            while (true)
            {
                //Debug.Log("Player atakuje");
                attack = PlayerAttack();
                if (attack == KilledType.Enemy) break;
                yield return new WaitForSeconds(1f); //mozna dodac chwile przerwy

                //Debug.Log("Enemy atakuje");
                attack = EnemyAttack();
                if (attack == KilledType.Player) break;
                yield return new WaitForSeconds(1f); //mozna dodac chwile przerwy
            }
            //Debug.Log("Zakonczono walke");
            
            //Sprawdzić kto wygrał i switch case z KilledType
            //Wywalić też z Lemur Manager sprawdzanie dmg i zabijanie
            
            switch (attack)
            {
                case KilledType.Enemy:
                    //Debug.Log("Zabito enemy");
                    yield return new WaitForEndOfFrame();
                    yield return new WaitUntil(() => !fightUIAnimations.sequence.IsPlaying());
                    DetectAndKill(charactersManager.GetEnemyLemurs());
                    winEvent.Invoke();
                    //Debug.Log("Odczekano enemy");
                    break;
                case KilledType.Player:
                    //Debug.Log("Zabito gracza");
                    yield return new WaitForEndOfFrame();
                    yield return new WaitUntil(() => !fightUIAnimations.sequence.IsPlaying());
                    DetectAndKill(charactersManager.GetPlayerLemurs());
                    loseEvent.Invoke();
                    //Debug.Log("Odczekano player");
                    break;
                default:
                    Debug.LogError("exception");
                    break;
            }
        }

        private void DetectAndKill(List<LemurManager> lemurs)
        {
            foreach (LemurManager lemur in lemurs)
            {
                if (lemur.scriptableCharacter.energy <= 0)
                {
                    lemur.KillMe();
                }
            }
        }
        
        [Button]
        public void ApplyPlayerAbility()
        {
            List<LemurManager> playerLemurs = charactersManager.GetPlayerLemurs();
            if (playerLemurs.Count <= 0)
            {
                Debug.LogError("Brak lemurów playera");
                return;
            }
            
            foreach (LemurManager pLemur in playerLemurs)
            {
                pLemur.SpecialAbility();
                break;
            }
        }
        
        [Button]
        public void ApplyEnemyAbility()
        {
            List<LemurManager> enemyLemurs = charactersManager.GetEnemyLemurs();
            
            if (enemyLemurs.Count <= 0)
            {
                Debug.LogError("Brak lemurów enemy");
                return;
            }
            
            foreach (LemurManager eLemur in enemyLemurs)
            {
                eLemur.SpecialAbility();
                break;
            }
        }
        
        [Button]
        public KilledType PlayerAttack()
        {
            List<LemurManager> playerLemurs = charactersManager.GetPlayerLemurs();
            List<LemurManager> enemyLemurs = charactersManager.GetEnemyLemurs();

            //calculate damage
            int combinedDamage = 0;
            foreach (LemurManager pLemur in playerLemurs)
            {
                combinedDamage += pLemur.scriptableCharacter.attack;
            }
            
            //przeiterowac przez przeciwników i zadać im dmg
            //bije tylko pierwszego enemiego
            if (enemyLemurs.Count <= 0)
            {
                Debug.Log("Brak enemies");
                return KilledType.Exception;
            }

            StartCoroutine(SendDamageDataToAnim(charactersManager.GetEnemyLemurs()[0].transform.position,
                combinedDamage, playerLemurs));

            foreach (LemurManager eLemur in enemyLemurs)
            {
                eLemur.DealDamage = combinedDamage;
                if (eLemur.scriptableCharacter.energy <= 0)
                {
                    return KilledType.Enemy;
                }
                return KilledType.NoOne;
            }

            return KilledType.Exception;
        }

        private IEnumerator SendDamageDataToAnim(Vector3 endPos, int combinedDamage,  List<LemurManager> lemurs)
        {
            if (combinedDamage <= 0)
            {
                Debug.LogWarning("Wykonano atak ale nie ma lemurkow");
                yield break;
            }
            
            foreach (LemurManager lemur in lemurs)
            {
                fightUIAnimations.SetupAndStartPreAttackAnim(lemur.transform.position,lemur.scriptableCharacter.attack.ToString());
            }

            yield return new WaitForSeconds(fightUIAnimations.durationOne);
            
            fightUIAnimations.SetupAndStartAttackAnim(endPos,combinedDamage.ToString());
        }
        
        [Button]
        public KilledType EnemyAttack()
        {
            List<LemurManager> playerLemurs = charactersManager.GetPlayerLemurs();
            List<LemurManager> enemyLemurs = charactersManager.GetEnemyLemurs();
            
            //calculate damage - tylko z pierwszy walczymy więć tylko z pierwszego pobieram damage
            int combinedDamage = 0;
            foreach (LemurManager eLemur in enemyLemurs)
            {
                combinedDamage += eLemur.scriptableCharacter.attack;
                break;
            }
            
            //przeiterowac przez playera i zadać im dmg
            if (playerLemurs.Count <= 0)
            {
                Debug.Log("Brak lemurów gracza");
                return KilledType.Player;
            }

            KilledType wasPLemurKiller = KilledType.NoOne;
            foreach (LemurManager pLemur in playerLemurs)
            {
                pLemur.DealDamage = combinedDamage;
                
                if (pLemur.scriptableCharacter.energy <= 0) pLemur.KillMe();
            }

            int pLCount = playerLemurs.Count;
            int j = 0;
            for (int i = 0; i < playerLemurs.Count; i++)
            {
                if (playerLemurs[i] == null) j++;
            }

            if (j == pLCount) return KilledType.Player;

            playerLemurs = charactersManager.GetPlayerLemurs();
            if (playerLemurs.Count > 0) wasPLemurKiller = KilledType.NoOne;

            return wasPLemurKiller;
        }
    }

    public enum KilledType
    {
        NoOne,
        Exception,
        Player,
        Enemy
    }
}