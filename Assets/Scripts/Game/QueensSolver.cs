using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueensSolver : MonoBehaviour
{
    //public List<List<int>> FindAllSolutions(int n)
    //{
    //    var solutions = new List<List<int>>();
    //    Solve(n, 0, new List<int>(), solutions);
    //    return solutions;
    //}

    //private void Solve(int n, int row, List<int> queens, List<List<int>> solutions)
    //{
    //    if (row == n)
    //    {
    //        solutions.Add(new List<int>(queens));
    //        return;
    //    }

    //    for (int col = 0; col < n; col++)
    //    {
    //        if (IsSafe(queens, row, col))
    //        {
    //            queens.Add(col);
    //            Solve(n, row + 1, queens, solutions);
    //            queens.RemoveAt(queens.Count - 1);
    //        }
    //    }
    //}

    //private bool IsSafe(List<int> queens, int row1, int col1)
    //{
    //    for (int row2 = 0; row2 < queens.Count; row2++)
    //    {
    //        int col2 = queens[row2];
    //        if (col1 == col2 || Math.Abs(col1 - col2) == Math.Abs(row1 - row2))
    //            return false;
    //    }
    //    return true;
    //}
}
