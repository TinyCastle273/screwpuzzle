using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public enum Screen
{
    NONE = 0,
    MENU,
    GAMEPLAY
}

public partial class GM
{
    private Screen _currentScreen = Screen.NONE;

    public void RequestGoToMenu()
    {
        RequestGoTo(Screen.MENU);
    }

    public void RequestGoTo(Screen screen, bool append = false)
    {
        Log.Info($"Request go to {screen}");

        var from = _currentScreen;
        var to = screen;

        PrepareDeactivateScreen(from);
        PrepareActivateScreen(to);

        _loadingScreen.RequestLoad(beforeOutAction: ()  =>
        {
            Log.Info($"Perform activate screen");
            DeactivateScreen(from);
            ActivateScreen(to);

        }, 
        loadAppend: append);

        _currentScreen = to;
    }

    // Prepare save game data before quit
    private void PrepareDeactivateScreen(Screen screen)
    {
        Log.Info($"Prepare deactivate {screen}");

        switch (screen)
        {
            case Screen.MENU:
                MainMenu.PrepareDeativate();
                break;

            case Screen.GAMEPLAY:
                MainGame.PrepareDeactivate();
                break;

            default:
                break;
        }
    }

    // Prepare game data before start
    private void PrepareActivateScreen(Screen screen)
    {
        Log.Info($"Prepare activate {screen}");

        switch (screen)
        {
            case Screen.MENU:
                MainMenu.PrepareActivate();
                break;

            case Screen.GAMEPLAY:
                MainGame.PrepareActivate();
                break;

            default: 
                break;
        }
    }

    private void DeactivateScreen(Screen screen)
    {
        Log.Info($"Deactivate {screen}");

        switch (screen)
        {
            case Screen.MENU:
                MainMenu.Deactivate();
                break;

            case Screen.GAMEPLAY:
                MainGame.Deactivate();
                break;
        }
    }

    private void ActivateScreen(Screen screen)
    {
        Log.Info($"Activate {screen}");

        switch (screen)
        {
            case Screen.MENU:
                MainMenu.Activate();
                break;

            case Screen.GAMEPLAY:
                MainGame.Activate();
                break;

            default:
                break;
        }
    }
}