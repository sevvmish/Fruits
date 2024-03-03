using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private Transform GameField;
    //[SerializeField] private GameObject cellExample;
    [SerializeField] private GameObject backExample;

    private AssetManager assets;
    private GameManager gm;
    private LevelManager levelManager;
    private HashSet<Vector2> cellsToPlay = new HashSet<Vector2>();
    private List<Vector2> respawnCells = new List<Vector2>();

    private readonly float cellSpeed = 0.12f;

    

    public void SetData()
    {
        gm = GameManager.Instance;
        assets = GameManager.Instance.Assets;
        this.levelManager = gm.LevelManager;
        Globals.Cells = new Dictionary<Vector2, CellControl> ();

        float fromX = Globals.FieldDimention.x / 2f - 0.5f;
        float fromY = Globals.FieldDimention.y / 2f - 0.5f;

        for (float x = -fromX; x <= fromX; x++)
        {
            for (float y = -fromY; y <= fromY; y++)
            {
                CellControl cell = getNewCell();
                
                cell.transform.position = new Vector3(x, y, 0);
                cell.transform.eulerAngles = Vector3.zero;                
                Globals.Cells.Add(new Vector2(x, y), cell);
                cellsToPlay.Add(new Vector2(x, y));
                GameObject b = Instantiate(backExample, GameField);
                b.transform.position = new Vector3(x, y, 0.5f);
                b.transform.localScale = Vector3.one * 0.97f;
                b.SetActive(true);

                if (y == fromY)
                {
                    respawnCells.Add(new Vector2(x, y));
                }
            }
        }                
    }

    public void UndoCells(CellControl[] cells)
    {
        HashSet<CellActions> actions = new HashSet<CellActions>();
        CellTypes _type = CellTypes.none;
        if (cells.Length > 0) _type = cells[0].CellType;

        for (int i = 0; i < cells.Length; i++)
        {
            actions.Add(cells[i].CellAction);

            switch (cells[i].CellAction)
            {
                case CellActions.simple:
                    bool result = gm.CountCell(cells[i]);
                    cells[i].DestroyCell(result);
                    break;

                case CellActions.small_explosion:
                    cells[i].DestroyCell(false);
                    destroyCellFromSmallBlow(cells[i]);
                    break;
            }
                           
        }

        if (actions.Contains(CellActions.small_explosion))
        {
            SoundUI.Instance.PlayUISound(SoundsUI.small_blow, 0.2f);
        }
        else if (actions.Contains(CellActions.simple))
        {
            if (_type == CellTypes.fruit1 || _type == CellTypes.fruit2 || _type == CellTypes.fruit4)
            {
                SoundUI.Instance.PlayUISound(SoundsUI.fruit_blow, 0.3f);
            }
            else
            {
                SoundUI.Instance.PlayUISound(SoundsUI.berry_blow, 0.3f);
            }
        }

        
        

        StartCoroutine(rearrangeCellsDelay(Globals.SMALL_BLOW_TIME));
    }
    private IEnumerator rearrangeCellsDelay(float sec)
    {
        yield return new WaitForSeconds(sec);
        rearrangeCells();
    }

    private void destroyCellFromSmallBlow(CellControl cell)
    {
        List<CellControl> cells = new List<CellControl>();
        

        if (Globals.Cells.ContainsKey(new Vector2(cell.transform.position.x + 1, cell.transform.position.y)))
        {
            cells.Add(Globals.Cells[new Vector2(cell.transform.position.x + 1, cell.transform.position.y)]);
        }

        if (Globals.Cells.ContainsKey(new Vector2(cell.transform.position.x - 1, cell.transform.position.y)))
        {
            cells.Add(Globals.Cells[new Vector2(cell.transform.position.x - 1, cell.transform.position.y)]);
        }

        if (Globals.Cells.ContainsKey(new Vector2(cell.transform.position.x, cell.transform.position.y + 1)))
        {
            cells.Add(Globals.Cells[new Vector2(cell.transform.position.x, cell.transform.position.y + 1)]);
        }

        if (Globals.Cells.ContainsKey(new Vector2(cell.transform.position.x, cell.transform.position.y - 1)))
        {
            cells.Add(Globals.Cells[new Vector2(cell.transform.position.x, cell.transform.position.y - 1)]);
        }

        if (Globals.Cells.ContainsKey(new Vector2(cell.transform.position.x - 1, cell.transform.position.y - 1)))
        {
            cells.Add(Globals.Cells[new Vector2(cell.transform.position.x - 1, cell.transform.position.y - 1)]);
        }

        if (Globals.Cells.ContainsKey(new Vector2(cell.transform.position.x + 1, cell.transform.position.y + 1)))
        {
            cells.Add(Globals.Cells[new Vector2(cell.transform.position.x + 1, cell.transform.position.y + 1)]);
        }

        if (Globals.Cells.ContainsKey(new Vector2(cell.transform.position.x + 1, cell.transform.position.y - 1)))
        {
            cells.Add(Globals.Cells[new Vector2(cell.transform.position.x + 1, cell.transform.position.y - 1)]);
        }

        if (Globals.Cells.ContainsKey(new Vector2(cell.transform.position.x - 1, cell.transform.position.y + 1)))
        {
            cells.Add(Globals.Cells[new Vector2(cell.transform.position.x - 1, cell.transform.position.y + 1)]);
        }

       
        UndoCells(cells.ToArray());
    }

    private void rearrangeCells()
    {
        bool isOK = true;

        for (int j = 0; j < 1000; j++)
        {
            isOK = true;

            foreach (Vector2 location in cellsToPlay)
            {
                if (!Globals.Cells.ContainsKey(location))
                {
                    isOK = false;
                    
                    for (int i = 1; i < 1000; i++)
                    {
                        Vector2 toCheck = new Vector2(location.x, location.y + i);
                        if (cellsToPlay.Contains(toCheck) && Globals.Cells.ContainsKey(toCheck))
                        {
                            CellControl cell = Globals.Cells[toCheck];
                            //cell.transform.DOMove(location, cellSpeed).SetEase(Ease.Linear);
                            moveCell(cell, location);
                            Globals.Cells.Remove(toCheck);
                            Globals.Cells.Add(location, cell);
                            break;
                        }
                        else if (!cellsToPlay.Contains(toCheck))
                        {
                            checkRespawns();
                            break;
                        }
                        if (i == 999) print("to much1");
                    }
                }
            }

            checkRespawns();

            if (isOK) break;
            if (j == 999) print("to much2");
        }        
    }

    private void checkRespawns()
    {
        for (int i = 0; i < respawnCells.Count; i++)
        {
            if (!Globals.Cells.ContainsKey(respawnCells[i]))
            {
                CellControl cell = getNewCell();
                
                cell.transform.position = new Vector3(respawnCells[i].x, respawnCells[i].y + 1, 0);
                cell.transform.eulerAngles = Vector3.zero;

                //cell.transform.DOMove(new Vector3(respawnCells[i].x, respawnCells[i].y, 0), cellSpeed).SetEase(Ease.Linear);
                moveCell(cell, new Vector3(respawnCells[i].x, respawnCells[i].y, 0));
                Globals.Cells.Add(new Vector2(respawnCells[i].x, respawnCells[i].y), cell);
            }
        }
    }

    private CellControl getNewCell()
    {
        CellActions _action = CellActions.simple;
        
        int rnd = UnityEngine.Random.Range(0,101);

        Dictionary<CellActions, int> approvedCellActions = levelManager.ApprovedCellActions;
        int minValue = 0;
        foreach (CellActions item in approvedCellActions.Keys)
        {
            minValue += approvedCellActions[item];

            if (rnd <= minValue)
            {
                _action = item;
                break;
            }
        }

        CellTypes _type = levelManager.ApprovedFruitTypeCells[UnityEngine.Random.Range(0, levelManager.ApprovedFruitTypeCells.Length)];

        CellControl cell = assets.GetCell();
        cell.SetData(_type, _action);
        cell.gameObject.SetActive(true);
        return cell;
    }

    private void moveCell(CellControl cell, Vector3 pos)
    {
        cell.transform.DOKill();
        StartCoroutine(playMove(cell, pos));
    }
    private IEnumerator playMove(CellControl cell, Vector3 pos)
    {
        cell.transform.DOMove(pos, cellSpeed).SetEase(Ease.Linear);

        yield return new WaitForSeconds(cellSpeed);

        cell.MakeOneTimeShakeScale(0.2f, 0.7f);

    }

    public List<CellControl> FindCombination()
    {
        Dictionary<Vector2, List<Vector2>> results = new Dictionary<Vector2, List<Vector2>>();
        foreach (Vector2 item in Globals.Cells.Keys)
        {
            List<Vector2> result = new List<Vector2>();
            findSurroundings(Globals.Cells[item], ref result);
            if (result.Count > 2) results.Add(item, result);

            if (results.Count > 3) break;
        }

        int current = 1;
        List<Vector2> endResult = new List<Vector2>();

        foreach (var item in results.Keys)
        {
            if (results[item].Count > 2 && results[item].Count > current)
            {
                if ((endResult.Count > 0 && results[item].Count <= 6) || endResult.Count == 0)
                endResult = results[item];
                /*
                print(results[item].Count + ": =================");
                for (int i = 0; i < results[item].Count; i++)
                {
                    print(i + ": " + results[item][i]);
                }
                print("=================");*/
            }
        }

        List<CellControl> cellsResult = new List<CellControl>();

        for (int i = 0; i < endResult.Count; i++)
        {
            cellsResult.Add(Globals.Cells[endResult[i]]);
        }

        return cellsResult;
    }

    private List<Vector2> findSurroundings(CellControl cell, ref List<Vector2> result)
    {        
        
        Vector2 vec = new Vector2(cell.transform.position.x, cell.transform.position.y);
        result.Add(vec);
                        
        if (Globals.Cells.ContainsKey(new Vector2(vec.x + 1, vec.y)) 
            && !result.Contains(new Vector2(vec.x + 1, vec.y))
            && cell.CellType == Globals.Cells[new Vector2(vec.x + 1, vec.y)].CellType)
        {
            findSurroundings(Globals.Cells[new Vector2(vec.x + 1, vec.y)], ref result);
        }

        else if (Globals.Cells.ContainsKey(new Vector2(vec.x - 1, vec.y))
            && !result.Contains(new Vector2(vec.x - 1, vec.y))
            && cell.CellType == Globals.Cells[new Vector2(vec.x - 1, vec.y)].CellType)
        {
            findSurroundings(Globals.Cells[new Vector2(vec.x - 1, vec.y)], ref result);
        }

        else if (Globals.Cells.ContainsKey(new Vector2(vec.x - 1, vec.y-1))
            && !result.Contains(new Vector2(vec.x - 1, vec.y-1))
            && cell.CellType == Globals.Cells[new Vector2(vec.x - 1, vec.y-1)].CellType)
        {
            findSurroundings(Globals.Cells[new Vector2(vec.x - 1, vec.y-1)], ref result);
        }

        else if (Globals.Cells.ContainsKey(new Vector2(vec.x + 1, vec.y + 1))
            && !result.Contains(new Vector2(vec.x + 1, vec.y + 1))
            && cell.CellType == Globals.Cells[new Vector2(vec.x + 1, vec.y + 1)].CellType)
        {
            findSurroundings(Globals.Cells[new Vector2(vec.x + 1, vec.y + 1)], ref result);
        }

        else if (Globals.Cells.ContainsKey(new Vector2(vec.x + 1, vec.y - 1))
            && !result.Contains(new Vector2(vec.x + 1, vec.y - 1))
            && cell.CellType == Globals.Cells[new Vector2(vec.x + 1, vec.y - 1)].CellType)
        {
            findSurroundings(Globals.Cells[new Vector2(vec.x + 1, vec.y - 1)], ref result);
        }

        else if (Globals.Cells.ContainsKey(new Vector2(vec.x - 1, vec.y + 1))
            && !result.Contains(new Vector2(vec.x - 1, vec.y + 1))
            && cell.CellType == Globals.Cells[new Vector2(vec.x - 1, vec.y + 1)].CellType)
        {
            findSurroundings(Globals.Cells[new Vector2(vec.x - 1, vec.y + 1)], ref result);
        }

        else if (Globals.Cells.ContainsKey(new Vector2(vec.x, vec.y + 1))
            && !result.Contains(new Vector2(vec.x, vec.y + 1))
            && cell.CellType == Globals.Cells[new Vector2(vec.x, vec.y + 1)].CellType)
        {
            findSurroundings(Globals.Cells[new Vector2(vec.x, vec.y + 1)], ref result);
        }

        else if (Globals.Cells.ContainsKey(new Vector2(vec.x, vec.y - 1))
            && !result.Contains(new Vector2(vec.x, vec.y - 1))
            && cell.CellType == Globals.Cells[new Vector2(vec.x, vec.y - 1)].CellType)
        {
            findSurroundings(Globals.Cells[new Vector2(vec.x, vec.y - 1)], ref result);
        }


        return result;
    }
}
