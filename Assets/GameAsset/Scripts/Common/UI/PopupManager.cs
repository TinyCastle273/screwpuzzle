using System;
using GUtils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class PopupManager : MonoManagerBase
{
    public event Action HasPopupEvent;

    [SerializeField] private Transform _popupHost;

    private Dictionary<Type, UIPopup> _popupsByType;
    private Dictionary<string, UIPopup> _popupsByName;

    private int _activePopupCount;

    public bool HasPopups => _activePopupCount > 0;

    public override ReinitializationPolicy ReInitPolicy => ReinitializationPolicy.NOT_ALLOWED;

    protected override void EndInitializationBehavior()
    {
        // Do nothing
        _activePopupCount = 0;
    }

    protected override void StartInitializationBehavior()
    {
        _popupsByType = new Dictionary<Type, UIPopup>();
        _popupsByName = new Dictionary<string, UIPopup>();

        var popupList = _popupHost.GetDirectOrderedChildComponents<UIPopup>();

        var z = 1;
        foreach (var popup in popupList)
        {
            popup.SetManager(this, Log);
            popup.Initialize();
            popup.PopupZ = z;

            var type = popup.Behaviour.GetType();

            if (type != typeof(UIPopupBehaviour))
            {
                _popupsByType.Add(type, popup);
            }

            _popupsByName.Add(popup.ExplicitName, popup);

            ++z;
        }

        EndInitialize(true);
    }

    internal void ShowPopup(UIPopup popup, bool immediately, string chainTo)
    {
        ++_activePopupCount;
        popup.PopupZ = _activePopupCount;
        popup.transform.SetAsLastSibling();

        Log.Info($"Popup {popup.ExplicitName} should show.");
        popup.ManagerShow(immediately);

        HasPopupEvent?.Invoke();
    }

    internal void HidePopup(UIPopup popup, bool immediately = false)
    {
        Log.Info($"Popup {popup.ExplicitName} should hide.");
        popup.ManagerHide(immediately);
    }
}