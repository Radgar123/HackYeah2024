using System.Collections;
using _Code.Scripts.SceneTransitionScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Code.Scripts.SceneLoader
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private UIAnimations _uiControllerAnimations;
        
        public void LoadSceneById(int id)
        {
            _uiControllerAnimations.UITransitionFadeIn();
            StartCoroutine(DelayedLoadScene(id));
        }

        IEnumerator DelayedLoadScene(int id)
        {
            yield return new WaitForSeconds(2.1f);
            SceneManager.LoadScene(id);
        }
    }
}