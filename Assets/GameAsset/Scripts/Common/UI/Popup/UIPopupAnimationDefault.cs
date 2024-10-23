using System;
using System.Collections;
using UnityEngine;

public class UIPopupAnimationDefault : UIPopupAnimationBase
{
    private Transform _panel;
    private CanvasGroup _canvasGroup;

    [SerializeField] private float _duration = 2f;

    internal override void Initialize()
    {
        _canvasGroup = transform.Find("Background")?.GetComponent<CanvasGroup>();
        _panel = transform.Find("Panel")?.GetComponent<RectTransform>();
        base.Initialize();
    }

    protected override void PerformShowImmediately()
    {
        _panel.localScale = Vector3.one;
        _canvasGroup.alpha = 0.75f;
    }

    protected override void PerformHideImmediately()
    {
        _panel.localScale = Vector3.zero;
        _canvasGroup.alpha = 0f;
    }

    protected override void PerformShowAnimation()
    {
        _canvasGroup.alpha += 1f;
        Vector3 initialScale = new Vector3(0, 0, 0);
        Vector3 targetScale = new Vector3(1, 1, 1);
        StartCoroutine(AnimatePopup(initialScale, targetScale));
    }

    private IEnumerator AnimatePopup(Vector3 initialScale, Vector3 targetScale)
    {
        float elapsedTime = 0;
        while (elapsedTime < _duration)
        {
            float t = elapsedTime / _duration;
            float a = Mathf.Round(t);

            // Calculate the scale            
            float currentScaleValue = 4 * Mathf.Pow(t, 3) * (1 - a) + (1 - 4 * Mathf.Pow(1 - t, 3)) * a;
            //float currentScaleValue = Mathf.Pow(t, 2) + t * (1 - Mathf.Pow(t, 4));
            Vector3 newScale = Vector3.Lerp(initialScale, targetScale, currentScaleValue);

            // Apply the new scale
            _panel.localScale = newScale;

            // Increment time
            elapsedTime += Time.deltaTime;

            // Yield until the next frame
            yield return null;
        }

        _panel.localScale = targetScale;
    }

    protected override void PerformHideAnimation()
    {
        _canvasGroup.alpha -= 1f;
        Vector3 targetScale = new Vector3(0, 0, 0);
        Vector3 initialScale = new Vector3(1, 1, 1);
        StartCoroutine(AnimatePopup(initialScale, targetScale));
    }
}