using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-2)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
        
    [Header("Others")]
    [SerializeField] private AssetManager assetManager;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform cameraBody;    
    [SerializeField] private UIManager mainUI;
    [SerializeField] private OptionsMenu options;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private InputControl inputControl;
    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private FieldManager fieldManager;

    public Camera GetCamera() => _camera;
    public Transform GetCameraBody() => cameraBody;
    public UIManager GetUI() => mainUI;
    public AssetManager Assets => assetManager;
    public FieldManager GetFieldManager => fieldManager;
    public LevelManager LevelManager => levelManager;
    public Dictionary<CellTypes, int> CurrentProgress { get; private set; }

    //GAME START    
    public bool IsGameStarted { get; private set; }
    private float cameraShakeCooldown;
   

    //TODEL
    [SerializeField] private TextMeshProUGUI testText;
    public TextMeshProUGUI GetTestText() => testText;
    [SerializeField] private Button testSticky;
    private bool isSticky;


    // Start is called before the first frame update
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

        //TODEL
        Globals.MainPlayerData = new PlayerData();
        Globals.MainPlayerData.Lvl = 0;
        Globals.IsInitiated = true;
        Globals.IsMobile = true;
        Globals.IsSoundOn = true;
        Globals.IsMusicOn = true;
        Globals.Language = Localization.GetInstanse(Globals.CurrentLanguage).GetCurrentTranslation();

        if (Globals.IsMobile)
        {
            QualitySettings.antiAliasing = 2;
        }
        else
        {
            QualitySettings.antiAliasing = 4;
        }

        

        if (Globals.MainPlayerData != null) YandexGame.StickyAdActivity(true);
        isSticky = true;
        Globals.MainPlayerData.AdvOff = false;

        levelManager.SetData();
        inputControl.SetData(_camera);
        cameraControl.SetData(_camera);
        assetManager.SetData();
        fieldManager.SetData();

        CurrentProgress = new Dictionary<CellTypes, int>();
        foreach (CellTypes item in Globals.VictoryCondition.Keys)
        {
            CurrentProgress.Add(item, 0);
        }

        IsGameStarted = true;

        
    }

    private void Start()
    {
        AmbientMusic.Instance.PlayAmbient(AmbientMelodies.forest);

        testSticky.onClick.AddListener(() =>
        {
            if (isSticky)
            {
                isSticky = false;
                YandexGame.StickyAdActivity(false);
                Globals.MainPlayerData.AdvOff = true;
                cameraControl.UpdateCamera();
            }
            else
            {
                isSticky = true;
                YandexGame.StickyAdActivity(true);
                Globals.MainPlayerData.AdvOff = false;
                cameraControl.UpdateCamera();
            }
        });

        ScreenSaver.Instance.ShowScreen();
    }

    public bool CountCell(CellControl cell)
    {
        if (CurrentProgress.ContainsKey(cell.CellType))
        {
            CurrentProgress[cell.CellType]++;
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Globals.MainPlayerData = new PlayerData();
            SaveLoadManager.Save();
            SceneManager.LoadScene("Gameplay");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            YandexGame.StickyAdActivity(false);
        }
    }


    public void ShakeScreen(float _time, float strength, int vibra)
    {
        if (cameraShakeCooldown > 0) return;

        _time = _time < 0.1f ? 0.1f : _time;
        strength = strength < 1f ? 1f : strength;
        vibra = vibra < 10 ? 10 : vibra;


        _camera.transform.DOShakePosition(_time, strength, vibra);
        cameraShakeCooldown = _time + 0.1f;
    }
  
    
}
