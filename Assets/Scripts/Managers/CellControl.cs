using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellControl : MonoBehaviour
{
    public CellTypes CellType { get; private set; }
    public CellActions CellAction { get; private set; }

    [SerializeField] private Transform visual;
    [SerializeField] private GameObject clickVFX;
    [SerializeField] private GameObject cellBlowVFX;
    [SerializeField] private GameObject smallBlowVFX;

    private readonly float smallBlowVFXTime = Globals.SMALL_BLOW_TIME;
    private readonly float flyToMenuTime = Globals.FLY_TO_BONUS_TIME;

    private AssetManager assets;
    private GameObject apprearance;
    private bool isClicked;

    private float _timer, timerCooldown;

    private bool isDestroing;

    public void SetData(CellTypes _type, CellActions _action)
    {
        isDestroing = false;
        assets = GameManager.Instance.Assets;
        resetAll();
        CellType = _type;
        CellAction = _action;
        visual.localPosition = Vector3.zero;
        visual.localEulerAngles = Vector3.zero;
        visual.localScale = Vector3.one;
                
        apprearance = GameManager.Instance.Assets.GetFruitByType(CellType, CellAction);
        apprearance.transform.parent = visual;
        apprearance.transform.localPosition = Vector3.zero;
        apprearance.transform.localEulerAngles = Vector3.zero;
        apprearance.transform.localScale = Vector3.one;
        apprearance.SetActive(true);

        if (CellAction == CellActions.line_explosion_horizontal)
        {
            apprearance.transform.localEulerAngles = new Vector3(0, 0, 90);
        }

        

        Color color = Color.white;

        switch(CellType)
        {
            case CellTypes.fruit1:
                color = new Color(78f/256f, 206f/256f, 81f/256f, 1);
                break;

            case CellTypes.fruit2:
                color = new Color(236f / 256f, 236f / 256f, 107f / 256f, 1);
                break;

            case CellTypes.fruit3:
                color = new Color(56f / 256f, 140f / 256f, 231f / 256f, 1);
                break;

            case CellTypes.fruit4:
                color = new Color(1, 0.5f, 0.5f, 1);
                break;

            case CellTypes.fruit5:
                color = new Color(197f / 256f, 85f / 256f, 67f / 256f, 1);
                break;

            case CellTypes.fruit6:
                color = new Color(0.5f, 0, 0.5f, 1);
                break;
        }

        cellBlowVFX.GetComponent<ParticleSystem>().startColor = color;
        smallBlowVFX.GetComponent<ParticleSystem>().startColor = color;
        clickVFX.GetComponent<ParticleSystem>().startColor = color;

        timerCooldown = UnityEngine.Random.Range(2f, 5f);
    }

    private void Start()
    {
        if (CellAction == CellActions.simple)
        {
            if (CellType == CellTypes.fruit1 || CellType == CellTypes.fruit2 || CellType == CellTypes.fruit4)
            {
                cellBlowVFX.GetComponent<AudioSource>().clip = SoundUI.Instance.GetAudioClip(SoundsUI.fruit_blow);
            }
            else
            {
                cellBlowVFX.GetComponent<AudioSource>().clip = SoundUI.Instance.GetAudioClip(SoundsUI.berry_blow);
            }
        }
    }

    private void Update()
    {
        if (!isClicked && (CellAction == CellActions.line_explosion_vertical || CellAction == CellActions.line_explosion_horizontal))
        {
            if (_timer > timerCooldown)
            {
                _timer = 0;
                timerCooldown = UnityEngine.Random.Range(2f, 5f);

                switch (CellAction)
                {
                    case CellActions.line_explosion_vertical:
                        visual.DOKill();
                        //visual.DOPunchScale(new Vector3(0,1,0), 0.15f).SetEase(Ease.InOutSine);
                        visual.DOPunchPosition(new Vector3(0, UnityEngine.Random.Range(-0.15f, 0.15f), 0), 0.2f, 20).SetEase(Ease.Linear);
                        break;

                    case CellActions.line_explosion_horizontal:
                        visual.DOKill();
                        //visual.DOPunchScale(new Vector3(1, 0, 0), 0.15f).SetEase(Ease.InOutSine);
                        visual.DOPunchPosition(new Vector3(UnityEngine.Random.Range(-0.15f, 0.15f), 0, 0), 0.2f, 20).SetEase(Ease.Linear);
                        break;
                }
            }
            else
            {
                _timer += Time.deltaTime;
            }
            
        }
    }

    public void MakeOneTimeShakeScale(float _time, float _power, int vibrato)
    {
        if (isClicked) return;

        visual.DOKill();
        visual.DOShakeScale(_time, _power, vibrato).SetEase(Ease.Linear).OnComplete(()=> { visual.localScale = Vector3.one;});
    }
    
    public void CLickedEffect(bool isCl)
    {        
        if (isCl && !isClicked)
        {            
            visual.DOKill();

            isClicked = true;
            clickVFX.SetActive(true);
            visual.localScale = Vector3.one * 1.2f;
            visual.DOPunchScale(Vector3.one * 1.25f, 0.15f).SetEase(Ease.InOutSine);
            RotatorControl.Instance.Add(visual);
        }
        else if(!isCl && isClicked)
        {
            visual.DOKill();

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
        cellBlowVFX.SetActive(false);
        smallBlowVFX.SetActive(false);
    }

    public void DestroyCell(bool isCountable)
    {
        StartCoroutine(playDestroy(isCountable));
    }
    private IEnumerator playDestroy(bool isCountable)
    {
        if (isDestroing) yield break;

        isDestroing = true;

        switch(CellAction)
        {
            case CellActions.simple:
                cellBlowVFX.SetActive(true);
                break;

            case CellActions.small_explosion:
                smallBlowVFX.SetActive(true);
                break;
        }

        

        if (!Globals.Cells.ContainsKey(new Vector2(transform.position.x, transform.position.y)))
        {
            Debug.LogError("trying to remove cell which not exists");
            yield break;
        }
        else if (Globals.Cells[new Vector2(transform.position.x, transform.position.y)].Equals(this))
        {
            Globals.Cells.Remove(new Vector2(transform.position.x, transform.position.y));
        }
        

        if (isCountable)
        {
            visual.DOKill();
            visual.localScale = Vector3.one;

            yield return new WaitForSeconds(Time.deltaTime);

            float distKoeff = (visual.position - new Vector3(0, 7, -1)).magnitude / 7f;
            visual.DOMove(new Vector3(0, 7, -1), flyToMenuTime * distKoeff).SetEase(Ease.Linear);
            float timer = (flyToMenuTime * distKoeff) > smallBlowVFXTime ? (flyToMenuTime * distKoeff) : smallBlowVFXTime;
            yield return new WaitForSeconds(timer);
            assets.ReturnFruitByType(CellType, CellAction, apprearance);
            assets.ReturnCell(gameObject);
        }
        else
        {
            apprearance.SetActive(false);
            assets.ReturnFruitByType(CellType, CellAction, apprearance);
            yield return new WaitForSeconds(smallBlowVFXTime);            
            assets.ReturnCell(gameObject);
        }        
    }
    public void ReleaseAllGameobjects()
    {
        assets.ReturnFruitByType(CellType, CellAction, apprearance);
        assets.ReturnCell(gameObject);
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

public enum CellActions
{
    simple,
    small_explosion,
    line_explosion_vertical,
    line_explosion_horizontal
}