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

    private readonly float smallBlowVFXTime = Globals.SMALL_BLOW_TIME;
    private readonly float flyToMenuTime = Globals.FLY_TO_BONUS_TIME;

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
        visual.localScale = Vector3.one;

        apprearance = GameManager.Instance.Assets.GetFruitByType(cellType);
        apprearance.transform.parent = visual;
        apprearance.transform.localPosition = Vector3.zero;
        apprearance.transform.localEulerAngles = Vector3.zero;
        apprearance.transform.localScale = Vector3.one;
        apprearance.SetActive(true);

        Color color = Color.white;

        switch(cellType)
        {
            case CellTypes.fruit1:
                color = Color.green;
                break;

            case CellTypes.fruit2:
                color = Color.yellow;
                break;

            case CellTypes.fruit3:
                color = Color.blue;
                break;

            case CellTypes.fruit4:
                color = new Color(1, 0.5f, 0.5f, 1);
                break;

            case CellTypes.fruit5:
                color = Color.red;
                break;

            case CellTypes.fruit6:
                color = new Color(0.5f, 0, 0.5f, 1);
                break;
        }

        smallBlowVFX.GetComponent<ParticleSystem>().startColor = color;
        clickVFX.GetComponent<ParticleSystem>().startColor = color;
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
            visual.localScale = Vector3.one * 1.2f;
            visual.DOPunchScale(Vector3.one * 1.25f, 0.15f).SetEase(Ease.InOutSine);
            RotatorControl.Instance.Add(visual);
        }
        else if(!isCl && isClicked)
        {
            isClicked = false;
            clickVFX.SetActive(false);            
            RotatorControl.Instance.Remove(visual);
            visual.localEulerAngles = Vector3.zero;
            visual.localScale = Vector3.one;
        }
    }

    private void resetAll()
    {
        CLickedEffect(false);
        clickVFX.SetActive(false);
        smallBlowVFX.SetActive(false);
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
            visual.localScale = Vector3.one;
            
            float distKoeff = (visual.position - new Vector3(0, 7, -1)).magnitude / 7f;
            visual.DOMove(new Vector3(0, 7, -1), flyToMenuTime * distKoeff).SetEase(Ease.Linear);
            float timer = (flyToMenuTime * distKoeff) > smallBlowVFXTime ? (flyToMenuTime * distKoeff) : smallBlowVFXTime;
            yield return new WaitForSeconds(timer);
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
