using System.Collections.Generic;
using Hearings.Core.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Code.Scripts.LemurSystems
{
    public class CharactersManager : Singleton<CharactersManager>
    {
        [SerializeField] private GameObject lemurPrefab;
        [SerializeField] private List<Transform> playerSpawnPoints;
        [SerializeField] private List<Transform> enemySpawnPoints;

        #region Debug

        [Header("Debug")]
        [SerializeField] private List<GameObject> debugCharacters;
        [SerializeField] private List<GameObject> debugEnemyCharacters;
        
        [Button]
        public void DebugSpawner()
        {
            LoadNewFight(debugCharacters,debugEnemyCharacters);
        }

        #endregion

        public List<LemurManager> GetPlayerLemurs()
        {
            List<LemurManager> playersLemurs = new List<LemurManager>();
            foreach (Transform parent in playerSpawnPoints)
            {
                LemurManager lm = parent.GetComponentInChildren<LemurManager>();
                if (lm == null) continue;
                playersLemurs.Add(lm);
            }

            return playersLemurs;
        }
        
        public List<LemurManager> GetEnemyLemurs()
        {
            List<LemurManager> enemyLemurs = new List<LemurManager>();
            foreach (Transform parent in enemySpawnPoints)
            {
                foreach (Transform child in parent)
                {
                    LemurManager lm = child.GetComponentInChildren<LemurManager>();
                    if (lm == null) continue;
                    enemyLemurs.Add(lm);
                }
            }

            return enemyLemurs;
        }

        public void LoadNewFight(List<GameObject> playerCharacters, List<GameObject> enemyCharacters)
        {
            InitSpawnLemurs(playerCharacters);
            InitSpawnLemurs(enemyCharacters);
        }
        
        private void InitSpawnLemurs(List<GameObject> characters)
        {
            if (characters.Count <= 0)
            {
                Debug.LogWarning("0 or less lemurs to spawn");
                return;
            }
            
            switch (characters[0].GetComponent<LemurManager>().scriptableCharacter.characterType)
            {
                case CharacterType.Player:
                    SpawnLemurs(characters,playerSpawnPoints);
                    break;
                case CharacterType.Enemy:
                    SpawnLemurs(characters,enemySpawnPoints);
                    break;
                default:
                    Debug.LogError("No character type, or unsupported");
                    break;
            }
        }

        private void SpawnLemurs(List<GameObject> characters,List<Transform> currSpawnPoints)
        {
            if (characters.Count > currSpawnPoints.Count)
            {
                Debug.LogError($"Over {currSpawnPoints.Count} lemurs to spawn, quiting - value driven by spawnPoints");
                return;
            }
    
            for (int i = 0; i < characters.Count; i++)
            {
                GameObject currentLemur = Instantiate(lemurPrefab,currSpawnPoints[i]);
                currentLemur.GetComponent<LemurManager>().scriptableCharacter =
                    Instantiate(characters[i].GetComponent<LemurManager>().scriptableCharacter);
                //currentLemur.GetComponent<LemurManager>().scriptableCharacter = scriptableCharacters[i];
            }
        }
    }
}