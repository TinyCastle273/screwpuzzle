using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GM : MonoManagerBase
{
    public static GM Instance { get; private set; }

    [Header("Params")]
    [SerializeField] private bool _shouldLog;
    [SerializeField] private bool _isCheat;

    [Header("Components")]
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private PopupManager _popupManager;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private MainGameManager _mainGameManager;
    [SerializeField] private MainGameHUD _mainHUD;
    [SerializeField] private AudioManager _audioManager;
    private PlayerManager _playerManager;

    private List<IIntializable> _primaryManagers;
    private List<IIntializable> _secondaryManagers;

    // Manager accessors
    public PlayerManager Player => _playerManager;
    public LoadingScreen Loading => _loadingScreen;
    public MainGameManager MainGame => _mainGameManager;
    public AudioManager AudioManager => _audioManager;
    public PopupManager Popups => _popupManager;
    public MainMenu MainMenu => _mainMenu;
    public MainGameHUD MainHUD => _mainHUD;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = -1;
        Logger.ShouldLog = _shouldLog;

        _playerManager = new PlayerManager();
    }

    private void Start()
    {
        _primaryManagers = new List<IIntializable>()
        {
            _playerManager,
        };

        _secondaryManagers = new List<IIntializable>()
        {    
            MainGame,
            AudioManager,
            Popups,
            MainMenu,
            _mainHUD
        };

        Initialize();

        // temporary

    }

    private void Update()
    {
        var dt = Time.deltaTime;

        UpdateInitialization(dt);
    }

}
