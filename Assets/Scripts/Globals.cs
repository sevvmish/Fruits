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

    public const int MAX_RESPAWN_POINTS = 25;

    public static bool IsDontShowIntro;
    public static bool IsGlobalTouch;
    public static bool IsShowArrowNotificatorOnPlay;

    public static bool IsMobile;
    public static bool IsDevelopmentBuild = false;
    public static bool IsOptions;
    public static bool IsAllRestarter;


    public const float BASE_SPEED = 7f;
    public const float JUMP_POWER = 40f;
    public const float GRAVITY_KOEFF = 2.75f;

    public const float SHADOW_Y_DISTANCE = 8f;

    //public static WaitForSeconds ZeroOne = new WaitForSeconds(0.1f);

    public const float MASS = 1.5f; //3
    public const float DRAG = 5f; //1
    public const float ANGULAR_DRAG = 5f;

    public static Vector2 DelayForBots = new Vector2(0.3f, 1.3f);

    public const float RAGDOLL_MASS = 0.25f;
    public const float RAGDOLL_DRAG = 1f;
    public const float RAGDOLL_ANGULAR_DRAG = 0.5f;

    public const float MOUSE_X_SENS = 26f;
    public const float MOUSE_Y_SENS = 13f;

    public const float SPEED_INC_IN_NONGROUND_PC = 0.25f;
    public const float SPEED_INC_IN_NONGROUND_MOBILE = 0.35f;

    public const float MAX_HIT_IMPULSE_MAGNITUDE = 60f;
    public const float MIN_HIT_IMPULSE_MAGNITUDE = 20f;

    public const int LAYER_OCCLUSION = 6;
    public const int LAYER_DANGER = 7;
    public const int LAYER_PLAYER = 9;

    public const float ZOOM_DELTA = 0.22f;
    public const float ZOOM_LIMIT = 6f;

    public const float SCREEN_SAVER_AWAIT = 1f;

    public const float PLAYERS_COLLIDE_FORCE = 7f;

    public const float OFFER_UPDATE = 4f;

    //abilities
    public const float ABILITY_DURATION = 10f;
    public const float ACCELERATION_DURATION = 4f;
    public const float ROCKETPACK_DURATION = 3f;

    public static readonly Vector3 BasePosition = new Vector3(0, 8.5f, -6.5f);
    public static readonly Vector3 BaseRotation = new Vector3(50, 0, 0);

    public static readonly LayerMask ignoreTriggerMask = LayerMask.GetMask(new string[] { "trigger", "player", "ragdoll" });

    public static Vector3 UIPlayerPosition = new Vector3(0.1f, -0.8f, 0);
    public static Vector3 UIPlayerRotation = new Vector3(0, 180, 0);

    
}