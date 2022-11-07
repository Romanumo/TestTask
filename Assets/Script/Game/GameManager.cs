using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDeath();

public class GameManager : MonoBehaviour
{
    const string ATTEMPTS_AMOUNT_PREFS = "AttemptAmount";

    BallMovement player;
    SceneScroller sceneScroller;
    bool isGameGoing = false;
    public int attemptAmount { get; private set; }
    public float timePassed { get; private set; }

    public OnDeath onPlayerDeath;

    void Start()
    {
        sceneScroller = GameManager.FindObjectOfType<SceneScroller>();
        attemptAmount = PlayerPrefs.GetInt(ATTEMPTS_AMOUNT_PREFS);

        onPlayerDeath += StopGame;
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

    void StopGame()
    {
        attemptAmount++;
        PlayerPrefs.SetInt(ATTEMPTS_AMOUNT_PREFS, attemptAmount);

        isGameGoing = false;
        player.enabled = false;
    }

    public void StartGame(int difficulty)
    {
        LoadPlayer();
        SetUpGame(difficulty);
    }

    public void RestartGame(int difficulty)
    {
        SetUpGame(difficulty);
        sceneScroller.RestartScroller();

        player.RestartSpeed();
        player.enabled = true;
        player.transform.position = Vector3.zero;
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

    void UpdateGameTimer() => timePassed += Time.deltaTime;
}
