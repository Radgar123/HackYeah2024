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
    }
}