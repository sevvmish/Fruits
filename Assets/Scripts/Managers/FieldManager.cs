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

    private readonly float cellSpeed = 0.2f;

    

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
        
        CellTypes _type = CellTypes.none;

        for (int i = 0; i < cells.Length; i++)
        {
            if (CellControl.IsCellForResult(cells[i].CellType))
            {
                _type = cells[i].CellType;

                bool result = gm.CountCell(cells[i]);
                cells[i].DestroyCell(result);                
            }
            else if(CellControl.IsCellForAction(cells[i].CellType))
            {

            }            
        }

        if (_type == CellTypes.fruit1 || _type == CellTypes.fruit2 || _type == CellTypes.fruit4)
        {
            SoundUI.Instance.PlayUISound(SoundsUI.fruit_blow, 0.3f);
        }
        else
        {
            SoundUI.Instance.PlayUISound(SoundsUI.berry_blow, 0.3f);
        }

        StartCoroutine(rearrangeCellsDelay(Globals.SMALL_BLOW_TIME));
    }
    private IEnumerator rearrangeCellsDelay(float sec)
    {
        yield return new WaitForSeconds(sec);
        rearrangeCells();
    }

    private void rearrangeCells()
    {
        bool isOK = true;

        for (int j = 0; j < 1000; j++)
        {
            foreach (Vector2 location in cellsToPlay)
            {
                if (!Globals.Cells.ContainsKey(location))
                {
                    isOK = false;
                    print("detected");

                    for (int i = 1; i < 1000; i++)
                    {
                        Vector2 toCheck = new Vector2(location.x, location.y + i);
                        if (cellsToPlay.Contains(toCheck) && Globals.Cells.ContainsKey(toCheck))
                        {
                            CellControl cell = Globals.Cells[toCheck];
                            cell.transform.DOMove(location, cellSpeed).SetEase(Ease.Linear);
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
                                
                cell.transform.DOMove(new Vector3(respawnCells[i].x, respawnCells[i].y, 0), cellSpeed).SetEase(Ease.Linear);
                Globals.Cells.Add(new Vector2(respawnCells[i].x, respawnCells[i].y), cell);
            }
        }
    }

    private CellControl getNewCell()
    {
        CellTypes _type = levelManager.ApprovedSimpleCells[UnityEngine.Random.Range(0, levelManager.ApprovedSimpleCells.Length)];

        CellControl cell = assets.GetCell();
        cell.SetData(_type, CellActions.simple);
        cell.gameObject.SetActive(true);
        return cell;
    }
}
