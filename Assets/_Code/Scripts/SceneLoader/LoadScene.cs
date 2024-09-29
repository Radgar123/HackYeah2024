using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Code.Scripts.SceneLoader
{
    public class LoadScene : MonoBehaviour
    {
        public void LoadSceneById(int id)
        {
            SceneManager.LoadScene(id);
        }
    }
}