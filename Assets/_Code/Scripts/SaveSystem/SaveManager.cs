using System;
using System.Collections.Generic;
using Hearings.Core.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;
using System.Linq;
using _Code.Scripts.Enemies;
using _Code.Scripts.LemurSystems;
using _Code.Scripts.WorldInteraction;
using Hearings.Shop;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Hearings.SaveSystem
{
    public class SaveManager : Singleton<SaveManager>
    {
        public PlayerData playerData;
        
        private string savePath;

        // List<CharacterData> _CharacterDatas = new List<CharacterData>();
        
        public UnityEvent OnSaveComplete;
        public UnityEvent OnLoadComplete;

        public GameObject[] loadedObjects = new GameObject[3];
        public GameObject[] instantiatedObjects = new GameObject[3];

        public string SavePath
        {
            get{return savePath;}
            set{savePath = value;}
        }

        #region Base Methods

        private void Awake()
        {
            SavePath = Application.persistentDataPath + "/save.json";
            
            if (File.Exists(SavePath))
            {
                Load();
                Debug.Log("Loading save file");

                List<GameObject> playersLemurs = new List<GameObject>();
                List<GameObject> enemiesLemurs = new List<GameObject>();

                foreach (var VARIABLE in loadedObjects)
                {
                    playersLemurs.Add(VARIABLE);
                }

                //To musi sie wykonac tylko na scenie z walką
                if(SceneManager.GetActiveScene().buildIndex == 2) enemiesLemurs.Add(EnemiesManager.Instance.GetRandomEnemy());
                if(SceneManager.GetActiveScene().buildIndex == 2) CharactersManager.Instance.LoadNewFight(playersLemurs,enemiesLemurs);
            }
            else
            {
                playerData = new PlayerData();
                playerData.charactersArray = new List<CharacterData>();
                
                for (int i = 0; i < 3; i++)
                {
                    CharacterData characterData = new CharacterData(-1,i);
                    playerData.charactersArray.Add(characterData);
                }
                
                
                Debug.Log("Test" + playerData.charactersArray.Count);
                Save();
                Debug.Log("Save new created file");
            }
        }

        private void OnDisable()
        {
            Save();
        }

        #endregion
        

        #region Save System Operations

        [Button]
        public void Save()
        {
            string jsonString = JsonUtility.ToJson(playerData);
            File.WriteAllText(SavePath, jsonString);
            Debug.Log(savePath);
            
            OnSaveComplete?.Invoke();
        }

        [Button]
        public void Load()
        {
            string jsonString = File.ReadAllText(SavePath); 
            playerData = JsonUtility.FromJson<PlayerData>(jsonString);
            
            LoadObjectArray();
            OnLoadComplete?.Invoke();
        }

        public void LoadObjectArray()
        {
            for (int i = 0; i < playerData.charactersArray.Count; i++)
            {
                if (playerData.charactersArray[i].spawnObjectId > -1)
                {
                    loadedObjects[i] = ShopManager.Instance.
                        _objectsInShop[playerData.charactersArray[i].spawnObjectId];
                    GameObject obj = Instantiate(loadedObjects[i], ShopManager.Instance.
                        pointsInShop[playerData.charactersArray[i].positionOnBoard]);
                    
                    obj.GetComponent<ObjectInteractable>()._isInteracting= false;
                    instantiatedObjects[i] = obj;
                }
            }
        }
        
        #endregion

        #region CharacterManipulate
        
        public void AddObjectToCharacter(int id)
        {
            var character = playerData.charactersArray.FirstOrDefault(obj => obj.spawnObjectId == -1);
            
            int index = playerData.charactersArray.IndexOf(character);
            character.spawnObjectId = id;
            playerData.charactersArray[index] = character;

            if (ShopManager.Instance)
            {
                loadedObjects[index] = ShopManager.Instance._objectsInShop[id];
                GameObject obj = Instantiate(loadedObjects[index], ShopManager.
                    Instance.pointsInShop[playerData.charactersArray[index].positionOnBoard]);
                obj.GetComponent<ObjectInteractable>()._isInteracting = false;
                instantiatedObjects[index] = obj;
            }
            
            Save();
        }

        [Button]
        public void Remove()
        {
            RemoveObject(0);
            Save();
        }
        
        public void RemoveObject(int positionId)
        {
            var playerDataCharacters = playerData.charactersArray[positionId];
            playerDataCharacters.spawnObjectId = -1;

            playerData.charactersArray[positionId] = playerDataCharacters;
            loadedObjects[positionId] = null;
            Destroy(instantiatedObjects[positionId]);
        }
        
        public void Clear()
        {
            playerData = new PlayerData();
        }

        #endregion
       
    }
}