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
                        //int hitRow = chessSquareComponent.row;
                        //int hitCol = chessSquareComponent.col;

                        //Vector2 spawnPos = chessboardSquares[hitRow, hitCol].transform.position;
                        //Debug.Log("Row: " + hitRow + ", Col: " + hitCol);
                        //Debug.Log("spawnPos.x: " + spawnPos.x + ", spawnPos.y: " + spawnPos.y);
                        GameObject queen = Instantiate(queenPrefab, chessSquareComponent.gameObject.transform);
                        queen.transform.SetParent(canvas.transform, false);
                        queen.transform.SetParent(chessSquareComponent.gameObject.transform);
                        queen.transform.localPosition = Vector3.zero;   

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
        //chessboardSquares = new GameObject[rows, columns];

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
                //chessboardSquares[row, col].tag = "ChessboardSquare_" + row + "_" + col;
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

    void OnChessSquareClicked(Vector2 queenSpawnPos)
    {
        // Get the clicked chessboard square position
        //Vector3 clickedSquarePosition = GetChessSquareCenter(xPos, yPos);

        // Instantiate the queen prefab at the clicked position
        GameObject queen = Instantiate(queenPrefab, new(queenSpawnPos.x, queenSpawnPos.y, -2), Quaternion.identity);
        queen.transform.SetParent(canvas.transform, false);
    }

    //Vector3 GetChessSquareCenter(float xPos, float yPos)
    //{
    //    // Calculate the position with an offset
    //    //float offsetX = 30f;
    //    //float offsetY = 86f;

    //    Vector3 position = new(xPos * squareSize - squareSize / 2, yPos * squareSize - squareSize / 2, -2);

    //    return position;
    //}

    Vector2 FindClosestCenter(Vector2 target, Vector2[,] centers)
    {
        Vector2 closestCenter = centers[0, 0];
        float minDistance = Vector2.Distance(target, closestCenter);

        for (int i = 0; i < centers.GetLength(0); i++)
        {
            for (int j = 0; j < centers.GetLength(1); j++)
            {
                Vector2 center = centers[i, j];
                float distance = Vector2.Distance(target, center);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCenter = center;
                }
            }
        }

        return closestCenter;
    }

    Vector2 QueenSpawnPos(Vector2 clickedPosition)
    {
        // Calculate the logical indices of the clicked chessboard square
        //int row = Mathf.FloorToInt(clickedPosition.y);
        //int col = Mathf.FloorToInt(clickedPosition.x);

        float squareBorderWidth = boardWidth / 8;
        float squareBorderHeight = boardHeight / 8;

        float firstXCenter = bottomLeft.x + squareBorderWidth / 2;
        float firstYCenter = bottomLeft.y + squareBorderHeight / 2;

        float[] squareXCenterPoints = new float[8];
        float[] squareYCenterPoints = new float[8];

        for (int k = 0; k < 8; k++)
        {
            squareXCenterPoints[k] = firstXCenter + squareBorderWidth * k;
            squareYCenterPoints[k] = firstYCenter + squareBorderHeight * k;
        }

        Vector2[,] allCenterPoints = new Vector2[8, 8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                allCenterPoints[i, j] = new Vector2(squareXCenterPoints[i], squareYCenterPoints[j]);
                Debug.Log(allCenterPoints[i, j]);
            }
        }

        // En yakýn merkezi bulma
        Vector2 closestCenter = FindClosestCenter(clickedPosition, allCenterPoints);

        //float[] indices = { col, row };
        return closestCenter;
    }
}
