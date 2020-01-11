using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LorePage : MonoBehaviour
{
    public RawImage PageImage;
    public RawImage BackgroundImage;
    public Texture[] LoreImages;

    private int _currentImage = 0;

    private bool _initialImageLoaded = false;
    private float _initialImageAlpha = 0f;
    private float _initialImageFadeSpeed = 1.2f;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneHelper.NextScene();
        }

        if (!_initialImageLoaded)
        {
            // Fade in the initial image
            _initialImageAlpha += _initialImageFadeSpeed * Time.deltaTime;
            PageImage.color = new Color(PageImage.color.r,
                PageImage.color.g,
                PageImage.color.b,
                Mathf.Clamp01(_initialImageAlpha));

            if (Math.Abs(PageImage.color.a - 1f) < 1e-4)
            {
                // Remove the background image and proceed with lore scene
                BackgroundImage.enabled = false;
                _initialImageLoaded = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Keep tabbing through 
                if (_currentImage < LoreImages.Length - 1)
                {
                    _currentImage++;
                    PageImage.texture = LoreImages[_currentImage];
                }
                else
                {
                    SceneHelper.NextScene();
                }
            }
        }
    }
}
