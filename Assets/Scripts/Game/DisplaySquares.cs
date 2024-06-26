using System;
using UnityEngine;


public class DisplaySquares : MonoBehaviour
{
    private ChessGameManager MasterSingleton;

    // Array to store logical representation of the chessboard
    public static GameObject[,] chessboardSquares;

    public GameObject inGame;

    private int rows=0;
    private int columns=0;

    // Define the size of each chessboard square
    private static readonly float squareSize = 41.8f;

    public static DisplaySquares instance;
    private void Awake()
    {
        MasterSingleton = ChessGameManager.MasterSingleton;
        rows = MasterSingleton.rows;
        columns = MasterSingleton.columns;
    }

    public void InitializeChessboardSquares()
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

    public void MarkThreatenedSquares(int row, int col)
    {
        for (int i = 0; i < 8; i++)
        {
            if (chessboardSquares[row, i].GetComponent<ChessSquare>().isThreatened == false && chessboardSquares[i, col].GetComponent<ChessSquare>().isThreatened == false)
            {
                chessboardSquares[row, i].GetComponent<ChessSquare>().isThreatened = true;
                chessboardSquares[i, col].GetComponent<ChessSquare>().isThreatened = true;
                if (col == row && row == i)
                {
                    MasterSingleton.unThreatenedSquares--;
                }
                else
                {
                    MasterSingleton.unThreatenedSquares -= 2;
                }
            }

            MasterSingleton.threatenedSquares[row, i] = true;
            MasterSingleton.threatenedSquares[i, col] = true;

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
                        MasterSingleton.threatenedSquares[i, j] = true;
                        MasterSingleton.unThreatenedSquares--;
                    }
                }
            }
        }
    }

    public void HighlightThreatenedSquares(int row, int col)
    {
        // T�m kareleri kontrol et ve tehdit alt�ndakileri k�rm�z� yap
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (MasterSingleton.threatenedSquares[i, j] == true && chessboardSquares[i, j].GetComponent<ChessSquare>().isOccupied == false)
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
}
