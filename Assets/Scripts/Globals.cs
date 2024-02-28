using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


public class Globals : MonoBehaviour
{
    public static PlayerData MainPlayerData;
    public static bool IsSoundOn;
    public static bool IsMusicOn;
    public static bool IsInitiated;
    public static string CurrentLanguage;
    public static Translation Language;

    public static DateTime TimeWhenStartedPlaying;
    public static DateTime TimeWhenLastInterstitialWas;
    public static DateTime TimeWhenLastRewardedWas;
    public const float REWARDED_COOLDOWN = 120;
    public const float INTERSTITIAL_COOLDOWN = 70;

    public static bool IsMobile;
    public static bool IsDevelopmentBuild = false;
    public static bool IsOptions;
    
    public const float SCREEN_SAVER_AWAIT = 1f;

    public static Vector2 FieldDimention;
    public static Dictionary<Vector2, CellControl> Cells;

    
}
