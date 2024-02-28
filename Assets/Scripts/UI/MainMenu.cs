using DG.Tweening;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenu : MonoBehaviour
{    
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }


    private void Start()
    {
        if (Globals.IsInitiated)
        {
            Localize();
            playWhenInitialized();            
        }        
    }
    
    private void playWhenInitialized()
    {          
        YandexGame.StickyAdActivity(true);
     
        if (Globals.IsMusicOn)
        {
            int ambMusic = UnityEngine.Random.Range(0, 3);
            switch (ambMusic)
            {
                case 0:
                    AmbientMusic.Instance.PlayAmbient(AmbientMelodies.loop_melody3);
                    break;

                case 1:
                    AmbientMusic.Instance.PlayAmbient(AmbientMelodies.loop_melody4);
                    break;

                case 2:
                    AmbientMusic.Instance.PlayAmbient(AmbientMelodies.loop_melody5);
                    break;
            }
        }

        StartCoroutine(playStart());

    }

    
    private IEnumerator playStart()
    {
        //ScreenSaver.Instance.HideScreen();
        //yield return new WaitForSeconds(Globals.SCREEN_SAVER_AWAIT + 0.2f);
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("level1");
    }


    private void Update()
    {        
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            Globals.MainPlayerData = new PlayerData();
            SaveLoadManager.Save();

            SceneManager.LoadScene("MainMenu");
        }*/

      
        if (YandexGame.SDKEnabled && !Globals.IsInitiated)
        {
            Globals.IsInitiated = true;

            SaveLoadManager.Load();

            print("SDK enabled: " + YandexGame.SDKEnabled);
            Globals.CurrentLanguage = YandexGame.EnvironmentData.language;
            print("language set to: " + Globals.CurrentLanguage);

            Globals.IsMobile = YandexGame.EnvironmentData.isMobile;
            print("platform mobile: " + Globals.IsMobile);

            if (Globals.MainPlayerData.S == 1)
            {
                Globals.IsSoundOn = true;
                AudioListener.volume = 1;
            }
            else
            {
                Globals.IsSoundOn = false;
                AudioListener.volume = 0;
            }

            if (Globals.MainPlayerData.Mus == 1)
            {
                Globals.IsMusicOn = true;
            }
            else
            {
                Globals.IsMusicOn = false;
            }

            print("sound is: " + Globals.IsSoundOn);

            if (Globals.TimeWhenStartedPlaying == DateTime.MinValue)
            {
                Globals.TimeWhenStartedPlaying = DateTime.Now;
                Globals.TimeWhenLastInterstitialWas = DateTime.Now;
                Globals.TimeWhenLastRewardedWas = DateTime.Now.Subtract(new TimeSpan(1,0,0));
            }

            Localize();
            playWhenInitialized();
        }
    }


    private void Localize()
    {
        Globals.Language = Localization.GetInstanse(Globals.CurrentLanguage).GetCurrentTranslation();        
    }
}
