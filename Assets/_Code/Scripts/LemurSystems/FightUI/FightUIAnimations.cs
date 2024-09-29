using DG.Tweening;
using TMPro;
using UnityEngine;
using VFolders.Libs;

namespace _Code.Scripts.LemurSystems.FightUI
{
    public class FightUIAnimations : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI obj;
        [SerializeField] private float duration = 2f;
        [SerializeField] private Transform startPos;
        public Sequence sequence;
        
        public void SetupAndStartAttackAnim(Vector3 endPos, string damage)
        {
            sequence = DOTween.Sequence();
            TextMeshProUGUI newObj = Instantiate(obj.gameObject, obj.gameObject.transform.parent).GetComponent<TextMeshProUGUI>();
            newObj.text = damage;
            newObj.gameObject.SetActive(true);
            endPos = new Vector3(endPos.x, endPos.y, endPos.z - 1f);
            Tween tw1 = newObj.gameObject.transform.DOMove(endPos, duration);

            sequence.Append(tw1);
            
            sequence.OnComplete(() =>
            {
                newObj.gameObject.Destroy();
            });
            
            sequence.Play();
        }
    }
}