using System;
using System.Collections.Generic;
using UnityEngine;

public enum WinMode
{
    NORMAL = 0,
}

public class PopupBehaviourWin : UIPopupBehaviour
{
    private WinMode _winMode;
    private bool _isWin;

    public void SetWinMode(WinMode mode, bool isWin)
    {
        _winMode = mode;
        _isWin = isWin;
    }

    public void OnContinueButton()
    {
        //
        if (_isWin)
        {
            GM.Instance.MainGame.startNextLevel();
        }
        else
        {
            GM.Instance.MainGame.startNextLevel();
        }

        Popup.Hide();
    }

    public void OnReturnButton()
    {
        //
        Popup.Hide();
        GM.Instance.MainGame.GameState = GameState.PLAYING;
        GM.Instance.MainGame.RequestEndGame(false);
    }
}