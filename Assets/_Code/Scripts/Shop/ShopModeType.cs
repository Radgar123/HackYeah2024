using _Code.Scripts.WorldInteraction;
using UnityEngine;

namespace Hearings.Shop
{
    public class ShopModeType : MonoBehaviour
    {
        [SerializeField] private ObjectInteractable _interactable = new ObjectInteractable();

        public ObjectInteractable currentInteractable = null;
        
    }
}