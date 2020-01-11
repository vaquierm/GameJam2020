using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public RawImage MenuBackground;
    public int FinalBackgroundAlpha = 190;
    private float _finalBackgroundA = 0f;

    public CanvasGroup Buttons;

    private bool _menuLoaded = false;
    private float _backgroundAlpha = 1f;
    private float _backgroundFadeSpeed = 0f;
    private float _buttonsAlpha = 0f;
    private float _buttonsFadeSpeed = 0f;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);

        _finalBackgroundA = FinalBackgroundAlpha / 255f;
        _backgroundFadeSpeed = 0.5f * (255 - FinalBackgroundAlpha) / 255f;
        _buttonsFadeSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_menuLoaded)
        {
            // Fade out the menu background
            _backgroundAlpha -= _backgroundFadeSpeed * Time.deltaTime;
            MenuBackground.color = new Color(MenuBackground.color.r, 
                MenuBackground.color.g, 
                MenuBackground.color.b,
                Mathf.Clamp01(_backgroundAlpha));

            // Fade in the buttons
            _buttonsAlpha += _buttonsFadeSpeed * Time.deltaTime;
            Buttons.alpha = Mathf.Clamp01(_buttonsAlpha);

            if (MenuBackground.color.a <= _finalBackgroundA)
            {
                _menuLoaded = true;
            }
        }
    }
}
