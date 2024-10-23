using System;
using System.Collections.Generic;
using UnityEngine;

public partial class PopupManager
{
    public UIPopup GetPopup<T>(out T behaviour) where T : UIPopupBehaviour
    {
        if (!_popupsByType.ContainsKey(typeof(T)))
        {
            behaviour = null;
            return null;
        }

        var popup = _popupsByType[typeof(T)];
        behaviour = popup.Behaviour as T;
        return popup;
    }
}