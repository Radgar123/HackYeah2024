using System.Collections;
using System.Collections.Generic;
using _Code.Scripts.LemurSystems.FightUI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Code.Scripts.LemurSystems.Fight
{
    public class FightManager : MonoBehaviour
    {
        [SerializeField] private CharactersManager charactersManager;
        [SerializeField] private FightUIAnimations fightUIAnimations;
        
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
                attack = PlayerAttack();
                if (attack == KilledType.Enemy) break;
                yield return new WaitForSeconds(1f); //mozna dodac chwile przerwy

                attack = EnemyAttack();
                if (attack == KilledType.Player) break;
                yield return new WaitForSeconds(1f); //mozna dodac chwile przerwy
            }
            Debug.Log("Zakonczono walke");
            
            //Sprawdzić kto wygrał i switch case z KilledType
            //Wywalić też z Lemur Manager sprawdzanie dmg i zabijanie
            
            switch (attack)
            {
                case KilledType.Enemy:
                    Debug.Log("Czekanie enemy");
                    yield return new WaitForEndOfFrame();
                    yield return new WaitUntil(() => !fightUIAnimations.sequence.IsPlaying());
                    DetectAndKill(charactersManager.GetEnemyLemurs());
                    Debug.Log("Odczekano enemy");
                    break;
                case KilledType.Player:
                    //Debug.Log("Czekanie");
                    yield return new WaitForEndOfFrame();
                    yield return new WaitUntil(() => !fightUIAnimations.sequence.IsPlaying());
                    DetectAndKill(charactersManager.GetPlayerLemurs());
                    //Debug.Log("Odczekano");
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
                Debug.LogError("Brak enemies");
                return KilledType.Exception;
            }
            
            fightUIAnimations.SetupAndStartAttackAnim(charactersManager.GetEnemyLemurs()[0].transform.position,combinedDamage.ToString());

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
                Debug.LogError("Brak lemurów gracza");
                return KilledType.Player;
            }

            KilledType wasPLemurKiller = KilledType.NoOne;
            foreach (LemurManager pLemur in playerLemurs)
            {
                pLemur.DealDamage = combinedDamage;
                if (pLemur.scriptableCharacter.energy <= 0) wasPLemurKiller = KilledType.NoOne;
            }

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