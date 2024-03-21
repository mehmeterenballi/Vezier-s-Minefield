using UnityEngine;

public class ResetPlayScene : MonoBehaviour
{
    public ChessGameManager gameManager;

    public void ResetGame()
    {
        // Oyunu sýfýrla
        ChessGameManager.MasterSingleton.elapsedTime = 0f;
        ChessGameManager.MasterSingleton.score = 0;
        ChessGameManager.MasterSingleton.queenCounter = 8;
        ChessGameManager.MasterSingleton.unThreatenedSquares = 64;

        ChessGameManager.MasterSingleton.inGame.SetActive(true);
        gameManager.currentScoreText.gameObject.SetActive(false);
        ChessGameManager.MasterSingleton.DisplayScore.highscoreText.gameObject.SetActive(false);
        ChessGameManager.MasterSingleton.DisplayScore.scores.SetActive(false);
        ChessGameManager.MasterSingleton.solutions.SetActive(false);
        ChessGameManager.MasterSingleton.GameOver.SetActive(false);
        gameManager.replayButton.SetActive(false);

        for (int i = 0; i < ChessGameManager.MasterSingleton.rows; i++)
        {
            for (int j = 0; j < ChessGameManager.MasterSingleton.columns; j++)
            {
                ChessGameManager.MasterSingleton.threatenedSquares[i, j] = false;
                DisplaySquares.chessboardSquares[i, j].GetComponent<ChessSquare>().isThreatened = false;
                DisplaySquares.chessboardSquares[i, j].GetComponent<ChessSquare>().isOccupied = false;
                DisplaySquares.chessboardSquares[i, j].transform.position = new(DisplaySquares.chessboardSquares[i, j].transform.position.x,
                        DisplaySquares.chessboardSquares[i, j].transform.position.y, 90f);
            }
        }

        foreach (GameObject queen in ChessGameManager.MasterSingleton.spawnedQueens)
        {
            Destroy(queen);
        }

        ChessGameManager.MasterSingleton.gameOver = false;
        ChessGameManager.MasterSingleton.isWinner = false;
        ChessGameManager.MasterSingleton.DisplayScore.HideScores();
    }
}
 