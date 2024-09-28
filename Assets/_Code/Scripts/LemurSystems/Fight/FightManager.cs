using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Code.Scripts.LemurSystems.Fight
{
    public class FightManager : MonoBehaviour
    {
        [SerializeField] private CharactersManager charactersManager;

        [Button]
        public void StartFight()
        {
            ApplyPlayerAbility();
            ApplyEnemyAbility();
            StartCoroutine(MainFight());
        }

        private IEnumerator MainFight()
        {
            while (true)
            {
                if (PlayerAttack()) break;
                yield return null;
                if (EnemyAttack()) break;
                yield return null;
            }
            Debug.Log("Zakonczono walke");
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
        public bool PlayerAttack()
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
                return true;
            }
            
            foreach (LemurManager eLemur in enemyLemurs)
            {
                eLemur.DealDamage = combinedDamage;
                if (eLemur.scriptableCharacter.energy <= 0)
                {
                    return true;
                }
                return false;
            }

            return true;
        }

        [Button]
        public bool EnemyAttack()
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
                return true;
            }

            bool wasPLemurKiller = false;
            foreach (LemurManager pLemur in playerLemurs)
            {
                pLemur.DealDamage = combinedDamage;
                if (pLemur.scriptableCharacter.energy <= 0) wasPLemurKiller = true;
            }

            return wasPLemurKiller;
        }
    }
}