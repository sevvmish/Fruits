using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    [SerializeField] private TouchLineManager touchLine;
    [SerializeField] private FieldManager fieldManager;

    private Camera _camera;

    private Ray ray;
    private RaycastHit hit;
    private readonly float cameraRayCast = 30f;
    private GameManager gm;
    private GameObject lastPlaceForAudioListener;
    private float _cooldown;
    private Vector3 prevPos;

    private CellTypes lastCellType;
    private CellControl lastCell;
    private HashSet<CellControl> outlinedCells = new HashSet<CellControl>();
    private List<CellControl> cells = new List<CellControl>();

    public void SetData(Camera c)
    {
        _camera = c;
        setAudioListener(_camera.gameObject);
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {        
        if (!gm.IsGameStarted) return;
        if (_cooldown > 0) _cooldown -= Time.deltaTime;

        
        if (Input.GetMouseButton(0) && _cooldown <= 0)
        {
            ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, cameraRayCast))
            {
                if (hit.collider.TryGetComponent(out CellControl cell))
                {
                    if (lastCell == null)
                    {
                        addCell(cell);
                    }
                    else if (lastCell != null 
                        && lastCell != cell
                        && !outlinedCells.Contains(cell) 
                        && lastCellType == cell.CellType
                        && (lastCell.transform.position - cell.transform.position).magnitude < 1.85f)
                    {
                        addCell(cell);
                    }
                    else if (outlinedCells.Count > 1 && cells[cells.Count - 2] == cell)
                    {
                        removePreviousCell(lastCell);
                    }
                    
                    //print(cell.gameObject.name);
                    
                }                
                //_cooldown = Time.deltaTime;
            }
            else
            {
                if (outlinedCells.Count > 0)
                {
                    //touchLine.Add(Input.mousePosition);
                }
            }
        }
        else if (!Input.GetMouseButton(0))
        {
            if (outlinedCells.Count >= 3)
            {
                fieldManager.UndoCells(outlinedCells.ToArray());
                resetCells();
            }
            else if(outlinedCells.Count >= 1)
            {
                SoundUI.Instance.PlayUISound(SoundsUI.error, 0.2f);
                resetCells();
            }
            
        }
    }

    private void addCell(CellControl cell)
    {
        SoundUI.Instance.PlayUISound(SoundsUI.click, 0.3f);
        outlinedCells.Add(cell);
        cells.Add(cell);
        lastCellType = cell.CellType;
        lastCell = cell;
        touchLine.Add(cell);
        cell.CLickedEffect(true);
    }

    private void removePreviousCell(CellControl cell)
    {
        SoundUI.Instance.PlayUISound(SoundsUI.pop, 0.2f);
        outlinedCells.Remove(cell);
        cells.Remove(cell);
        cell.CLickedEffect(false);
        lastCell = cells[cells.Count - 1];
        touchLine.RemovePrevoius(cell);
    }

    private void resetCells()
    {
        foreach (CellControl item in outlinedCells)
        {
            item.CLickedEffect(false);
        }

        outlinedCells.Clear();
        cells.Clear();
        lastCellType = CellTypes.none;
        lastCell = null;
        touchLine.Reset();
    }

    private void setAudioListener(GameObject g)
    {
        if (lastPlaceForAudioListener != null && lastPlaceForAudioListener.Equals(g)) return;

        if (lastPlaceForAudioListener != null && lastPlaceForAudioListener.TryGetComponent(out AudioListener al))
        {
            al.enabled = false;
        }  
        
        if (g.TryGetComponent(out AudioListener al1))
        {
            al1.enabled = true;
        }
        else
        {
            g.AddComponent<AudioListener>();
        }

        lastPlaceForAudioListener = g;
        
    }
        
}
