using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    NONE = 0,
    PLAYING,
    PAUSED,
    COMPLETING,
    COMPLETED,
}

public partial class MainGameManager 
{
    [Header("Params")]
    [SerializeField] public Sprite selectedScrew;
    [SerializeField] private TextMeshProUGUI _levelPanel;
    [SerializeField] private TextMeshProUGUI _timePanel;

    [Header("Levels")]
    [SerializeField] 
    private LevelInfo levelInfo;

    [SerializeField]
    private Transform LevelGroup;

    public static MainGameManager Instance;

    private GameObject currentLevel;
    private int currentLevelIndex;
    private GameObject nextLevel;

    private string levelText;
    private string timeText;

    private float timer;

    private GameState _state = GameState.NONE;

    public TextMeshProUGUI LevelPanel => _levelPanel;
    public TextMeshProUGUI TimePanel => _timePanel;

    public GameState GameState
    {
        get => _state;
        set
        {
            _state = value;
        }
    }

    private void Awake()
    {
        Instance = this;
        currentLevel = levelInfo.GetLevel(0);
        nextLevel = levelInfo.GetLevel(1);
        currentLevelIndex = 1;
    }

    private void Update()
    {
        if (GameState == GameState.PLAYING)
        {
            UpdateTimer();
        }
    }

    public void startNextLevel()
    {
        CloseLevel();
        currentLevelIndex++;
        currentLevel = nextLevel;
        nextLevel = levelInfo.GetLevel(currentLevelIndex);

        StartGame();
    }

    public void RestartGame()
    {
        CloseLevel();
        currentLevel = levelInfo.GetLevel(0);
        nextLevel = levelInfo.GetLevel(1);
        currentLevelIndex = 1;

        StartGame();
    }

    public void RestartCurrentLevel()
    {
        GameState = GameState.COMPLETED;

        CloseLevel();

        StartGame();
    }

    public void StartGame()
    {
        GM.Instance.AudioManager.PlayMusic(LibraryMusics.InGame);
        LevelHolder.Instance.createLevel(currentLevel);
        SetLevelLabel(currentLevelIndex);
        GameState = GameState.PLAYING;

        ResetTimer();
    }

    public void SaveGameLevel()
    {
        Log.Info($"Saving player progress");
        Log.Warn($"Feature currently not implemented!!!");
    }

    private void CloseLevel()
    {
        Log.Info($"Close level request called, close {currentLevel}");

        LevelHolder.Instance.CloseFinishedLevel();
    }

    private void ResetTimer()
    {
        timer = 120f;
        timeText = string.Format("{0:00}:{1:00}", 2, 0);
        TimePanel.text = timeText;
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Log.Info($"Time's up, game over");
            RequestEndGame(false);
        }

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimePanel.text = timeText;
    }

    public void OnPauseButton()
    {
        if (GameState >= GameState.PAUSED) return;

        GameState = GameState.PAUSED;

        var popup = GM.Instance.Popups.GetPopup<UIPopupBehaviourPause>(out var behaviour);

        popup.Show();
    }

    public void SetLevelLabel(int level)
    {
        levelText = "Level " + level;
        LevelPanel.SetText(levelText);
    }

    public void RequestEndGame(bool isWin)
    {
        if (GameState >= GameState.COMPLETING) return;

        GameState = GameState.COMPLETING;

        GM.Instance.AudioManager.PlaySFX(isWin ? LibrarySounds.Win : LibrarySounds.Lose);

        OnEndGame(isWin);
    }

    private void OnEndGame(bool isWin)
    {
        GameState = GameState.COMPLETED;

        if (isWin)
        {
            var popup = GM.Instance.Popups.GetPopup<PopupBehaviourWin>(out var behaviour);

            behaviour.SetWinMode(WinMode.NORMAL, isWin);

            popup.Show();
        }
        else
        {
            var popup = GM.Instance.Popups.GetPopup<PopupBehaviourLose>(out var behaviour);

            popup.Show();
        }
    }

    public void ReturnMainMenu()
    {
        Log.Info($"Return to menu request called");

        GameState = GameState.NONE;

        // TODO:
        // Save level
        SaveGameLevel();
        CloseLevel();

        // temporary
        GM.Instance.MainMenu.ShowMainMenu();
    }
}