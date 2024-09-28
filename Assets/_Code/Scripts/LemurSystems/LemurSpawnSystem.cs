using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Code.Scripts.LemurSystems
{
    public class LemurSpawnSystem : MonoBehaviour
    {
        [SerializeField] private GameObject lemurObject;
        [SerializeField] private List<Transform> spawnPoints;

        #region Debug

        [Header("Debug")]
        [SerializeField] private List<ScriptableCharacter> debugCharacters;
        
        [Button]
        public void DebugSpawner()
        {
            SpawnLemurs(debugCharacters);
        }

        #endregion

        public void SpawnLemurs(List<ScriptableCharacter> scriptableCharacters)
        {
            if (scriptableCharacters.Count > 5)
            {
                Debug.LogError($"Over 5 lemurs to spawn, quiting");
                return;
            }
    
            for (int i = 0; i < scriptableCharacters.Count; i++)
            {
                GameObject currentLemur = Instantiate(lemurObject,spawnPoints[i]);
                currentLemur.GetComponent<LemurManager>().scriptableCharacter = scriptableCharacters[i];
            }
        }
    }
}