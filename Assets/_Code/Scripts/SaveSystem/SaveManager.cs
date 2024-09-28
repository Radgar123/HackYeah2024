using System;
using Hearings.Core.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;
using UnityEngine.TextCore.Text;

namespace Hearings.SaveSystem
{
    public class SaveManager : Singleton<SaveManager>
    {
        public PlayerData playerData;
        
        private string savePath;

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
            }
            else
            {
                playerData = new PlayerData();
                
                Save();
                Debug.Log("Save new created file");
            }
        }

        #endregion
        

        #region Save System Operations

        [Button]
        public void Save()
        {
            string jsonString = JsonUtility.ToJson(playerData);
            File.WriteAllText(SavePath, jsonString);
            Debug.Log(savePath);
        }

        [Button]
        public void Load()
        {
            string jsonString = File.ReadAllText(SavePath); 
            playerData = JsonUtility.FromJson<PlayerData>(jsonString);
        }
        
        public void Clear()
        {
            playerData = new PlayerData();
        }

        #endregion

        #region CharacterManipulate

        public void Add(CharacterData character)
        {
            if (playerData.charactersArray.Count > 5)
            {
                Debug.Log("Array is Full");
                return;
            }
            
            playerData.charactersArray.Add(character);
        }

        public void Remove(CharacterData character)
        {
            if (playerData.charactersArray.Contains(character))
            {
                playerData.charactersArray.Remove(character);
            }
        }

        #endregion
       
    }
}