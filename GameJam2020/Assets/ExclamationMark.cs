using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMark : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    
    private bool hasFired = false;
    
    // Start is called before the first frame update
    void Start()
    {    
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }

    public void Enable()
    {
        if (!hasFired)
        {
            hasFired = true;
            spriteRenderer.enabled = true;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
