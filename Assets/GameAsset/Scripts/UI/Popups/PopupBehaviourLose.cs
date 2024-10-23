public class PopupBehaviourLose : UIPopupBehaviour
{
    public void OnRetryButton()
    {
        GM.Instance.MainGame.RestartGame();

        Popup.Hide();
    }

    public void OnHomeButton()
    {
        GM.Instance.MainGame.ReturnMainMenu();

        Popup.Hide();
    }
}