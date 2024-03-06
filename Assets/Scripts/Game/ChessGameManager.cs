using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameManager : MonoBehaviour
{
    public GameObject queenPrefab;

    private Camera mainCamera;
    public GameObject canvas;

    // Define the size of each chessboard square
    private float squareSize = 41.8f;

    // Define the chessboard dimensions
    private int rows = 8;
    private int columns = 8;

    // Array to store logical representation of the chessboard
    private GameObject[,] chessboardSquares;

    void Start()
    {
        mainCamera = Camera.main;

        // Initialize the chessboardSquares array
        InitializeChessboard();
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

                // Perform the raycast
                if (hit.collider != null && hit.collider.CompareTag("Chessboard"))
                {
                    // Get the logical indices of the clicked chessboard square
                    int[] indices = GetChessboardIndices(hit.point);

                    // Call the function to handle the click
                    OnChessSquareClicked(indices[0], indices[1]);
                }
            }
        }
    }

    void InitializeChessboard()
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
                chessboardSquares[row, col].tag = "ChessboardSquare";

                // Set the Canvas as the parent of the chessboard square
                chessboardSquares[row, col].transform.SetParent(canvas.transform, false);

                // Set the scale of each chessboard square (adjust the multiplier as needed)
                float scaleMultiplier = 42f;
                chessboardSquares[row, col].transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1f);

            }
        }
    }

    void OnChessSquareClicked(int row, int col)
    {
        // Get the clicked chessboard square position
        Vector3 clickedPosition = GetChessSquareCenter(row, col);

        // Instantiate the queen prefab at the clicked position
        GameObject queen = Instantiate(queenPrefab, clickedPosition, Quaternion.identity);
        queen.transform.SetParent(canvas.transform, false);
    }

    Vector3 GetChessSquareCenter(int row, int col)
    {
        // Calculate the position with an offset
        //float offsetX = 30f;
        //float offsetY = 86f;

        Vector3 position = new Vector3(col * squareSize - squareSize / 2, row * squareSize - squareSize / 2, -2);

        return position;
    }

    int[] GetChessboardIndices(Vector2 clickedPosition)
    {
        // Calculate the logical indices of the clicked chessboard square
        int row = Mathf.FloorToInt(clickedPosition.y / squareSize);
        int col = Mathf.FloorToInt(clickedPosition.x / squareSize);

        Debug.Log("clickedPosition.y: " + clickedPosition.y + ", clickedPosition.x: " + clickedPosition.x);
        Debug.Log("clickedPosition.y / squareSize: " + clickedPosition.y / squareSize + ", clickedPosition.x / squareSize: " + clickedPosition.x / squareSize);
        Debug.Log("Row before clamp: " + row + ", col before clamp: " + col);

        // Ensure the indices are within the chessboard boundaries
        row = Mathf.Clamp(row, 0, rows - 1);
        col = Mathf.Clamp(col, 0, columns - 1);

        Debug.Log("Row after clamp: " + row + ", col after clamp: " + col);

        int[] indices = { row, col };
        return indices;
    }

}
