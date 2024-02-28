using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellControl : MonoBehaviour
{
    public CellTypes cellType { get; private set; }

    [SerializeField] private Transform visual;
    [SerializeField] private GameObject clickVFX;
    [SerializeField] private GameObject smallBlowVFX;
    [SerializeField] private GameObject errorVFX;

    private readonly float smallBlowVFXTime = 0.3f;
    private readonly float flyToMenuTime = 0.5f;

    private AssetManager assets;
    private GameObject apprearance;
    private bool isClicked;
    private static int[] cellsForResults = new int[] { 1, 2, 3, 4, 5, 6 };
    private static int[] cellsForAction = new int[] { 0 };

    public void SetData(CellTypes _type)
    {
        assets = GameManager.Instance.Assets;
        resetAll();
        cellType = _type;
        visual.localPosition = Vector3.zero;
        visual.localEulerAngles = Vector3.zero;

        apprearance = GameManager.Instance.Assets.GetFruitByType(cellType);
        apprearance.transform.parent = visual;
        apprearance.transform.localPosition = Vector3.zero;
        apprearance.transform.localEulerAngles = Vector3.zero;
        apprearance.SetActive(true);
    }

    public static bool IsCellForResult(CellTypes _type)
    {
        return cellsForResults.Contains((int)_type);
    }

    public static bool IsCellForAction(CellTypes _type)
    {
        return cellsForAction.Contains((int)_type);
    }

    public void CLickedEffect(bool isCl)
    {
        if (isCl && !isClicked)
        {
            isClicked = true;
            clickVFX.SetActive(true);
            visual.DOPunchScale(Vector3.one * 1.2f, 0.15f).SetEase(Ease.InOutSine);
            RotatorControl.Instance.Add(visual);
        }
        else if(!isCl && isClicked)
        {
            isClicked = false;
            clickVFX.SetActive(false);
            RotatorControl.Instance.Remove(visual);
            visual.localEulerAngles = Vector3.zero;
        }
    }

    private void resetAll()
    {
        CLickedEffect(false);
        clickVFX.SetActive(false);
        smallBlowVFX.SetActive(false);
        errorVFX.SetActive(false);
    }

    public void DestroyCell(bool isCountable)
    {
        StartCoroutine(playDestroy(isCountable));
    }
    private IEnumerator playDestroy(bool isCountable)
    {
        smallBlowVFX.SetActive(true);

        if (Globals.Cells[new Vector2(transform.position.x, transform.position.y)].Equals(this))
        {
            Globals.Cells.Remove(new Vector2(transform.position.x, transform.position.y));
        }
        else
        {
            Debug.LogError("trying to remove cell which not exists");
        }

        if (isCountable)
        {
            visual.DOMove(new Vector3(0, 7, 0), flyToMenuTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(flyToMenuTime);
            releaseGameobjects();
        }
        else
        {
            apprearance.SetActive(false);
            yield return new WaitForSeconds(smallBlowVFXTime);
            releaseGameobjects();
        }        
    }
    private void releaseGameobjects()
    {
        assets.ReturnFruitByType(cellType, apprearance);
        assets.ReturnCell(this.gameObject);
    }
}

public enum CellTypes
{
    none,
    fruit1,
    fruit2,
    fruit3,
    fruit4,
    fruit5,
    fruit6
}
