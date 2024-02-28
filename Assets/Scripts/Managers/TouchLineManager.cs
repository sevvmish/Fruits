using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLineManager : MonoBehaviour
{
    [SerializeField] private Material fruit1;
    [SerializeField] private Material fruit2;

    private LineRenderer line;
    private CellTypes cellType;
    private HashSet<CellControl> cells = new HashSet<CellControl>();
    private List<CellControl> cellsOrdered = new List<CellControl>();

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Add(CellControl cell)
    {
        if (cells.Count == 0 || (!cells.Contains(cell) && cellType == cell.cellType))
        {
            if (cells.Count == 0)
            {
                cellType = cell.cellType;

                switch (cellType)
                {
                    case CellTypes.fruit1:
                        line.material = fruit1;
                        break;

                    case CellTypes.fruit2:
                        line.material = fruit2;
                        break;
                }
            }

            cells.Add(cell);
            cellsOrdered.Add(cell);
            line.positionCount = cells.Count + 1;
            line.SetPosition(line.positionCount - 2, cell.transform.position);
            line.SetPosition(line.positionCount - 1, cell.transform.position);
        }        
    }

    public void RemovePrevoius(CellControl cell)
    {
        if (cell.Equals(cellsOrdered[cellsOrdered.Count - 1]))
        {
            cells.Remove(cell);
            cellsOrdered.Remove(cell);
            line.positionCount = cells.Count + 1;
            line.SetPosition(line.positionCount - 1, cellsOrdered[cellsOrdered.Count - 1].transform.position);
        }
    }

    public void Add(Vector3 pos)
    {
        line.SetPosition(line.positionCount - 1, pos);
    }

    public void Reset()
    {
        cellType = CellTypes.none;
        cells.Clear();
        cellsOrdered.Clear();
        line.positionCount = 1;
    }

}
