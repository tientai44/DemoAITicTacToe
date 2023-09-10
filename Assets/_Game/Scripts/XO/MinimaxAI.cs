using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimaxAI
{
    public static int CalIndexInList(int row, int col, int matrixSize)
    {
        return row * matrixSize + col;
    }
    // Hàm ?ánh giá tr?ng thái c?a trò ch?i Tic Tac Toe
    //public static int EvaluateBoard(List<int> matrixCell,int mapSize,int numCheck,int row,int col)
    //{
    //    int count = 1;
    //    int indexSelect = CalIndexInList(row, col, mapSize);

    //    //Check hang doc
    //    for (int i = row - 1; i >= row - numCheck; i--)
    //    {
    //        if (i < 0)
    //        {
    //            break;
    //        }
    //        int index = CalIndexInList(i, col, mapSize);
    //        if (matrixCell[index] == matrixCell[indexSelect])
    //        {
    //            count += 1;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    for (int i = row + 1; i <= row + numCheck; i++)
    //    {
    //        if (i >= mapSize)
    //        {
    //            break;
    //        }
    //        int index = CalIndexInList(i, col, mapSize);

    //        if (matrixCell[index] == matrixCell[indexSelect])
    //        {
    //            count += 1;

    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }

    //    if (count >= numCheck)
    //    {
    //        if (matrixCell[indexSelect] == 1)
    //        {
    //            return -10;
    //        }
    //        else
    //        {
    //            return 10;
    //        }
    //    }
    //    //Check hang ngang
    //    count = 1;
    //    for (int i = col - 1; i >= col - numCheck; i--)
    //    {
    //        if (i < 0)
    //        {
    //            break;
    //        }
    //        int index = CalIndexInList(row, i, mapSize);
    //        if (matrixCell[index] == matrixCell[indexSelect])
    //        {
    //            count += 1;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    for (int i = col + 1; i <= col + numCheck; i++)
    //    {
    //        if (i >= mapSize)
    //        {
    //            break;
    //        }
    //        int index = CalIndexInList(row, i, mapSize);

    //        if (matrixCell[CalIndexInList(row, i, mapSize)] == matrixCell[indexSelect])
    //        {
    //            count += 1;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }

    //    if (count >= numCheck)
    //    {
    //        if (matrixCell[indexSelect] == 1)
    //        {
    //            return -10;
    //        }
    //        else
    //        {
    //            return 10;
    //        }
    //    }
    //    //Check hang cheo 1
    //    count = 1;
    //    for (int i = 1; i < numCheck; i++)
    //    {
    //        if (row - i < 0 || col - i < 0)
    //        {
    //            break;
    //        }
    //        int index = CalIndexInList(row - i, col - i, mapSize);
    //        if (matrixCell[index] == matrixCell[indexSelect])
    //        {
    //            count += 1;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    for (int i = 1; i < numCheck; i++)
    //    {
    //        if (row + i >= mapSize || col + i >= mapSize)
    //        {
    //            break;
    //        }
    //        int index = CalIndexInList(row + i, col + i, mapSize);
    //        if (matrixCell[index] == matrixCell[indexSelect])
    //        {
    //            count += 1;

    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }

    //    if (count >= numCheck)
    //    {
    //        if (matrixCell[indexSelect] == 1)
    //        {
    //            return -10;
    //        }
    //        else
    //        {
    //            return 10;
    //        }
    //    }
    //    //Check hang cheo 2
    //    count = 1;
    //    for (int i = 1; i < numCheck; i++)
    //    {

    //        if (row - i < 0 || col + i >= mapSize)
    //        {
    //            break;
    //        }
    //        int index = CalIndexInList(row - i, col + i, mapSize);
    //        if (matrixCell[index] == matrixCell[indexSelect])
    //        {
    //            count += 1;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    for (int i = 1; i < numCheck; i++)
    //    {
    //        if (row + i >= mapSize || col - i < 0)
    //        {
    //            break;
    //        }
    //        int index = CalIndexInList(row + i, col - i, mapSize);
    //        if (matrixCell[index] == matrixCell[indexSelect])
    //        {
    //            count += 1;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    if (count >= numCheck)
    //    {
    //        if (matrixCell[indexSelect] == 1)
    //        {
    //            return -10;
    //        }
    //        else
    //        {
    //            return 10;
    //        }
    //    }
    //    return 0;
    //}

    public static int EvaluateBoard(List<int> matrixCell, int mapSize, int numCheck, int row, int col)
    {
        int count = 1;
        int indexSelect = CalIndexInList(row, col, mapSize);
        int point = 0;
        bool isContinue=true;
        //Check hang doc
        for (int i = row - 1; i >= row - numCheck; i--)
        {
            if (i < 0)
            {
                break;
            }
            int index = CalIndexInList(i, col, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                if (isContinue)
                {
                    count += 1;
                }
                point-=matrixCell[index]*2;
            }
            else
            {
                isContinue = false;
                if(0 != matrixCell[index])
                {
                    point -= matrixCell[index];
                    break;
                }
                else
                {
                    point -= matrixCell[indexSelect];
                }
            }
        }
        isContinue = true;
        for (int i = row + 1; i <= row + numCheck; i++)
        {
            if (i >= mapSize)
            {
                break;
            }
            int index = CalIndexInList(i, col, mapSize);

            if (matrixCell[index] == matrixCell[indexSelect])
            {
                if (isContinue)
                {
                    count += 1;
                }
                point -= matrixCell[index] * 2;
            }
            else
            {
                isContinue = false;
                if (0 != matrixCell[index])
                {
                    point -= matrixCell[index];
                    break;
                }
                else
                {
                    point -= matrixCell[indexSelect];
                }
            }
        }

        if (count >= numCheck)
        {
            if (matrixCell[indexSelect] == 1)
            {
                return -100;
            }
            else
            {
                return 100;
            }
        }
        //Check hang ngang
        count = 1;
        isContinue = true;
        for (int i = col - 1; i >= col - numCheck; i--)
        {
            if (i<0)
            {
                break;
            }
           
            int index = CalIndexInList(row, i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                if (isContinue)
                {
                    count += 1;
                }
                point -= matrixCell[index] * 2;
            }
            else
            {
                isContinue = false;
                if (0 != matrixCell[index])
                {
                    point -= matrixCell[index];
                    break;
                }
                else
                {
                    point -= matrixCell[indexSelect];
                }
            }
        }
        isContinue = true;
        for (int i = col + 1; i <= col + numCheck; i++)
        {
            if (i >= mapSize)
            {
                break;
            }
            int index = CalIndexInList(row, i, mapSize);

            if (matrixCell[index] == matrixCell[indexSelect])
            {
                if (isContinue)
                {
                    count += 1;
                }
                point -= matrixCell[index] * 2;
            }
            else
            {
                isContinue = false;
                if (0 != matrixCell[index])
                {
                    point -= matrixCell[index];
                    break;
                }
                else
                {
                    point -= matrixCell[indexSelect];
                }
            }
        }

        if (count >= numCheck)
        {
            if (matrixCell[indexSelect] == 1)
            {
                return -100;
            }
            else
            {
                return 100;
            }
        }

        //Check hang cheo 1
        count = 1;
        isContinue = true;
        for (int i = 1; i < numCheck; i++)
        {
            if (row - i < 0 || col - i < 0)
            {
                break;
            }
            int index = CalIndexInList(row - i, col - i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                if (isContinue)
                {
                    count += 1;
                }
                point -= matrixCell[index] * 2;
            }
            else
            {
                isContinue = false;
                if (0 != matrixCell[index])
                {
                    point -= matrixCell[index];
                    break;
                }
                else
                {
                    point -= matrixCell[indexSelect];
                }
            }
        }
        isContinue = true;
        for (int i = 1; i < numCheck; i++)
        {
            if (row + i >= mapSize || col + i >= mapSize)
            {
                break;
            }
            int index = CalIndexInList(row + i, col + i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                if (isContinue)
                {
                    count += 1;
                }
                point -= matrixCell[index] * 2;
            }
            else
            {
                isContinue = false;
                if (0 != matrixCell[index])
                {
                    point -= matrixCell[index];
                    break;
                }
                else
                {
                    point -= matrixCell[indexSelect];
                }
            }
        }

        if (count >= numCheck)
        {
            if (matrixCell[indexSelect] == 1)
            {
                return -100;
            }
            else
            {
                return 100;
            }
        }

        //Check hang cheo 2
        count = 1;
        isContinue = true;
        for (int i = 1; i < numCheck; i++)
        {

            if (row - i < 0 || col + i >= mapSize)
            {
                break;
            }
            int index = CalIndexInList(row - i, col + i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                if (isContinue)
                {
                    count += 1;
                }
                point -= matrixCell[index] * 2;
            }
            else
            {
                isContinue = false;
                if (0 != matrixCell[index])
                {
                    point -= matrixCell[index];
                    break;
                }
                else
                {
                    point -= matrixCell[indexSelect];
                }
            }
        }
        isContinue = true;
        for (int i = 1; i < numCheck; i++)
        {
            if (row + i >= mapSize || col - i < 0)
            {
                break;
            }
            int index = CalIndexInList(row + i, col - i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                if (isContinue)
                {
                    count += 1;
                }
                point -= matrixCell[index] * 2;
            }
            else
            {
                isContinue = false;
                if (0 != matrixCell[index])
                {
                    point -= matrixCell[index];
                    break;
                }
                else
                {
                    point -= matrixCell[indexSelect];
                }
            }
        }

        if (count >= numCheck)
        {
            if (matrixCell[indexSelect] == 1)
            {
                return -100;
            }
            else
            {
                return 100;
            }
        }

        return point;
    }

    // Hàm tìm n??c ?i t?i ?u cho AI b?ng thu?t toán Minimax
    public static int MiniMax(List<int> matrix,int mapSize,int numCheck,int row,int col, int depth, bool isMaximizing)
    {
        int score = EvaluateBoard(matrix,mapSize,numCheck,row,col);

        if (score== 100)
        {
            return score+depth;
        }
        if (score ==-100)
        {
            return score - depth;
        }
        if(depth == 0)
        {
            return score;
        }
        bool isDraw = true;
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                int index = CalIndexInList(i, j, mapSize);
                if (matrix[index] == 0)
                {
                    isDraw = false;
                    break;
                }
            }
        }
        if (isDraw)
        {
            return score;
        }
        if (isMaximizing)
        {
            int bestScore = -1000;

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    int index = CalIndexInList(i, j, mapSize);
                    if (matrix[index] == 0)
                    {
                        matrix[index] = -1;
                        int moveScore = MiniMax(matrix,mapSize,numCheck,i,j, depth - 1, false);
                        matrix[index] = 0;
                        bestScore = Mathf.Max(bestScore, moveScore);
                    }
                }
            }

            return bestScore;
        }
        else
        {
            int bestScore = 1000;

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    int index = CalIndexInList(i, j, mapSize);
                    if (matrix[index] == 0)
                    {
                        matrix[index] = 1;
                        int moveScore = MiniMax(matrix, mapSize, numCheck, i, j, depth - 1, true);
                        matrix[index] = 0;
                        bestScore = Mathf.Min(bestScore, moveScore);

                    }
                }
            }

            return bestScore;
        }
    }

    // Hàm th?c hi?n n??c ?i c?a AI
    public static void MakeAIMove(List<int> matrix, int mapSize, int numCheck, int depth)
    {
        int bestScore = -1000;
        int bestMoveX=-1;
        int bestMoveY=-1;
        // Xác ??nh n??c ?i t?i ?u cho AI
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                int index = CalIndexInList(i, j, mapSize);
                if (IsGoodCell(matrix, mapSize, numCheck, i, j, 2))
                {
                    if (matrix[index] == 0)
                    {

                        matrix[index] = -1; // ?i?m cho n??c ?i AI
                        int moveScore = MiniMax(matrix, mapSize, numCheck, i, j, depth, false); // Tính ?i?m cho n??c ?i
                        Debug.Log(moveScore.ToString() + " " + i.ToString() + " " + j.ToString());
                        matrix[index] = 0; // H?y n??c ?i t?m th?i
                        if (moveScore > bestScore)
                        {
                            bestScore = moveScore;
                            bestMoveX = i;
                            bestMoveY = j;

                        }
                    }
                }
            }
        }
        if (bestMoveX < 0 || bestMoveY < 0)
        {
            return;
        }
        GameManager.instance.GetCell(bestMoveX, bestMoveY).Tick(CellType.O);
        //// Th?c hi?n n??c ?i t?i ?u c?a AI
        //MakeMove(bestMoveX, bestMoveY, 1); // ?ánh d?u ô t?i t?a ?? (bestMoveX, bestMoveY) v?i giá tr? 1 (AI)
    }
    public static bool IsGoodCell(List<int> matrix, int mapSize, int numCheck,int row,int col,int sizeCheck)
    {
        int indexSelect = CalIndexInList(row, col, mapSize);

        for(int i=-sizeCheck; i < sizeCheck; i++)
        {
            for (int j = -sizeCheck; j < sizeCheck; j++)
            {
                if(i==0 && j == 0)
                {
                    continue;
                }
                if (row + i < 0 || col + j < 0||row+i>=mapSize||col+j>=mapSize)
                {
                    continue;
                }
                int index = CalIndexInList(row+i, col+j, mapSize);
                //Debug.Log(index.ToString() +" "+(row+i).ToString()+" "+(col+i).ToString());
                if (matrix[index] != 0)
                {
                    return true;
                }
            }
        }
       
        return false;
    }
}
