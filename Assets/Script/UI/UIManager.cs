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

    private int difficulty;

    public void StartGame()
    {
        menuWindow.SetActive(false);
        GlobalLibrary.gameManager.StartGame(difficulty);
    }

    public void ShowGameOverScreen()
    {
        gameOverWindow.SetActive(true);
        UpdateAttemptText();
        UpdateTimeText();
    }

    public void RestartGame()
    {
        GlobalLibrary.gameManager.RestartGame(difficulty);
        gameOverWindow.SetActive(false);
    }

    public void ChangeDifficulty(int difficulty) => this.difficulty = Mathf.Clamp(difficulty, 0, 2);

    void UpdateAttemptText() => attemptAmountText.text = "Количество попыток: " + GlobalLibrary.gameManager.attemptAmount;

    void UpdateTimeText() => timePassedText.text = "Время: " + Mathf.RoundToInt(GlobalLibrary.gameManager.timePassed) + " секунды";
}
