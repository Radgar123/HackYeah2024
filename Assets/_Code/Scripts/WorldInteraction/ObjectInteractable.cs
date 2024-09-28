using System;
using System.Collections;
using System.Numerics;
using _Code.Scripts.Interface;
using Hearings.SaveSystem;
using Hearings.Shop;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Code.Scripts.WorldInteraction
{
    public class ObjectInteractable : MonoBehaviour, IInteract
    {
        [SerializeField] private int ShopID;

        private Vector3 _basePosition;
        private Vector3 _interctPosition;
        public Vector3 _OffsetPosition;
        
        public bool _Interacting;
        public bool _isInteracting = true;
        
        private ShopModeType _shopMode;

        private void Awake()
        {
            _shopMode = FindObjectOfType<ShopModeType>();
            
            _basePosition = transform.position;
            _interctPosition = new Vector3(_basePosition.x + _OffsetPosition.x, 
                _basePosition.y + _OffsetPosition.y, _basePosition.z + _OffsetPosition.z);
        }

        private void Update()
        {
            if (_Interacting)
            {
                if (transform.position != _interctPosition)
                {
                    transform.position = 
                        Vector3.MoveTowards(transform.position, 
                            _interctPosition, Time.deltaTime * 2);
                }
            }
            else
            {
                if (transform.position != _basePosition)
                {
                    transform.position = 
                        Vector3.MoveTowards(transform.position, 
                            _basePosition, Time.deltaTime * 2);
                }
            }
        }

        public void Interact()
        {
            if (_isInteracting)
            {
                Debug.Log("Interact");
                _Interacting = !_Interacting;

                if (SaveManager.Instance && ShopManager.Instance)
                {
                    ShopManager.Instance.objectToBuy = ShopID;
                }   
            }
        }
    }
}