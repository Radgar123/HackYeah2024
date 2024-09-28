using _Code.Scripts.Interface;
using Hearings.SaveSystem;
using UnityEngine;

namespace _Code.Scripts.WorldInteraction
{
    public class PointInteractable : MonoBehaviour, IInteract
    {
        [SerializeField] private int _pointId;
        
        public void Interact()
        {
            if (SaveManager.Instance)
            {
                SaveManager.Instance.RemoveObject(_pointId);
            }
        }
    }
}