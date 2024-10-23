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
    [SerializeField] private AudioManager _audioManager;

    private List<IIntializable> _primaryManagers;
    private List<IIntializable> _secondaryManagers;

    // Manager accessors
    public LoadingScreen Loading => _loadingScreen;
    public MainGameManager MainGame => _mainGameManager;
    public AudioManager AudioManager => _audioManager;
    public PopupManager Popups => _popupManager;
    public MainMenu MainMenu => _mainMenu;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = -1;
        Logger.ShouldLog = _shouldLog;
    }

    private void Start()
    {
        _primaryManagers = new List<IIntializable>()
        {
            MainGame,
            AudioManager,
            Popups,
            MainMenu,
        };

        _secondaryManagers = new List<IIntializable>()
        {
        };

        Initialize();

        // temporary
        MainMenu.ShowMainMenu();

    }

    private void Update()
    {
        var dt = Time.deltaTime;

        UpdateInitialization(dt);
    }

}
