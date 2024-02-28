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

        //ScreenSaver.Instance.ShowScreen();
    }
    


   
}
