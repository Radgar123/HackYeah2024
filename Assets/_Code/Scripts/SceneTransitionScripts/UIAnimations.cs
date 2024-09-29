using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Code.Scripts.SceneTransitionScripts
{
    public class UIAnimations : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform spawnPosIn;
        [SerializeField] private Transform spawnPosOut;
        [SerializeField] private GameObject leaf;
        [SerializeField] private float fadeDuration = 2f;
        
        [Button]
        public void UITransitionFadeIn()
        {
            Sequence sequence = DOTween.Sequence();
            
            GameObject currentLeaf = Instantiate(leaf, spawnPosIn);
            Tween tw1 = currentLeaf.transform.DORotate(new Vector3(0, 0, -180), fadeDuration);
            Tween tw2 = currentLeaf.transform.DOMove(new Vector3(1000, 500, 0), fadeDuration);
            Tween tw3 = currentLeaf.transform.DOScale(new Vector3(2, 2, 2), fadeDuration);

            sequence.Append(tw1);
            sequence.Join(tw2);
            sequence.Join(tw3);

            sequence.OnComplete(() =>
            {
                Debug.Log("Scene change");
            });

            sequence.Play();
        }
        
        [Button]
        public void UITransitionFadeOut()
        {
            Sequence sequence = DOTween.Sequence();
            
            GameObject currentLeaf = Instantiate(leaf, spawnPosOut);
            currentLeaf.transform.localScale = new Vector3(2, 2, 2);
            
            Tween tw1 = currentLeaf.transform.DORotate(new Vector3(0, 0, -180), fadeDuration);
            Tween tw2 = currentLeaf.transform.DOMove(new Vector3(-500, 500, 0), fadeDuration);
            Tween tw3 = currentLeaf.transform.DOScale(new Vector3(.5f, .5f, .5f), fadeDuration);

            sequence.Append(tw1);
            sequence.Join(tw2);
            sequence.Join(tw3);

            sequence.OnComplete(() =>
            {
                Debug.Log("Scene loaded");
            });

            sequence.Play();
        }
    }
}