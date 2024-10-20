using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Code.Scripts.LemurSystems.FightUI
{
    public class FightUIAnimations : MonoBehaviour
    {
        [SerializeField] private Image image;
        public float durationOne = .5f;
        public float durationTwo = .5f;
        [SerializeField] private Transform startPos;
        public Sequence sequence;

        public void SetupAndStartAttackAnim(Vector3 endPos, string combinedDamage)
        {
            sequence = DOTween.Sequence();
            
            Image newImage = Instantiate(image.gameObject, image.gameObject.transform.parent).GetComponent<Image>();
            TextMeshProUGUI textObject = newImage.transform.GetComponentInChildren<TextMeshProUGUI>();
            textObject.text = combinedDamage;
            
            endPos = new Vector3(endPos.x, endPos.y, endPos.z - 1f);
            Tween tw2 = newImage.gameObject.transform.DOMove(endPos, durationOne);

            sequence.Join(tw2);

            sequence.OnComplete(() =>
            {
                Destroy(newImage.gameObject);
                //Debug.Log("animacja ended");
            });

            textObject.gameObject.SetActive(true);
            newImage.gameObject.SetActive(true);
            
            sequence.Play();
        }
        
        public void SetupAndStartPreAttackAnim(Vector3 _startPos, string damage)
        {
            sequence = DOTween.Sequence();
        
            Image newImage = Instantiate(image.gameObject, image.gameObject.transform.parent).GetComponent<Image>();
            TextMeshProUGUI textObject = newImage.transform.GetComponentInChildren<TextMeshProUGUI>();
            textObject.text = damage;

            newImage.transform.position = _startPos;
            
            Tween tw2 = newImage.gameObject.transform.DOMove(startPos.position, durationTwo);
            sequence.Join(tw2);
        
            sequence.OnComplete(() =>
            {
                Destroy(newImage.gameObject);
                //Debug.Log("animacja ended");
            });
            
            textObject.gameObject.SetActive(true);
            newImage.gameObject.SetActive(true);
            
            sequence.Play();
        }
    }
}