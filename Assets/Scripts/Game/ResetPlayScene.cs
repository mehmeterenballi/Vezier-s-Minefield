using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayScene : MonoBehaviour
{
    public ChessGameManager gameManager;

    public void ResetGame()
    {
        // Oyunu sýfýrla
        gameManager.queenCounter = 8;
        gameManager.unThreatenedSquares = 64;

        gameManager.inGame.SetActive(true);
        gameManager.currentScoreText.gameObject.SetActive(false);
        gameManager.highscoreText.gameObject.SetActive(false);
        gameManager.scores.SetActive(false);
        gameManager.GameOver.SetActive(false);
        gameManager.replayButton.gameObject.SetActive(false);

        for (int i = 0; i < gameManager.rows; i++)
        {
            for (int j = 0; j < gameManager.columns; j++)
            {
                gameManager.threatenedSquares[i, j] = false;
                gameManager.chessboardSquares[i, j].transform.position = new(gameManager.chessboardSquares[i, j].transform.position.x,
                        gameManager.chessboardSquares[i, j].transform.position.y, 90f);
            }
        }

        foreach (GameObject queen in gameManager.spawnedQueens)
        {
            Destroy(queen);
        }

        gameManager.gameOver = false;
        gameManager.isWinner = false;
        gameManager.StartTimer();
    }
}
 