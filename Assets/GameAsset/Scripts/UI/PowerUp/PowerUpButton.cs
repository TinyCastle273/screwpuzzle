using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUp
{
    DRILL,
    HAMMER,
    UNDO,
    RESTART
}

public class PowerUpButton : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] private PowerUp _powerType;
    [SerializeField] private int _powerPrice;
    [SerializeField] private Sprite _powerImg;
    [SerializeField] private Sprite _powerName;
    [SerializeField] private bool _unlocked = false;
    [SerializeField] private int _levelToUnlock;

    private Image _image;

    public int LevelToUnlock => _levelToUnlock;
    public int PowerPrice => _powerPrice;
    public Sprite PowerImg => _powerImg;
    public Sprite PowerName => _powerName;

    public PowerUp GetPowerType()
    {
        return _powerType;
    }

    public void OnPowerUpButton()
    {
        if (!_unlocked)
        {
            LogObj.Default.Info(name, $"Power up locked");
            return;
        }

        GM.Instance.MainHUD.RequestPowerUp(this);
    }

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void checkUnlockable(int currentLevel)
    {
        if (_unlocked) return;

        if (currentLevel >= _levelToUnlock)
            UnlockPowerUp();
    }

    private void UnlockPowerUp()
    {
        LogObj.Default.Info(name, $"Unlocked {_powerType}");

        _unlocked = true;
        _image.sprite = _powerImg;

        if (_powerType == PowerUp.RESTART) return;

        GM.Instance.MainHUD.RequestPowerUp(this);
    }

    public void PerformSuperPower()
    {
        if (!_unlocked) return;

        GM.Instance.MainHUD.SubtractCoins(PowerPrice);

        // Perform super power here
        switch (_powerType)
        {
            case PowerUp.RESTART:
                AudioManager.Instance.PlaySFX(PowerUpSounds.Replay);
                GM.Instance.MainGame.RestartCurrentLevel();
                break;

            case PowerUp.UNDO:
                AudioManager.Instance.PlaySFX(PowerUpSounds.Return);
                break;

            case PowerUp.DRILL:
                ScrewManager.Instance._currentState = PlayState.DRILL;
                break;

            case PowerUp.HAMMER:
                ScrewManager.Instance._currentState = PlayState.HAMMER;
                break;
        }
    }
}