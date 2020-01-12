using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController PlayerController;

    public CanvasGroup ObjectiveCanvasGroup;
    private bool _objectiveVisible = true;
    private float _objectiveAlpha = 1f;
    private float _objectiveFadeSpeed = 1f;

    public Text TimerText;
    private float _timerTime = 0f;

    private bool _winColliderHit = false;

    private bool _npcDetection = false;
    private bool _gameOver = false;

    public CanvasGroup GameOverScreenGroup;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.SetCanMove(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver)
        {
            return;
        }

        // If win collider has been hit, YOU WIN
        if (_winColliderHit)
        {
            Debug.Log("WIN");
        }

        // If NPC has detected you, YOU LOSE
        if (_npcDetection)
        {
            ObjectiveCanvasGroup.alpha = 0;
            GameOverScreenGroup.alpha = 1;

            PlayerController.SetCanMove(false);

            _gameOver = true;
            return;
        }

        // Update timer
        _timerTime += Time.deltaTime;
        TimerText.text = $"Time: {_timerTime:0.00}s";

        if (_objectiveVisible)
        {
            // Keep text visible for 7 seconds
            if (_timerTime >= 7f)
            {
                // Fade out the text
                _objectiveAlpha -= _objectiveFadeSpeed * Time.deltaTime;
                ObjectiveCanvasGroup.alpha = Mathf.Clamp01(_objectiveAlpha);

                if (_objectiveAlpha <= 0f)
                {
                    _objectiveVisible = false;
                }
            }
        }
    }

    public void SetWinColliderHit(bool hit)
    {
        _winColliderHit = hit;
    }

    public void SetNPCDetection(bool detect)
    {
        _npcDetection = detect;
    }
}
