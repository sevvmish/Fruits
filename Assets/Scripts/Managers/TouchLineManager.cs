using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLineManager : MonoBehaviour
{
    [SerializeField] private Material fruit1;
    [SerializeField] private Material fruit2;
    [SerializeField] private Material fruit3;
    [SerializeField] private Material fruit4;
    [SerializeField] private Material fruit5;
    [SerializeField] private Material fruit6;

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
        if (cells.Count == 0 || (!cells.Contains(cell) && cellType == cell.CellType))
        {
            if (cells.Count == 0)
            {
                cellType = cell.CellType;

                switch (cellType)
                {
                    case CellTypes.fruit1:
                        line.material = fruit1;
                        break;

                    case CellTypes.fruit2:
                        line.material = fruit2;
                        break;

                    case CellTypes.fruit3:
                        line.material = fruit3;
                        break;

                    case CellTypes.fruit4:
                        line.material = fruit4;
                        break;

                    case CellTypes.fruit5:
                        line.material = fruit5;
                        break;

                    case CellTypes.fruit6:
                        line.material = fruit6;
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
