using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;
    [SerializeField] private GameObject gameOverWindow;

    [SerializeField] private Text attemptAmountText;
    [SerializeField] private Text timePassedText;

    private void Start()
    {
        GlobalLibrary.gameManager.onPlayerDeath += ShowGameOverScreen;
        GlobalLibrary.gameManager.onGameRestart += () => GameOverUIState(false);
        GlobalLibrary.gameManager.onGameStart += () => MenuUIState(false);
    }

    void ShowGameOverScreen()
    {
        gameOverWindow.SetActive(true);
        UpdateAttemptText();
        UpdateTimeText();
    }

    void GameOverUIState(bool state) => gameOverWindow.SetActive(state);

    void MenuUIState(bool state) => menuWindow.SetActive(state);

    void UpdateAttemptText() => attemptAmountText.text = "Количество попыток: " + GlobalLibrary.gameManager.attemptAmount;

    void UpdateTimeText() => timePassedText.text = "Время: " + Mathf.RoundToInt(GlobalLibrary.gameManager.timePassed) + " секунды";
}
