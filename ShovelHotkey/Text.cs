using Il2CppInterop.Runtime;
using MelonLoader;
using UnityEngine;
using Il2CppTMPro;

#nullable disable
namespace ShovelHotkey;

[RegisterTypeInIl2Cpp]
class Text : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;

    public Text(IntPtr ptr) : base(ptr)
    {
    }

    private void Awake()
    {
        MelonLogger.Msg("Text.Awake() called");
        
        _rectTransform = gameObject.GetComponent<RectTransform>();
        if (_rectTransform == null)
        {
            _rectTransform = gameObject.AddComponent<RectTransform>();
        }

        // Configure RectTransform
        _rectTransform.sizeDelta = new Vector2(200, 100);
        _rectTransform.anchorMin = new Vector2(0, 0);
        _rectTransform.anchorMax = new Vector2(0, 0);
        _rectTransform.pivot = new Vector2(0.5f, 0.5f);
        _rectTransform.anchoredPosition = new Vector2(1920, 1080); // Center of 4K canvas initially

        // Add a Canvas component to control rendering order
        _canvas = gameObject.AddComponent<Canvas>();
        _canvas.overrideSorting = true;
        _canvas.sortingOrder = 9999; // Render on top of everything
        
        // Add CanvasGroup for visibility control
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        // Create text component
        _text = gameObject.AddComponent<TextMeshProUGUI>();
        _text.text = "TESTING";
        _text.fontSize = 72;
        _text.fontStyle = FontStyles.Bold;
        _text.color = new Color(1f, 0f, 0f, 1f); // Bright red
        _text.alignment = TextAlignmentOptions.Center;
        
        // Ensure it's not being masked
        _text.raycastTarget = false;

        MelonLogger.Msg($"Text component initialized! Position: {_rectTransform.anchoredPosition}");
        MelonLogger.Msg($"Text content: '{_text.text}', Color: {_text.color}, FontSize: {_text.fontSize}");
        MelonLogger.Msg($"GameObject active: {gameObject.activeSelf}, enabled: {enabled}");
    }

    private void OnEnable()
    {
        MelonLogger.Msg("Text.OnEnable() called");
    }

    private void Start()
    {
        MelonLogger.Msg("Text.Start() called");
        MelonLogger.Msg($"Text is now at position: {_rectTransform.anchoredPosition}");
    }

    public void SetText(string text)
    {
        if (_text != null)
        {
            _text.text = text;
            MelonLogger.Msg($"Text set to: '{text}'");
        }
    }

    public void SetPosition(Vector2 position)
    {
        if (_rectTransform != null)
        {
            _rectTransform.anchoredPosition = position;
            MelonLogger.Msg($"Position set to: {position}");
        }
    }

    public void SetFontSize(float size)
    {
        if (_text != null)
        {
            _text.fontSize = size;
        }
    }

    public void SetColor(Color color)
    {
        if (_text != null)
        {
            _text.color = color;
        }
    }
}