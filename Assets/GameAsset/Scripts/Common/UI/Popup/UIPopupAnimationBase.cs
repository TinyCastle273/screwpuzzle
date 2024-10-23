using System;
using System.Collections;
using UnityEngine;

public enum UIAnimState
{
    NONE,
    SHOW_START,
    SHOW_END,
    HIDE_START,
    HIDE_END
}

public abstract class UIPopupAnimationBase : MonoBehaviour
{

    private UIAnimState _state;

    public UIAnimState State => _state;

    public Action<UIAnimState> OnStateChangeAction { get; set; }

    public UIPopup Popup { get; internal set; }

    protected abstract void PerformShowImmediately();
    protected abstract void PerformHideImmediately();
    protected abstract void PerformShowAnimation();
    protected abstract void PerformHideAnimation();

    internal virtual void Initialize()
    {
        SetState(UIAnimState.NONE);
        PlayHideImmediately();
    }

    internal void PlayShow()
    {
        //
        gameObject.SetActive(true);

        SetState(UIAnimState.SHOW_START);

        PerformShowAnimation();

        SetState(UIAnimState.SHOW_END);
    }

    internal void PlayHide()
    {
        //
        SetState(UIAnimState.HIDE_START);
        PerformHideAnimation();
        SetState(UIAnimState.HIDE_END);
        
        gameObject.SetActive(false);
        SetState(UIAnimState.NONE);
    }

    internal void PlayShowImmediately()
    {
        gameObject.SetActive(true);
        PerformShowImmediately();
        SetState(UIAnimState.SHOW_START);
        SetState(UIAnimState.SHOW_END);
    }

    internal void PlayHideImmediately()
    {
        PerformHideImmediately();
        SetState(UIAnimState.HIDE_START);
        SetState(UIAnimState.HIDE_END);
        gameObject.SetActive(false);
        SetState(UIAnimState.NONE);
    }

    private void SetState(UIAnimState state)
    {
        _state = state;
        LogObj.Default.Info(Popup.ExplicitName, $"Animation: state is set to {_state}");
        OnStateChangeAction?.Invoke(state);
    }


}