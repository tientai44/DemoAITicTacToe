using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CellType
{
    X,O,empty
}
public class Cell : MonoBehaviour, IPointerClickHandler
{
    CellType state= CellType.empty;
    [SerializeField]SpriteRenderer visibleSprite;
    [SerializeField] SpriteRenderer frameSprite;
    [SerializeField] Sprite spriteX;
    [SerializeField] Sprite spriteO;
    [SerializeField] int row, col;
    [SerializeField] LineRenderer lineRenderer;
    private Transform tf;
    public Transform TF
    {
        get {
            if (tf == null)
            {
                tf = transform;
            }
            return tf; }  
    }
    private void Awake()
    {
        OnInit();
    }
    public void SetPos(int _row,int _col)
    {
        row = _row;
        col = _col;
    }
    public void OnInit()
    {
        lineRenderer.enabled = false;
        state = CellType.empty;
        frameSprite.color = Color.black;
        visibleSprite.gameObject.SetActive(false);
    }
    public void Tick(CellType cellType)
    {

        state = cellType;
        switch (state)
        {
            case CellType.X:
                visibleSprite.gameObject.SetActive(true);
                visibleSprite.sprite = spriteX;
                frameSprite.color = Color.red;
                GameManager.instance.SetValueMatrix(row, col, 1);
                break;
            case CellType.O:
                visibleSprite.gameObject.SetActive(true);
                visibleSprite.sprite = spriteO;
                frameSprite.color = Color.blue;
                GameManager.instance.SetValueMatrix(row, col, -1);
                break;
            default:
                Debug.LogError(cellType);
                break;
        }
    }
    public void Connect(Cell cell,Color color)
    {
        Debug.Log(row.ToString() + " " + col.ToString());
        Debug.Log(cell.row.ToString() + " " + cell.col.ToString());

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        Vector3 direct = (cell.TF.position - TF.position).normalized;
        lineRenderer.SetPosition(0, TF.position -direct);
        lineRenderer.SetPosition(1, cell.TF.position+direct);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.instance.state is not GameState.Playing)
        {
            return;
        }
        if (state != CellType.empty)
        {
            return;
        }
        if (GameManager.instance.turnState is TurnState.Player1)
        {
            Tick(CellType.X);
            GameManager.instance.ChangeState(TurnState.Player2);
            if (GameManager.instance.gameMode is GameMode.PVB)
            {
                GameManager.instance.AIMakeMove();
            }
        }
        else if (GameManager.instance.gameMode is GameMode.PVP)
        {
            Tick(CellType.O);
            GameManager.instance.ChangeState(TurnState.Player1);
        }
    }
}
