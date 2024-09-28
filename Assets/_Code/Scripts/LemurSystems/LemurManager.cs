using System;
using UnityEngine;

namespace _Code.Scripts.LemurSystems
{
    public class LemurManager : MonoBehaviour
    {
        [HideInInspector] public ScriptableCharacter scriptableCharacter;

        public int DealDamage
        {
            set
            {
                scriptableCharacter.energy -= value;
                if (scriptableCharacter.energy <= 0) KillMe();
            }
        }

        private void KillMe()
        {
            Destroy(gameObject);
        }
    }
}