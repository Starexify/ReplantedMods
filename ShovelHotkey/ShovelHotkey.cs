using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Il2CppReloaded.Gameplay;
using MelonLoader;
using UnityEngine;
using Input = UnityEngine.Input;
using Object = UnityEngine.Object;

[assembly: MelonInfo(typeof(ShovelHotkey.ShovelHotkey), "ShovelHotKey", "1.0.0", "ImVeryBad")]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

#nullable disable
namespace ShovelHotkey;

public class ShovelHotkey : MelonMod
{
    private const KeyCode DefaultKeybind = KeyCode.Q;
    private const string CategoryName = "PVZShovelHotkey";
    private const string EntryName = "shovelKey";

    private static Board _board;
    private static KeyCode _shovelKeybind;

    private GameObject _keybindDisplayObject;
    private Text _keybindDisplay;
    private Canvas _targetCanvas;
    private bool _positionLogged = false;

    public override void OnInitializeMelon()
    {
        ClassInjector.RegisterTypeInIl2Cpp<Text>();

        var category = MelonPreferences.GetCategory(CategoryName) ?? MelonPreferences.CreateCategory(CategoryName);
        var entry = category.GetEntry<KeyCode>(EntryName) ??
                    category.CreateEntry(EntryName, DefaultKeybind, "Shovel Keybind");

        _shovelKeybind = entry.Value;
        category.SaveToFile();
    }

    public override void OnUpdate()
    {
        UpdateKeybindDisplay();

        if (!CanPickupShovel()) return;

        if (Input.GetKeyDown(_shovelKeybind)) _board.PickUpTool(ReloadedObjectType.Shovel);
    }

    private void UpdateKeybindDisplay()
    {
        if (!ShouldShowKeybind())
        {
            if (_keybindDisplayObject != null)
                _keybindDisplayObject.SetActive(false);
            return;
        }

        if (_keybindDisplayObject == null || _keybindDisplay == null)
        {
            CreateKeybindDisplay();
        }
        else
        {
            _keybindDisplayObject.SetActive(true);
            _keybindDisplay.SetText(_shovelKeybind.ToString());
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        if (_board == null || _keybindDisplay == null || _targetCanvas == null) return;

        var shovelRect = _board.GetShovelButtonRect();
        var canvasRect = _targetCanvas.GetComponent<RectTransform>();

        if (!_positionLogged)
        {
            LoggerInstance.Msg($"Shovel Rect: x={shovelRect.x}, y={shovelRect.y}, w={shovelRect.width}, h={shovelRect.height}");
            LoggerInstance.Msg($"Screen: {Screen.width}x{Screen.height}");
            LoggerInstance.Msg($"Canvas Rect: {canvasRect.rect.width}x{canvasRect.rect.height}");
            _positionLogged = true;
        }

        var scaleX = canvasRect.rect.width / Screen.width;
        var scaleY = canvasRect.rect.height / Screen.height;

        // X position: center of shovel button
        var canvasX = (shovelRect.x + shovelRect.width / 2f) * scaleX;
    
        // Y position: shovel.y=0 is TOP of screen
        // We want text below the button (y + height + offset from top)
        var pixelsFromTop = shovelRect.y + shovelRect.height + 10f;
        var canvasY = canvasRect.rect.height - (pixelsFromTop * scaleY);

        _keybindDisplay.SetPosition(new Vector2(canvasX, canvasY));
    }

    private void CreateKeybindDisplay()
    {
        var canvases = Object.FindObjectsOfType<Canvas>();
        LoggerInstance.Msg($"Found {canvases.Length} canvases:");

        Canvas overlayCanvas = null;
        int highestSortOrder = int.MinValue;

        foreach (var canvas in canvases)
        {
            LoggerInstance.Msg($"  Canvas: {canvas.name}, Layer: {canvas.gameObject.layer}, " +
                               $"SortOrder: {canvas.sortingOrder}, RenderMode: {canvas.renderMode}");

            if (canvas.gameObject.activeInHierarchy)
            {
                // Prefer ScreenSpaceOverlay canvases with highest sort order
                if (canvas.renderMode == RenderMode.ScreenSpaceOverlay && canvas.sortingOrder > highestSortOrder)
                {
                    overlayCanvas = canvas;
                    highestSortOrder = canvas.sortingOrder;
                }

                // Fallback to any canvas with high sort order
                if (_targetCanvas == null || canvas.sortingOrder > _targetCanvas.sortingOrder)
                {
                    _targetCanvas = canvas;
                }
            }
        }

        // Prefer overlay canvas if found
        if (overlayCanvas != null)
        {
            _targetCanvas = overlayCanvas;
        }

        if (_targetCanvas == null)
        {
            LoggerInstance.Warning("No active Canvas found!");
            return;
        }

        LoggerInstance.Msg(
            $"Using canvas: {_targetCanvas.name} (SortOrder: {_targetCanvas.sortingOrder}, RenderMode: {_targetCanvas.renderMode})");

        _keybindDisplayObject = new GameObject("ShovelKeybindDisplay")
        {
            layer = _targetCanvas.gameObject.layer
        };
        _keybindDisplayObject.transform.SetParent(_targetCanvas.transform, false);

        _keybindDisplay = _keybindDisplayObject.AddComponent<Text>();

        _positionLogged = false;

        LoggerInstance.Msg("KeybindDisplay created successfully!");
    }

    private void CleanupKeybindDisplay()
    {
        if (_keybindDisplayObject != null)
        {
            Object.Destroy(_keybindDisplayObject);
            _keybindDisplayObject = null;
            _keybindDisplay = null;
        }

        _positionLogged = false;
    }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        CleanupKeybindDisplay();
        _targetCanvas = null;
    }

    private static bool ShouldShowKeybind()
    {
        return _board != null
               && _board.mApp.GameMode != GameMode.ChallengeZenGarden
               && !_board.mPaused;
    }

    private static bool CanPickupShovel()
    {
        return _board != null
               && _board.mApp.GameMode != GameMode.ChallengeZenGarden
               && _board.mCursorObject?.mCursorType != CursorType.Shovel
               && !_board.mPaused;
    }

    [HarmonyPatch(typeof(Board), "StartLevel")]
    private static class BoardInitPatch
    {
        public static void Prefix(Board __instance) => _board = __instance;
    }

    [HarmonyPatch(typeof(Board), "DisposeBoard")]
    private static class BoardDisposePatch
    {
        public static void Prefix() => _board = null;
    }
}