using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Code.Scripts.LemurSystems.Fight
{
    public class FightManager : MonoBehaviour
    {
        [SerializeField] private CharactersManager charactersManager;

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
        public void PlayerAttack()
        {
            List<LemurManager> playerLemurs = charactersManager.GetPlayerLemurs();
            List<LemurManager> enemyLemurs = charactersManager.GetEnemyLemurs();

            //calculate damage
            float combinedDamage = 0;
            foreach (LemurManager pLemur in playerLemurs)
            {
                combinedDamage += pLemur.scriptableCharacter.attack;
            }
            
            //przeiterowac przez przeciwników i zadać im dmg
            //bije tylko pierwszego enemiego
            if (enemyLemurs.Count <= 0)
            {
                Debug.LogError("Brak enemies");
                return;
            }
            
            foreach (LemurManager eLemur in enemyLemurs)
            {
                eLemur.DealDamage = combinedDamage;
                break;
            }
        }

        [Button]
        public void EnemyAttack()
        {
            List<LemurManager> playerLemurs = charactersManager.GetPlayerLemurs();
            List<LemurManager> enemyLemurs = charactersManager.GetEnemyLemurs();
            
            //calculate damage - tylko z pierwszy walczymy więć tylko z pierwszego pobieram damage
            float combinedDamage = 0;
            foreach (LemurManager eLemur in enemyLemurs)
            {
                combinedDamage += eLemur.scriptableCharacter.attack;
                break;
            }
            
            //przeiterowac przez playera i zadać im dmg
            if (playerLemurs.Count <= 0)
            {
                Debug.LogError("Brak lemurów gracza");
                return;
            }
            
            foreach (LemurManager pLemur in playerLemurs)
            {
                pLemur.DealDamage = combinedDamage;
            }
        }
    }
}