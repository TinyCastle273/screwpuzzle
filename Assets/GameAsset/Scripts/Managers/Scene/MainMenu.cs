using System;
using System.Collections;
using TMPro;
using UnityEngine;

public partial class MainMenu
{
    [SerializeField]
    private TextMeshProUGUI _coinsText;

    public TextMeshProUGUI CoinsText => _coinsText;
    
    public void OnSettingsButton()
    {
        var popup = GM.Instance.Popups.GetPopup<UIPopupBehaviourSettings>(out var behaviour); ;

        popup.Show();
    }

    public void OnCreditsButton()
    {
        var popup = GM.Instance.Popups.GetPopup<UIPopupBehaviourCredits>(out var behaviour); ;

        popup.Show();
    }

    public void OnPlayButton()
    {
        Log.Info($"Start game request is called");
        RequestGameStart();
    }

    public void ShowMainMenu()
    {
        Log.Info($"Main menu {name} should show");

        gameObject.SetActive(true);
        AudioManager.Instance.PlayMusic(LibraryMusics.Menu);
    }

    private void HideMainMenu()
    {
        Log.Info($"Main menu {name} should hide");

        gameObject.SetActive(false);
    }

    private void RequestGameStart()
    {
        // Loading screen

        GM.Instance.MainGame.StartGame();

        HideMainMenu();
    }
}