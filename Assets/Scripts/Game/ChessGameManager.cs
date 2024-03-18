using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class ChessGameManager : MonoBehaviour
{

    //public CheckScore CheckScore { get; private set; }
    //CheckScore = GetComponentInChildren<AudioManager>();

    public static ChessGameManager MasterSingleton { get; private set; }

    public CheckScore CheckScore { get; private set; }
    public DisplaySquares DisplaySquares { get; private set; }
    public PlayerAction PlayerAction { get; private set; }

    private void Awake()
    {
        if (MasterSingleton != null && MasterSingleton != this)
        {
            Destroy(this);
            return;
        }

        MasterSingleton = this;

        CheckScore = GetComponentInChildren<CheckScore>();
        DisplaySquares = GetComponentInChildren<DisplaySquares>();
        PlayerAction = GetComponentInChildren<PlayerAction>();
    }

    public bool isWinner = false;
    public bool gameOver = false;

    private int[] klon;

    public GameObject GameOver;
    public GameObject solutions;
    public GameObject queenPrefab;

    private Camera mainCamera;
    public GameObject inGame;

    // Define the chessboard dimensions
    public int rows = 8;
    public int columns = 8;
    public int queenCounter = 8;
    public int unThreatenedSquares = 64;

    public TextMeshProUGUI timerText;
    public float elapsedTime = 0f;

    public int score = 0;

    public TextMeshProUGUI currentScoreText;
    public GameObject replayButton;
    public GameObject backButton;
    public GameObject backArrowButton;

    public bool isRunning = true;

    public bool[,] threatenedSquares = new bool[8, 8];

    public List<GameObject> spawnedQueens = new();

    void Start()
    {
        mainCamera = Camera.main;

        Utilites.StartTimer();

        // Initialize the chessboardSquares array
        DisplaySquares.InitializeChessboardSquares();
    }

    void Update()
    {
        if (unThreatenedSquares == 0)
        {
            GameEnded();
        }
        else if (unThreatenedSquares < 0)
        {
            Debug.Log("Error with unthreatened square count calculations occured.");
        }
        else
        {
            elapsedTime += Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(elapsedTime);
            score = (int)Mathf.Round(elapsedTime);

            if (DisplayScore.areScoresDisplayed) // Her çevrimde iþlenmesin diye
            {
                DisplayScore.HideScores();
            }

            // Kullanýcý tahtaya týkladýðýnda çalýþýr
            PlayerAction.TouchBoard(mainCamera);
        }
    }

    private void GameEnded()
    {
        if (!DisplayScore.areScoresDisplayed) // Her çevrimde iþlenmesin diye
        {
            CheckScore.ManageScore(score);
            DisplayScore.ShowScore();
        }
        replayButton.SetActive(true);
        backButton.SetActive(true);
        backArrowButton.SetActive(false);
    }
}
