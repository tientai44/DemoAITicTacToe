using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TurnState
{
    Player1, Player2
}
public enum GameMode
{
    PVB, PVP
}

public enum GameState
{
    Playing,Waiting
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TurnState turnState = TurnState.Player1;
    public GameMode gameMode = GameMode.PVB;
    public GameState state = GameState.Waiting;
    private Map currentMap;
    Cell cellStart, cellEnd;
    [SerializeField] List<Map> maps;
    [SerializeField] List<Cell> cells = new List<Cell>();
    [SerializeField] List<int> matrixCell = new List<int>();
    [SerializeField] int currentSelect;
    private void Awake()
    {
        instance = this;
        //ChooseMap(currentSelect);
        float ratio = Screen.height / Screen.width;
        if(ratio > 1.8f)
        {
            Camera.main.fieldOfView = 73;
        }
        else
        {
            Camera.main.fieldOfView = 60;
        }
    }
    public void ResetGame()
    {
        ChooseMap(currentSelect,gameMode);
    }
    public void ChooseMap(int index,GameMode _gameMode)
    {
        if (currentMap != null)
        {
            currentMap.gameObject.SetActive(false);
        }
        state = GameState.Playing;
        gameMode = _gameMode;
        currentSelect = index;
        turnState = TurnState.Player1;
        currentMap = maps[index];
        currentMap.gameObject.SetActive(true);
        cells.Clear();
        matrixCell.Clear();
        for (int i = 0; i < currentMap.transform.childCount; i++)
        {
            Transform row = currentMap.transform.GetChild(i);
            for (int j = 0; j < row.childCount; j++)
            {
                Cell cell = row.GetChild(j).GetComponent<Cell>();
                cell.OnInit();
                cell.SetPos(i, j);
                cells.Add(cell);
                matrixCell.Add(0);
            }
        }
    }
    public void ChangeState(TurnState turn)
    {
        turnState = turn;
    }
    int CalIndexInList(int row, int col, int matrixSize)
    {
        return row * matrixSize + col;
    }
    public Cell GetCell(int row,int col)
    {
        return cells[CalIndexInList(row, col, currentMap.mapSize)];
    }
    public void SetValueMatrix(int row,int col,int val)
    {
        matrixCell[CalIndexInList(row, col, currentMap.mapSize)] = val;
        bool isWin = CheckWin(row, col);
        if (isWin)
        {
            Color c;
            if (val == 1)
            {
                c = Color.red;
            }
            else
            {
                c = Color.blue;
            }
            cellStart.Connect(cellEnd, c);
            Win();
        }
        else
        {
            bool isDraw = IsDraw();
            if (isDraw)
            {
                Draw();
            }
        }
    }
    public void Win()
    {
        state = GameState.Waiting;
        Invoke(nameof(ResetGame), 2f);
    }
    public void Draw()
    {
        state = GameState.Waiting;

        Invoke(nameof(ResetGame), 2f);
    }
    public bool IsDraw()
    {
        for (int i = 0; i < currentMap.mapSize; i++)
        {
            for (int j = 0; j < currentMap.mapSize; j++)
            {
                int index = CalIndexInList(i, j, currentMap.mapSize);
                if (matrixCell[index] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool CheckWin(int row, int col)
    {
        int numCheck = currentMap.winSize;
        int mapSize = currentMap.mapSize;
        int count = 1;
        int indexSelect = CalIndexInList(row, col, mapSize);

        //Check hang doc
        cellStart = cells[indexSelect];
        cellEnd = cells[indexSelect];
        for (int i = row - 1; i >= row - numCheck; i--)
        {
            if (i < 0)
            {
                break;
            }
            int index = CalIndexInList(i, col, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                count += 1;
                cellStart = cells[index];
            }
            else
            {
                break;
            }
        }
        for (int i = row + 1; i <= row + numCheck; i++)
        {
            if (i >= mapSize)
            {
                break;
            }
            int index = CalIndexInList(i, col, mapSize);

            if (matrixCell[index] == matrixCell[indexSelect])
            {
                count += 1;
                cellEnd = cells[index];

            }
            else
            {
                break;
            }
        }

        if (count >= numCheck)
        {
            return true;
        }
        //Check hang ngang
        cellStart = cells[indexSelect];
        cellEnd = cells[indexSelect];
        count = 1;
        for (int i = col - 1; i >= col - numCheck; i--)
        {
            if (i < 0)
            {
                break;
            }
            int index = CalIndexInList(row, i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                count += 1;
                cellStart = cells[index];
            }
            else
            {
                break;
            }
        }
        for (int i = col + 1; i <= col + numCheck; i++)
        {
            if (i >= mapSize)
            {
                break;
            }
            int index = CalIndexInList(row, i, mapSize);

            if (matrixCell[CalIndexInList(row, i, mapSize)] == matrixCell[indexSelect])
            {
                count += 1;
                cellEnd = cells[index];
            }
            else
            {
                break;
            }
        }

        if (count >= numCheck)
        {
            return true;
        }
        //Check hang cheo 1
        cellStart = cells[indexSelect];
        cellEnd = cells[indexSelect];
        count = 1;
        for (int i = 1; i < numCheck; i++)
        {
            if (row -i < 0||col-i<0)
            {
                break;
            }
            int index = CalIndexInList(row - i, col - i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                count += 1;
                cellStart = cells[index];
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < numCheck; i++)
        {
            if (row + i >= mapSize ||col+i>=mapSize)
            {
                break;
            }
            int index = CalIndexInList(row + i, col + i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                count += 1;
                cellEnd = cells[index];

            }
            else
            {
                break;
            }
        }

        if (count >= numCheck)
        {
            return true;
        }
        //Check hang cheo 2
        cellStart = cells[indexSelect];
        cellEnd = cells[indexSelect];
        count = 1;
        for (int i = 1; i < numCheck; i++)
        {
   
            if (row - i < 0 || col + i >=mapSize)
            {
                break;
            }
            int index = CalIndexInList(row - i, col + i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                count += 1;
                cellStart = cells[index];
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < numCheck; i++)
        {
            if (row + i >= mapSize || col - i <0)
            {
                break;
            }
            int index = CalIndexInList(row + i, col - i, mapSize);
            if (matrixCell[index] == matrixCell[indexSelect])
            {
                count += 1;
                cellEnd = cells[index];
            }
            else
            {
                break;
            }
        }
        if (count >= numCheck)
        {
            return true;
        }

        return false;
    }
    
    public void AIMakeMove()
    {
        if (state is GameState.Playing)
        {
            MinimaxAI.MakeAIMove(new List<int>(matrixCell), currentMap.mapSize, currentMap.winSize, 3);
            ChangeState(TurnState.Player1);
        }
    }
}
