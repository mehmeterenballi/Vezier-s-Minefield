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
    public DisplayScore DisplayScore { get; private set; }
    public Utilities Utilities { get; private set; }
    public LocalizeGame LocalizeGame { get; private set; }

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
        DisplayScore = GetComponentInChildren<DisplayScore>();
        Utilities = GetComponentInChildren<Utilities>();
        LocalizeGame = GetComponentInChildren<LocalizeGame>();
    }

    public bool isWinner = false;
    public bool gameOver = false;

    private int[] klon;

    public GameObject GameOver;
    public GameObject solutions;
    public GameObject queenPrefab;

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
        MasterSingleton.Utilities.StartTimer();

        // Initialize the chessboardSquares array
        MasterSingleton.DisplaySquares.InitializeChessboardSquares();
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
            MasterSingleton.timerText.text = "Time: " + Mathf.Round(elapsedTime);
            score = (int)Mathf.Round(elapsedTime);

            if (MasterSingleton.DisplayScore.areScoresDisplayed) // Her çevrimde iþlenmesin diye
            {
                MasterSingleton.DisplayScore.HideScores();
            }

            // Kullanýcý tahtaya týkladýðýnda çalýþýr
            MasterSingleton.PlayerAction.TouchBoard();
        }
    }

    private void GameEnded()
    {
        if (!MasterSingleton.DisplayScore.areScoresDisplayed) // Her çevrimde iþlenmesin diye
        {
            MasterSingleton.CheckScore.ManageScore(score);
            MasterSingleton.DisplayScore.ShowScore();
        }
        MasterSingleton.replayButton.SetActive(true);
        MasterSingleton.backButton.SetActive(true);
        MasterSingleton.backArrowButton.SetActive(false);
    }
}
