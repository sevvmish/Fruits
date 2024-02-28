using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private Transform GameField;
    [SerializeField] private GameObject cellExample;

    public void SetData()
    {
        Globals.Cells = new Dictionary<Vector2, CellControl> ();

        float fromX = Globals.FieldDimention.x / 2f - 0.5f;
        float fromY = Globals.FieldDimention.y / 2f - 0.5f;

        for (float x = -fromX; x <= fromX; x++)
        {
            for (float y = -fromY; y <= fromY; y++)
            {
                GameObject g = Instantiate(cellExample, GameField);
                g.transform.position = new Vector3(x, y, 0);
                g.transform.eulerAngles = Vector3.zero;
                CellControl cell = g.GetComponent<CellControl>();
                cell.SetData((CellTypes)UnityEngine.Random.Range(1,3));
                g.SetActive(true);
                Globals.Cells.Add(new Vector2(x, y), cell);
            }
        }
    }
}
