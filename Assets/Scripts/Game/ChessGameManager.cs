using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class ChessGameManager : MonoBehaviour
{
    CheckScore checkScore;
    PlayerAction playerAction;

    public static bool isWinner = false;
    public static bool gameOver = false;

    private int[] klon;

    public static GameObject GameOver;
    public static GameObject solutions;
    public static GameObject queenPrefab;

    private Camera mainCamera;
    public static GameObject inGame;

    // Define the chessboard dimensions
    public static int rows = 8;
    public static int columns = 8;
    public static int queenCounter = 8;
    public static int unThreatenedSquares = 64;

    public TextMeshProUGUI timerText;
    public static float elapsedTime = 0f;

    public static int score = 0;

    public TextMeshProUGUI currentScoreText;
    public GameObject replayButton;
    public GameObject backButton;
    public GameObject backArrowButton;

    public static bool isRunning = true;

    public static bool[,] threatenedSquares = new bool[8, 8];

    public static List<GameObject> spawnedQueens = new();

    void Start()
    {
        mainCamera = Camera.main;

        Utilites.StartTimer();

        // Initialize the chessboardSquares array
        DisplaySquares.instance.InitializeChessboardSquares();
        checkScore.ManageScore(score);
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
            playerAction.TouchBoard(mainCamera);
        }
    }

    private void GameEnded()
    {
        if (!DisplayScore.areScoresDisplayed) // Her çevrimde iþlenmesin diye
        {
            checkScore.ManageScore(score);
            DisplayScore.ShowScore();
        }
        replayButton.SetActive(true);
        backButton.SetActive(true);
        backArrowButton.SetActive(false);
    }
}
