using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Code.Scripts.LemurSystems
{
    public class LemurSpawnSystem : MonoBehaviour
    {
        [SerializeField] private GameObject lemurObject;
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
                GameObject currentLemur = Instantiate(lemurObject,currSpawnPoints[i]);
                //currentLemur.GetComponent<LemurManager>().scriptableCharacter = scriptableCharacters[i];
            }
        }
    }
}