using System;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image _icon;

    [Header("Params")]
    [SerializeField] private LibrarySounds _tapSounds = LibrarySounds.Button;
    [SerializeField] private EventWrapper _buttonClickedEvent;

    private Button _unityButton;

    public Sprite Icon
    {
        get => _icon.sprite;
        set
        {
            if (_icon != null)
            {
                _icon.sprite = value;
            }
        }
    }

    public bool Interactable
    {
        get => _unityButton.interactable;
        set => _unityButton.interactable = value;
    }

    public Button UnityButton => _unityButton;
    public Image IconImage => _icon;

    public EventWrapper Event => _buttonClickedEvent;

    private void Awake()
    {
        _unityButton = GetComponent<Button>();
        _unityButton.onClick.AddListener(OnUnityButtonClick);
    }

    public void OnUnityButtonClick()
    {
        GM.Instance.AudioManager.PlaySFX(_tapSounds);
        // other things
        _buttonClickedEvent?.Invoke();
    }
}