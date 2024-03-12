using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class ChessGameManager : MonoBehaviour
{

    public bool isWinner = false;
    public bool gameOver = false;

    private bool isHighScore = false;
    private int[] klon;

    public GameObject GameOver;
    public GameObject solutions;
    public GameObject queenPrefab;

    private Camera mainCamera;
    public GameObject inGame;

    // Define the size of each chessboard square
    private readonly float squareSize = 41.8f;

    // Define the chessboard dimensions
    public readonly int rows = 8;
    public readonly int columns = 8;
    public int queenCounter = 8;
    public int unThreatenedSquares = 64;

    public TextMeshProUGUI timerText;
    float elapsedTime = 0f;

    private int score = 0;

    public GameObject scores;
    string[] highscoreStrings;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoresText;
    private int[] highscores = new int[5];
    public GameObject replayButton;

    bool isRunning = true;
    bool areScoresDisplayed = false;

    // Array to store logical representation of the chessboard
    public GameObject[,] chessboardSquares;

    public bool[,] threatenedSquares = new bool[8, 8];

    public List<GameObject> spawnedQueens = new();

    void Start()
    {
        mainCamera = Camera.main;

        StartTimer();

        // Initialize the chessboardSquares array
        InitializeChessboardSquares();
        CheckScore(score);
    }

    void Update()
    {
        if (unThreatenedSquares == 0)
        {
            if (!areScoresDisplayed) // Her çevrimde iþlenmesin diye
            {
                CheckScore(score);
                ShowScore();
            }
            replayButton.SetActive(true);
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
            if (areScoresDisplayed) // Her çevrimde iþlenmesin diye
            {
                HideScores();
            }
            // Check for touches
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    // Raycast to detect the chessboard
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    if (hit.collider != null)
                    {
                        if (hit.collider.TryGetComponent<ChessSquare>(out var chessSquareComponent))
                        {
                            if (unThreatenedSquares > 0 && !hit.collider.GetComponent<ChessSquare>().isOccupied && !hit.collider.GetComponent<ChessSquare>().isThreatened)
                            {
                                int row = chessSquareComponent.row;
                                int col = chessSquareComponent.col;

                                GameObject queen = Instantiate(queenPrefab, chessSquareComponent.gameObject.transform);
                                spawnedQueens.Add(queen);
                                queenCounter--;
                                queen.transform.SetParent(inGame.transform, false);
                                queen.transform.SetParent(chessSquareComponent.gameObject.transform);
                                queen.transform.localPosition = new(0, 0, -2);

                                hit.collider.GetComponent<ChessSquare>().isOccupied = true;

                                MarkThreatenedSquares(row, col);
                                HighlightThreatenedSquares(row, col);

                                //Debug.Log("Unthreatened squares: " + unThreatenedSquares);
                            }
                            else
                            {
                                Debug.Log("Square is threatened!");
                            }
                        }
                        else
                        {
                            Debug.Log(hit.collider.gameObject.name);
                            Debug.Log("chessSquareComponent not found!");
                        }
                    }
                }
            }
        }
    }

    private void InitializeChessboardSquares()
    {
        // Initialize the chessboardSquares array
        chessboardSquares = new GameObject[rows, columns];

        // Load the sprite that you want to use for the chessboard squares
        Sprite squareSprite = Resources.Load<Sprite>("Sprites/Square");

        if (squareSprite == null)
        {
            Debug.LogError("Square sprite not found. Make sure the sprite exists in the Resources folder.");
            return;
        }

        // Populate the array with logical representations of the chessboard squares
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float xPosition = (col - 4) * squareSize + 21.1f;
                float yPosition = row * squareSize - 95.9f;

                // Create a logical representation of the chessboard square
                chessboardSquares[row, col] = new GameObject("ChessboardSquare_" + row + "_" + col);
                chessboardSquares[row, col].transform.position = new Vector3(xPosition, yPosition, 0f);

                chessboardSquares[row, col].AddComponent<BoxCollider2D>().isTrigger = true;

                // Add a SpriteRenderer component to the chessboard square
                SpriteRenderer squareRenderer = chessboardSquares[row, col].AddComponent<SpriteRenderer>();

                // Set the sprite for the chessboard square
                squareRenderer.sprite = squareSprite;
                chessboardSquares[row, col].GetComponent<SpriteRenderer>().color = new Color32(200, 0, 0, 180);

                // Set the inGame as the parent of the chessboard square
                chessboardSquares[row, col].transform.SetParent(inGame.transform, false);

                // Set the scale of each chessboard square (adjust the multiplier as needed)
                float scaleMultiplier = 42f;
                chessboardSquares[row, col].transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1f);

                ChessSquare chessSquareComponent = chessboardSquares[row, col].AddComponent<ChessSquare>();
                chessSquareComponent.row = row;
                chessSquareComponent.col = col;
            }
        }
    }

    private void MarkThreatenedSquares(int row, int col)
    {
        for (int i = 0; i < 8; i++)
        {
            if (chessboardSquares[row, i].GetComponent<ChessSquare>().isThreatened == false && chessboardSquares[i, col].GetComponent<ChessSquare>().isThreatened == false)
            {
                chessboardSquares[row, i].GetComponent<ChessSquare>().isThreatened = true;
                chessboardSquares[i, col].GetComponent<ChessSquare>().isThreatened = true;
                if (col == row && row == i)
                {
                    unThreatenedSquares--;
                }
                else
                {
                    unThreatenedSquares--;
                    unThreatenedSquares--;
                }
            }

            threatenedSquares[row, i] = true;
            threatenedSquares[i, col] = true;

        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i == row || j == col || Math.Abs(i - row) == Math.Abs(j - col))
                {
                    if (chessboardSquares[i, j].GetComponent<ChessSquare>().isThreatened == false)
                    {
                        chessboardSquares[i, j].GetComponent<ChessSquare>().isThreatened = true;
                        threatenedSquares[i, j] = true;
                        unThreatenedSquares--;
                    }
                }
            }
        }
    }

    private void HighlightThreatenedSquares(int row, int col)
    {
        // Tüm kareleri kontrol et ve tehdit altýndakileri kýrmýzý yap
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (threatenedSquares[i, j] == true && chessboardSquares[i, j].GetComponent<ChessSquare>().isOccupied == false)
                {
                    chessboardSquares[i, j].transform.position = new(chessboardSquares[i, j].transform.position.x,
                        chessboardSquares[i, j].transform.position.y, -2f);
                }
                else
                {
                    chessboardSquares[row, col].transform.position = new(chessboardSquares[row, col].transform.position.x,
                        chessboardSquares[row, col].transform.position.y, 90f);
                }
            }
        }
    }

    public void StartTimer()
    {
        // Kronometreyi baþlat
        isRunning = true;
    }

    private void StopTimer()
    {
        // Kronometreyi durdur
        isRunning = false;
    }

    private void InsertAndShift(int[] array, int index, int value)
    {
        for (int i = array.Length - 1; i > index; i--)
        {
            array[i] = array[i - 1];
        }
        array[index] = value;
    }

    private void CheckScore(int score)
    {
        highscoreStrings = PlayerPrefs.GetString("highscores", "").Split(",");
        //Debug.Log(highscoreStrings + " " + highscoreStrings.Length + highscoreStrings[0]);
        isHighScore = false;

        if (queenCounter == 0)
        {
            if (highscoreStrings[0] == "")
            {
                highscores[0] = score;
                isHighScore = true;
                KeepRecord();
            }
            else if (highscoreStrings.Length < 5)
            {
                for (int i = 0; i < highscoreStrings.Length; i++)
                {
                    if (Int32.TryParse(highscoreStrings[i], out int currentScore))
                    {
                        highscores[i] = currentScore;
                        if (score == highscores[i])
                        {
                            break;
                        }
                        if (score < highscores[i])
                        {
                            isHighScore = true;
                            InsertAndShift(highscores, i, score);
                            KeepRecord();
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("Invalid score: " + highscoreStrings[i]);
                    }
                }

            }
            else
            {
                for (int i = 0; i < highscoreStrings.Length; i++)
                {
                    if (Int32.TryParse(highscoreStrings[i], out int currentScore))
                    {
                        highscores[i] = currentScore;
                        if (score < highscores[i])
                        {
                            isHighScore = true;
                            InsertAndShift(highscores, i, score);
                            KeepRecord();
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("Invalid score: " + highscoreStrings[i]);
                    }
                }
            }
        }
    }

    private void KeepRecord()
    {
        Array.Sort(highscores);
        List<int> yeniDizi = new List<int>();
        for (int i = 0; i < highscores.Length; i++)
        {
            if (highscores[i] != 0)
            {
                yeniDizi.Add(highscores[i]);
            }
        }
        int[] sonuc = yeniDizi.ToArray();
        klon = (int[])sonuc.Clone();
        string highscoresString = string.Join(",", sonuc.Select(p => p.ToString()).ToArray());
        PlayerPrefs.SetString("highscores", highscoresString);
    }

    private void ShowScore()
    {
        if (queenCounter == 0)
        {
            isWinner = true;
            Debug.Log("You Won");
            currentScoreText.text = score + "!";
            currentScoreText.gameObject.SetActive(true);
            if (isHighScore)
            {
                highscoreText.gameObject.SetActive(true);
            }
        }
        else
        {
            gameOver = true;
            GameOver.SetActive(true);
        }

        highScoresText.text = "Best Score: " + klon[0] + "\n";
        if (klon.Length > 0)
        {
            for (int i = 1; i < klon.Length; i++)
            {
                highScoresText.text += (i + 1).ToString() + ": " + klon[i] + "\n";
            }
        }
        else
        {
            for (int i = 0; i < klon.Length; i++)
            {
                highScoresText.text += (i + 1).ToString() + ": " + klon[i] + "\n";
            }
        }


        inGame.SetActive(false);
        scores.SetActive(true);
        solutions.SetActive(true);
        StopTimer();
        areScoresDisplayed = true;
    }

    private void HideScores()
    {
        StartTimer();
        areScoresDisplayed = false;
    }

    public void ResetGame()
    {
        // Oyunu sýfýrla
        elapsedTime = 0f;
        score = 0;
        queenCounter = 8;
        unThreatenedSquares = 64;

        inGame.SetActive(true);
        currentScoreText.gameObject.SetActive(false);
        highscoreText.gameObject.SetActive(false);
        scores.SetActive(false);
        solutions.SetActive(false);
        GameOver.SetActive(false);
        replayButton.SetActive(false);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                threatenedSquares[i, j] = false;
                chessboardSquares[i, j].GetComponent<ChessSquare>().isThreatened = false;
                chessboardSquares[i, j].GetComponent<ChessSquare>().isOccupied = false;
                chessboardSquares[i, j].transform.position = new(chessboardSquares[i, j].transform.position.x,
                        chessboardSquares[i, j].transform.position.y, 90f);
            }
        }

        foreach (GameObject queen in spawnedQueens)
        {
            Destroy(queen);
        }

        gameOver = false;
        isWinner = false;
        HideScores();
    }
}
