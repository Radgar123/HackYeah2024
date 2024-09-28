using System.Collections.Generic;
using UnityEngine;

namespace Hearings.SaveSystem
{
    [System.Serializable]
    public struct PlayerData
    {
        public List<CharacterData> charactersArray;

        [Header("Economic")] 
        public int cash;

        public PlayerData(int cash, List<CharacterData> charactersArray)
        {
            this.cash = cash;
            this.charactersArray = charactersArray;
        }
    }
}