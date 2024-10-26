using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupBehaviourPowerUp : UIPopupBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _coins;
    [SerializeField] private Image _title;
    [SerializeField] private Image _powerUp;

    private PowerUpButton _powerUpButton;

    public void SetupPopup(PowerUpButton powerUpButton)
    {
        LogObj.Default.Info(name, $"Setting up PowerUp");

        _priceText.text = "" + powerUpButton.PowerPrice;
        _coins.text = GM.Instance.MainHUD.CoinsText.text;
        _powerUp.sprite = powerUpButton.PowerImg;

        if ( powerUpButton.GetPowerType() == PowerUp.RESTART)
        {
            _title.enabled = false;
        }
        else
            _title.sprite = powerUpButton.PowerName;

        _powerUpButton = powerUpButton;
    }

    public void OnBuyButton()
    {
        if (GM.Instance.MainHUD.GetTotalCoin() < _powerUpButton.PowerPrice)
        {
            LogObj.Default.Warn(name, $"Not enough coins");
            Popup.Hide();
            return;
        }

        _powerUpButton.PerformSuperPower();

        GM.Instance.MainGame.GameState = GameState.PLAYING;
        Popup.Hide();
    }

    public void OnClaimButton()
    {

        GM.Instance.MainGame.GameState = GameState.PLAYING;
        Popup.Hide();
    }

}
