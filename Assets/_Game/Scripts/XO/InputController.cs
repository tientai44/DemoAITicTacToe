using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    bool isPressed = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Cell cell=null; 
            Physics2D.OverlapPoint(mousePos)?.TryGetComponent<Cell>(out cell);
            if (cell)
            {
                if (GameManager.instance.state is not GameState.Playing)
                {
                    return;
                }
                if (cell.State != CellType.empty)
                {
                    return;
                }
                if (GameManager.instance.turnState is TurnState.Player1)
                {
                    cell.Tick(CellType.X);
                    GameManager.instance.ChangeState(TurnState.Player2);
                    if (GameManager.instance.gameMode is GameMode.PVB)
                    {
                        GameManager.instance.AIMakeMove();
                    }
                }
                else if (GameManager.instance.gameMode is GameMode.PVP)
                {
                    cell.Tick(CellType.O);
                    GameManager.instance.ChangeState(TurnState.Player1);
                }
            }
        }
      
       
    }
}
