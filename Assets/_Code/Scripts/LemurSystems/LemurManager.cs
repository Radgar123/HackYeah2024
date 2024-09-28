using UnityEngine;

namespace _Code.Scripts.LemurSystems
{
    public class LemurManager : MonoBehaviour
    {
        [HideInInspector] public ScriptableCharacter scriptableCharacter;
        
        public void LemurDie()
        {
            Destroy(gameObject);
        }
    }
}