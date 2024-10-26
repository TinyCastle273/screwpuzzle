using System;
using System.Collections;
using TMPro;
using UnityEngine;

public partial class MainMenu
{
    [SerializeField]
    private TextMeshProUGUI _coinsText;

    public TextMeshProUGUI CoinsText => _coinsText;

    public void SetCoinText(string text)
    {
        CoinsText.text = text;
    }

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
        Log.Info($"Start game");
        GM.Instance.RequestGoTo(Screen.GAMEPLAY);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        GM.Instance.AudioManager.PlayMusic(LibraryMusics.Menu);
    }

    public void PrepareDeativate()
    {
        // Do nothing
    }

    public void PrepareActivate()
    {
        // Do nothing
    }
}