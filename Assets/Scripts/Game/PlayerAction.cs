using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public void TouchBoard(Camera mainCamera)
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
                    if (hit.collider.TryGetComponent<ChessSquare>(out var chessSquareComponent))
                    {
                        if (ChessGameManager.MasterSingleton.unThreatenedSquares > 0 && !hit.collider.GetComponent<ChessSquare>().isOccupied && !hit.collider.GetComponent<ChessSquare>().isThreatened)
                        {
                            InstantiateQueen(hit, chessSquareComponent);
                            MarkSquares(chessSquareComponent);
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


    public void InstantiateQueen(RaycastHit2D hit, ChessSquare chessSquareComponent)
    {
        GameObject queen = Instantiate(ChessGameManager.MasterSingleton.queenPrefab, chessSquareComponent.gameObject.transform);
        ChessGameManager.MasterSingleton.spawnedQueens.Add(queen);
        ChessGameManager.MasterSingleton.queenCounter--;
        queen.transform.SetParent(ChessGameManager.MasterSingleton.inGame.transform, false);
        queen.transform.SetParent(chessSquareComponent.gameObject.transform);
        queen.transform.localPosition = new(0, 0, -2);
        hit.collider.GetComponent<ChessSquare>().isOccupied = true;
    }

    private void MarkSquares(ChessSquare chessSquareComponent)
    {
        int row = chessSquareComponent.row;
        int col = chessSquareComponent.col;
        ChessGameManager.MasterSingleton.DisplaySquares.MarkThreatenedSquares(row, col);
        ChessGameManager.MasterSingleton.DisplaySquares.HighlightThreatenedSquares(row, col);
    }
}
