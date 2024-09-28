using System;
using _Code.Scripts.Interface;
using UnityEngine;

namespace _Code.Scripts.WorldInteraction
{
    public class WorldInteractor : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactableLayer;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray,out hit, _interactableLayer))
                {
                    if (hit.transform.gameObject.TryGetComponent<IInteract>(out IInteract interact))
                    {
                        interact.Interact();
                    }
                }
            }
        }
    }
}