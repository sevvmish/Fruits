using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSaver : MonoBehaviour
{
    public static ScreenSaver Instance { get; private set; }

    [SerializeField] private RectTransform type1;
    [SerializeField] private RectTransform type2;

    private readonly float additionalWait = 0.3f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        type1.gameObject.SetActive(true);
        type1.anchoredPosition = new Vector3(-1144, 954, 0);

        type2.gameObject.SetActive(true);
        type2.anchoredPosition = new Vector3(1372, -501, 0);
    }

    public void HideScreen()
    {
        type1.gameObject.SetActive(true);
        type1.anchoredPosition = new Vector3(-2139, 1528, 0);
        type1.DOAnchorPos3D(new Vector3(-1144, 954, 0), Globals.SCREEN_SAVER_AWAIT + additionalWait).SetEase(Ease.Linear);

        type2.gameObject.SetActive(true);
        type2.anchoredPosition = new Vector3(2411, -1100, 0);
        type2.DOAnchorPos3D(new Vector3(1372, -501, 0), Globals.SCREEN_SAVER_AWAIT + additionalWait).SetEase(Ease.Linear);
    }

    public void FastShowScreen()
    {        
        StartCoroutine(deactivateAfter(0));
    }

    public void ShowScreen()
    {
        if (Globals.IsDevelopmentBuild)
        {            
            type1.gameObject.SetActive(true);
            type1.anchoredPosition = new Vector3(-2139, 1528, 0);

            type2.gameObject.SetActive(true);
            type2.anchoredPosition = new Vector3(2411, -1100, 0);

            StartCoroutine(deactivateAfter(0));
            return;
        }

        type1.gameObject.SetActive(true);
        type1.anchoredPosition = new Vector3(-1144, 954, 0);
        type1.DOAnchorPos3D(new Vector3(-2139, 1528, 0), Globals.SCREEN_SAVER_AWAIT + additionalWait).SetEase(Ease.Linear);

        type2.gameObject.SetActive(true);
        type2.anchoredPosition = new Vector3(1372, -501, 0);
        type2.DOAnchorPos3D(new Vector3(2411, -1100, 0), Globals.SCREEN_SAVER_AWAIT + additionalWait).SetEase(Ease.Linear);

        StartCoroutine(deactivateAfter(Globals.SCREEN_SAVER_AWAIT));
    }

    private IEnumerator deactivateAfter(float secs)
    {
        yield return new WaitForSeconds(secs);
        type1.gameObject.SetActive(false);
        type2.gameObject.SetActive(false);
        
    }


}
