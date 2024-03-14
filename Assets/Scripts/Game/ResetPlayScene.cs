using UnityEngine;

public class ResetPlayScene : MonoBehaviour
{
    public ChessGameManager gameManager;

    public void ResetGame()
    {
        // Oyunu sýfýrla
        ChessGameManager.elapsedTime = 0f;
        ChessGameManager.score = 0;
        ChessGameManager.queenCounter = 8;
        ChessGameManager.unThreatenedSquares = 64;

        ChessGameManager.inGame.SetActive(true);
        gameManager.currentScoreText.gameObject.SetActive(false);
        DisplayScore.highscoreText.gameObject.SetActive(false);
        DisplayScore.scores.SetActive(false);
        ChessGameManager.solutions.SetActive(false);
        ChessGameManager.GameOver.SetActive(false);
        gameManager.replayButton.SetActive(false);

        for (int i = 0; i < ChessGameManager.rows; i++)
        {
            for (int j = 0; j < ChessGameManager.columns; j++)
            {
                ChessGameManager.threatenedSquares[i, j] = false;
                DisplaySquares.chessboardSquares[i, j].GetComponent<ChessSquare>().isThreatened = false;
                DisplaySquares.chessboardSquares[i, j].GetComponent<ChessSquare>().isOccupied = false;
                DisplaySquares.chessboardSquares[i, j].transform.position = new(DisplaySquares.chessboardSquares[i, j].transform.position.x,
                        DisplaySquares.chessboardSquares[i, j].transform.position.y, 90f);
            }
        }

        foreach (GameObject queen in ChessGameManager.spawnedQueens)
        {
            Destroy(queen);
        }

        ChessGameManager.gameOver = false;
        ChessGameManager.isWinner = false;
        DisplayScore.HideScores();
    }
}
 