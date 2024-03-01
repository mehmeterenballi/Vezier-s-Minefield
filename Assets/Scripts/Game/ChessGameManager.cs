using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameManager : MonoBehaviour
{
    //public GameObject queenPrefab;
    //public GameObject occupiedIndicatorPrefab;
    //public Transform chessBoardTransform;
    //private QueensSolver solver = new QueensSolver();
    //private List<List<int>> solutions;
    //public Slider solutionSlider;

    //void Start()
    //{
    //    solutions = solver.FindAllSolutions(8);
    //    solutionSlider.maxValue = solutions.Count - 1;
    //    solutionSlider.onValueChanged.AddListener(ShowSolution);
    //    ShowSolution(0);
    //}

    //public void ShowSolution(float index)
    //{
    //    foreach (Transform child in chessBoardTransform)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    var solution = solutions[(int)index];
    //    for (int i = 0; i < solution.Count; i++)
    //    {
    //        Instantiate(queenPrefab, new Vector3(solution[i], 0, i), Quaternion.identity, chessBoardTransform);
    //    }
    //}
}
