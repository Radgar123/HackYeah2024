using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Code.Scripts.LemurSystems.FightUI
{
    public class FightUIAnimations : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private float duration = 2f;
        [SerializeField] private Transform startPos;
        public TextMeshProUGUI newObj;
        public Sequence sequence;

        public void SetupAndStartAttackAnim(Vector3 endPos, string damage)
        {
            sequence = DOTween.Sequence();

            Image newImage = Instantiate(image.gameObject, image.gameObject.transform.parent).GetComponent<Image>();
            newObj = newImage.transform.GetComponentInChildren<TextMeshProUGUI>();
            newObj.text = damage;
            newObj.gameObject.SetActive(true);
            newImage.gameObject.SetActive(true);
            endPos = new Vector3(endPos.x, endPos.y, endPos.z - 1f);
            Tween tw1 = newObj.gameObject.transform.DOMove(endPos, duration);
            Tween tw2 = newImage.gameObject.transform.DOMove(endPos, duration);

            sequence.Append(tw1);
            sequence.Join(tw2);

            sequence.OnComplete(() =>
            {
                //Destroy(newObj.gameObject);
                Destroy(newImage.gameObject);
                Debug.Log("animacja ended");
            });

            sequence.Play();
        }
    }
}