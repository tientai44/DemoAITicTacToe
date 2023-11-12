using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CellType
{
    X, O, empty
}
public class Cell : MonoBehaviour
{
    CellType state = CellType.empty;
    [SerializeField] GameObject hint;
    [SerializeField] SpriteRenderer visibleSprite;
    [SerializeField] SpriteRenderer frameSprite;
    [SerializeField] Sprite spriteX;
    [SerializeField] Sprite spriteO;
    [SerializeField] int row, col;
    [SerializeField] LineRenderer lineRenderer;
    private static Cell prevCell;
    private Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    public CellType State { get => state; set => state = value; }

    private void Awake()
    {
        OnInit();
    }
    public void SetPos(int _row, int _col)
    {
        row = _row;
        col = _col;
    }
    public void OnInit()
    {
        ActiveHine(false);
        lineRenderer.enabled = false;
        state = CellType.empty;
        frameSprite.color = Color.black;
        visibleSprite.gameObject.SetActive(false);
    }
    public void Tick(CellType cellType)
    {
        if (prevCell)
        {
            prevCell.ActiveHine(false);
        }
        prevCell = this;
        prevCell.ActiveHine(true);
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
    public void ActiveHine(bool active)
    {
        hint.SetActive(active);
    }
    public void Connect(Cell cell, Color color)
    {
        Debug.Log(row.ToString() + " " + col.ToString());
        Debug.Log(cell.row.ToString() + " " + cell.col.ToString());

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        Vector3 direct = (cell.TF.position - TF.position).normalized;
        lineRenderer.SetPosition(0, TF.position - direct);
        lineRenderer.SetPosition(1, cell.TF.position + direct);
    }
    
}
