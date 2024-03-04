using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using UnityEngine.SceneManagement;
using YG;

public class UIManager : MonoBehaviour
{
    [Header("How many objects in line")]
    [SerializeField] private GameObject objectAmountInformer;
    [SerializeField] private TextMeshProUGUI objectAmountText;
    private RectTransform backForObjectAmount;
    private int objectAmount;
    private float _timerAmount;

    [Header("Main info panel")]
    [SerializeField] private GameObject mainInfoPanel;


    [Header("ADV")]
    [SerializeField] private Rewarded rewarded;

        

    [Header("ADV")]
    [SerializeField] private Interstitial interstitial;
    private string whatLevelToLoadAfterAdv;


    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        ScreenSaver.Instance.ShowScreen();

        objectAmountInformer.SetActive(false);
        backForObjectAmount = objectAmountText.transform.parent.GetComponent<RectTransform>();
    }
    
    public void SetObjectAmount(int amount)
    {
        objectAmount = amount;
                
        if (objectAmount >= 5)
        {                  

            switch (objectAmount)
            {
                case 5:
                    backForObjectAmount.sizeDelta = new Vector2 (120, 120);
                    objectAmountText.fontSize = 50;
                    break;

                case 6:
                    backForObjectAmount.sizeDelta = new Vector2(130, 130);
                    objectAmountText.fontSize = 55;
                    break;

                case 7:
                    backForObjectAmount.sizeDelta = new Vector2(150, 150);
                    objectAmountText.fontSize = 60;
                    break;

                case 8:
                    backForObjectAmount.sizeDelta = new Vector2(150, 150);
                    objectAmountText.fontSize = 65;
                    break;

                default:
                    backForObjectAmount.sizeDelta = new Vector2(160, 160);
                    objectAmountText.fontSize = 70;
                    break;
            }

            if (!objectAmountInformer.activeSelf)
            {
                objectAmountInformer.SetActive(true);

                if (objectAmount < 7)
                {
                    objectAmountInformer.transform.localScale = Vector3.zero;
                    objectAmountInformer.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutSine).OnComplete(() => { objectAmountInformer.transform.DOShakeScale(0.1f, 1, 30).SetEase(Ease.InOutQuad); });
                }                
            }

            

        }
        else
        {
            if (objectAmountInformer.activeSelf) objectAmountInformer.SetActive(false);                            
        }

        objectAmountText.text = "+" + objectAmount.ToString();
    }

    private void Update()
    {
        if (objectAmount >=7)
        {
            float koeff = 5f;

            switch(objectAmount)
            {
                case 7:
                    koeff = 5f;
                    break;

                case 8:
                    koeff = 8f;
                    break;

                default:
                    koeff = 12f;
                    break;
            }

            if (_timerAmount > 0.25f)
            {
                _timerAmount = 0;
                objectAmountInformer.transform.DOPunchPosition(new Vector3(UnityEngine.Random.Range(-2f, 2f) * koeff, UnityEngine.Random.Range(-2f, 2f) * koeff, 1), 0.2f, 30).SetEase(Ease.InOutQuad);
            }
            else
            {
                _timerAmount += Time.deltaTime;
            }
        }
    }


}
