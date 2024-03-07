using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class ChessGameManager : MonoBehaviour
{
    public GameObject queenPrefab;

    private Camera mainCamera;
    public GameObject canvas;

    // Define the size of each chessboard square
    private readonly float squareSize = 41.8f;

    // Define the chessboard dimensions
    private readonly int rows = 8;
    private readonly int columns = 8;

    // Array to store logical representation of the chessboard
    private GameObject[,] chessboardSquares;

    private BoxCollider2D chessboardCollider;

    Vector3 bottomLeft;
    Vector3 topRight;

    public GameObject ChessBoard;

    float boardWidth;
    float boardHeight;

    void Start()
    {
        mainCamera = Camera.main;


        if (ChessBoard.TryGetComponent<BoxCollider2D>(out chessboardCollider))
        {
            // Calculate the world coordinates of the corners
            bottomLeft = transform.TransformPoint(chessboardCollider.offset - chessboardCollider.size / 2f);
            topRight = transform.TransformPoint(chessboardCollider.offset + chessboardCollider.size / 2f);

            boardWidth = topRight.x - bottomLeft.x;
            boardHeight = topRight.y - bottomLeft.y;

            // Log the results
            Debug.Log("Bottom-left corner: " + bottomLeft);
            Debug.Log("Top-right corner: " + topRight);
        }
        else
        {
            Debug.LogError("BoxCollider2D not found!");
        }

        // Initialize the chessboardSquares array
        InitializeChessboardSquares();
    }

    void Update()
    {
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
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.TryGetComponent<ChessSquare>(out var chessSquareComponent))
                    {

                        GameObject queen = Instantiate(queenPrefab, chessSquareComponent.gameObject.transform);
                        queen.transform.SetParent(canvas.transform, false);
                        queen.transform.SetParent(chessSquareComponent.gameObject.transform);
                        //Vector3 squareCenter = chessSquareComponent.gameObject.transform.position;
                        queen.transform.localPosition = new(0, 0, -2);

                    }
                    else
                    {
                        Debug.Log("chessSquareComponent not found!");
                    }
                }
            }
        }
    }

    private void InitializeChessboardSquares()
    {
        // Initialize the chessboardSquares array
        chessboardSquares = new GameObject[rows, columns];

        // Populate the array with logical representations of the chessboard squares
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float xPosition = (col - 4) * squareSize + 21.1f;
                float yPosition = row * squareSize + 70.2f;

                // Create a logical representation of the chessboard square
                chessboardSquares[row, col] = new GameObject("ChessboardSquare_" + row + "_" + col);
                chessboardSquares[row, col].transform.position = new Vector3(xPosition, yPosition, 0f);

                chessboardSquares[row, col].AddComponent<BoxCollider2D>().isTrigger = true;
                // Set the Canvas as the parent of the chessboard square
                chessboardSquares[row, col].transform.SetParent(canvas.transform, false);

                // Set the scale of each chessboard tile (adjust the multiplier as needed)
                float scaleMultiplier = 42f;
                chessboardSquares[row, col].transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1f);

                ChessSquare chessSquareComponent = chessboardSquares[row, col].AddComponent<ChessSquare>();
                chessSquareComponent.row = row;
                chessSquareComponent.col = col;
            }
        }
    }

}
