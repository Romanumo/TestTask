using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLibrary : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    [SerializeField] private GameManager _gameManager;

    public static UIManager UIManager { get; private set; }
    [SerializeField] private UIManager _UIManager;

    void Awake()
    {
        gameManager = _gameManager;
        UIManager = _UIManager;
    }
}
