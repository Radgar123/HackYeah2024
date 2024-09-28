using System.Collections.Generic;
using _Code.Scripts.WorldInteraction;
using Hearings.Core.Singleton;
using Hearings.SaveSystem;
using UnityEngine;

namespace Hearings.Shop
{
    public class ShopManager : Singleton<ShopManager>
    {
        public List<GameObject> _objectsInShop;
        public Transform[] pointsInShop;

        public int objectToBuy;


        public void BuyObject()
        {
            SaveManager.Instance.AddObjectToCharacter(objectToBuy);
        }
        
        //public void 
    }
}