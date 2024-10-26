using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LoadState
{
    IDLE = 0,
    TRANSIT_IN,
    LOADING,
    TRANSIT_OUT
}

public class LoadingScreen : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _initialLoadBlocker;
    [SerializeField] private GameObject _inputBlocker;
    [SerializeField] private Slider _slider;

    private LoadState _loadState = LoadState.IDLE;
    private bool _hasAppend;
    private Action _beforeOutAction;
    private Action _completeAction;

    private float _progress = 0f;

    public bool Active => gameObject.activeSelf;

    private void Awake()
    {
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0f, 0f);
        UpdateProgressBar(out _);


    }

    private void LateUpdate()
    {
        if (!Active) return;

        // Always update if Active
        UpdateProgressBar(out var isDone);

        if (_loadState == LoadState.LOADING)
        {
            // Wait for done and trigger here
            if (isDone)
            {
                OnLoadingDone();
            }
        }
    }

    public void RequestLoad(Action beforeOutAction = null,
                            Action completeAction = null,
                            bool loadAppend = false)
    {
        if (Active && loadAppend)
        {
            if (_loadState >= LoadState.TRANSIT_OUT)
            {
                LogObj.Default.Warn("LoadingScreen", "Load append is requested too late, and will be skipped.");
                return;
            }
            else
            {
                _hasAppend = true;
                LogObj.Default.Info("LoadingScreen", "Appended a load.");
            }
        }

        _beforeOutAction = beforeOutAction;
        _completeAction = completeAction;

        gameObject.SetActive(true);
        _inputBlocker.SetActive(true);

        if (_loadState < LoadState.TRANSIT_IN)
        {
            TransitIn();
        }
        else
        {
            ProperLoading();
        }
    }

    private void TransitIn()
    {
        _loadState = LoadState.TRANSIT_IN;

        _slider.value = _progress = 0f;

        ConcludeTransitIn();
    }

    private void ConcludeTransitIn()
    {
        ProperLoading();
    }

    private void ProperLoading()
    {
        _loadState = LoadState.LOADING;

        // Do nothing
    }

    private void UpdateProgressBar(out bool isDone)
    {
        _progress += 0.01f;
        _slider.value += _progress;
        isDone = _progress >= 1;
    }

    private void OnLoadingDone()
    {
        _hasAppend = false;

        // Perform Action
        _beforeOutAction?.Invoke();
        _initialLoadBlocker.SetActive(false);

        if (!_hasAppend)
        {
            TransitOut();
        }
    }

    private void TransitOut()
    {
        _loadState = LoadState.TRANSIT_OUT;

        ConcludeTransitOut();
    }

    private void ConcludeTransitOut()
    {
        // Perform callback
        _completeAction?.Invoke();
        ConcludeLoading();
    }

    private void ConcludeLoading()
    {
        _beforeOutAction = null;
        _completeAction = null;

        _loadState = LoadState.IDLE;
        _hasAppend = false;

        gameObject.SetActive(false);
        _inputBlocker.SetActive(false);
    }
}
