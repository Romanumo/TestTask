using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnGameEvent();

public class GameManager : MonoBehaviour
{
    const string ATTEMPTS_AMOUNT_PREFS = "AttemptAmount";

    BallMovement player;
    SceneScroller sceneScroller;
    int difficulty;
    bool isGameGoing = false;

    public int attemptAmount { get; private set; }
    public float timePassed { get; private set; }

    public OnGameEvent onPlayerDeath;
    public OnGameEvent onGameRestart;
    public OnGameEvent onGameStart;

    void Start()
    {
        sceneScroller = GameManager.FindObjectOfType<SceneScroller>();
        attemptAmount = PlayerPrefs.GetInt(ATTEMPTS_AMOUNT_PREFS);

        onPlayerDeath += StopGame;
        onGameRestart += () => RestartSettings(difficulty);

        onGameStart += LoadPlayer;
        onGameStart += () => SetUpGame(difficulty);
    }

    void Update()
    {
        if (isGameGoing)
        {
            UpdateGameTimer();
            sceneScroller.ScrollScene();
        }
    }

    public void GameOver()
    {
        if (onPlayerDeath != null)
            onPlayerDeath.Invoke();
    }

    public void Restart()
    {
        if (onGameRestart != null)
            onGameRestart.Invoke();
    }

    public void StartGame()
    {
        if (onGameStart != null)
            onGameStart.Invoke();
    }

    void RestartSettings(int difficulty)
    {
        SetUpGame(difficulty);
        sceneScroller.RestartScroller();
        player.enabled = true;
    }

    void StopGame()
    {
        attemptAmount++;
        PlayerPrefs.SetInt(ATTEMPTS_AMOUNT_PREFS, attemptAmount);

        isGameGoing = false;
        player.enabled = false;
    }

    void SetUpGame(int difficulty)
    {
        timePassed = 0;
        isGameGoing = true;
        sceneScroller.ScrollSpeed(difficulty);
    }

    void LoadPlayer()
    {
        GameObject Player = Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject))) as GameObject;
        Player.transform.position = new Vector3(0, 0, 0);
        player = Player.GetComponent<BallMovement>();
    }

    public void ChangeDifficulty(int difficulty) => this.difficulty = Mathf.Clamp(difficulty, 0, 2);

    void UpdateGameTimer() => timePassed += Time.deltaTime;
}
