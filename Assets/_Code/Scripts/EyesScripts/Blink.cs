using UnityEngine;

using System.Collections;
public class TextureSwitcher : MonoBehaviour
{
    public Renderer objectRenderer; 
    public Texture alternativeTexture; 
    private Texture originalTexture; 

    private Animator animator; 
    private bool isChangingTexture = true; 

    private void Start()
    {
        
        originalTexture = objectRenderer.material.mainTexture;
        animator = GetComponentInParent<Animator>(); 
        StartCoroutine(SwitchTexture());
    }

    private IEnumerator SwitchTexture()
    {
        while (true)
        {
            if (isChangingTexture)
            {
                
                float waitTime = Random.Range(3f, 7f);
                yield return new WaitForSeconds(waitTime);

                
                objectRenderer.material.mainTexture = alternativeTexture;

                
                yield return new WaitForSeconds(0.2f);

                
                objectRenderer.material.mainTexture = originalTexture;
            }
            else
            {
                yield return null; 
            }
        }
    }

    public void StartPermanentTexture()
    {
        isChangingTexture = false; 
        objectRenderer.material.mainTexture = alternativeTexture; 
    }

    public void ResetTexture()
    {
        isChangingTexture = true; 
        objectRenderer.material.mainTexture = originalTexture; 
    }

    private void Update()
    {
        // Przyk³ad: Wykrywanie zmiany animacji
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TwojaAnimacja")) 
        {
            StartPermanentTexture();
        }
        else
        {
            ResetTexture();
        }
    }
}
