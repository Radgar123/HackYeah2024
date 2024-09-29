using UnityEngine;

using System.Collections;
public class TextureSwitcher : MonoBehaviour
{
    public Renderer objectRenderer; 
    public Texture BlinkTexture; 
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
                
                float waitTime = Random.Range(2f, 5f);
                yield return new WaitForSeconds(waitTime);

                
                objectRenderer.material.mainTexture = BlinkTexture;

                
                yield return new WaitForSeconds(5f);

                
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
        objectRenderer.material.mainTexture = BlinkTexture; 
    }

    public void ResetTexture()
    {
        isChangingTexture = true; 
        objectRenderer.material.mainTexture = originalTexture; 
    }

    private void Update()
    {
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lemur_SleepIdle")) 
        {
            StartPermanentTexture();
        }
        else
        {
            ResetTexture();
        }
    }
}
