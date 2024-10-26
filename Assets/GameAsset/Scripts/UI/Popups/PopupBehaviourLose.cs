public class PopupBehaviourLose : UIPopupBehaviour
{
    public void OnRetryButton()
    {
        GM.Instance.MainGame.RestartGame();

        Popup.Hide();
    }

    public void OnHomeButton()
    {
        GM.Instance.RequestGoToMenu();

        Popup.Hide();
    }
}