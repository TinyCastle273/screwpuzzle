using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class MainGameHUD
{
    [Header("Level data")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    [Header("Buttons")]
    [SerializeField] private List<PowerUpButton> _powerUpButtons;

    private int _coins;

    public TextMeshProUGUI LevelText => _levelText;
    public TextMeshProUGUI CoinsText => _coinsText;
    public PowerUpButton DrillPowerUpButton => _powerUpButtons[0];
    public PowerUpButton HammerPowerUpButton => _powerUpButtons[1];
    public PowerUpButton UndoPowerUpButton => _powerUpButtons[2];
    public PowerUpButton RestartPowerUpButton => _powerUpButtons[3];

    public void CheckUnlockPowerUp(int currentLevel)
    {
        _powerUpButtons.ForEach(powerUpButton =>
        {
            powerUpButton.checkUnlockable(currentLevel);
        });
    }

    private void UpdateCoins()
    {
        _coinsText.text = "" + _coins;
        GM.Instance.MainMenu.CoinsText.text = _coinsText.text;
    }

    public void AddCoins(int coin)
    {
        _coins += coin;
        GM.Instance.Player.AddResource(GlobalConstants.COINS_RESOURCE, coin);
        UpdateCoins();
    }

    public void SubtractCoins(int coin)
    {
        _coins -= coin;
        GM.Instance.Player.UseResource(GlobalConstants.COINS_RESOURCE, coin);
        UpdateCoins();
    }

    public int GetTotalCoin()
    {
        return _coins;
    }

    public void RequestPowerUp(PowerUpButton powerUp)
    {
        GM.Instance.MainGame.GameState = GameState.PAUSED;

        var popup = GM.Instance.Popups.GetPopup<UIPopupBehaviourPowerUp>(out var behaviour);

        behaviour.SetupPopup(powerUp);

        popup.Show();
    }
}