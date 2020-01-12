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

    public CanvasGroup WinScreenGroup;
    public Text WinScoreText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.SetCanMove(true);
    }

    private IEnumerator DelayedGameOverScreen()
    {
        yield return new WaitForSeconds(1f);

        GameOverScreenGroup.alpha = 1;
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
            ObjectiveCanvasGroup.alpha = 0;
            GameOverScreenGroup.alpha = 0;
            WinScreenGroup.alpha = 1;

            WinScoreText.text = $"Your score: {_timerTime:0.00}s";
            TimerText.text = "";

            PlayerController.SetCanMove(false);
            _gameOver = true;
        }

        // If NPC has detected you, YOU LOSE
        if (_npcDetection)
        {
            ObjectiveCanvasGroup.alpha = 0;
            WinScreenGroup.alpha = 0;

            PlayerController.SetCanMove(false);

            StartCoroutine(DelayedGameOverScreen());

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

    public bool IsGameOver()
    {
        return _gameOver;
    }
}
