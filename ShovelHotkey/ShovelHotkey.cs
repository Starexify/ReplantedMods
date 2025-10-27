using HarmonyLib;
using Il2CppReloaded.Gameplay;
using MelonLoader;
using UnityEngine;
using Input = UnityEngine.Input;

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
    private GUIStyle _labelStyle;

    public override void OnInitializeMelon()
    {
        // Create category in Preferences and load it
        var category = MelonPreferences.GetCategory(CategoryName) ?? MelonPreferences.CreateCategory(CategoryName);
        var entry = category.GetEntry<KeyCode>(EntryName) ??
                    category.CreateEntry(EntryName, DefaultKeybind, "Shovel Keybind");

        _shovelKeybind = entry.Value;
        category.SaveToFile();
    }

    public override void OnGUI()
    {
        if (!ShouldShowKeybind()) return;

        _labelStyle ??= new GUIStyle
        {
            fontSize = 24,
            fontStyle = FontStyle.BoldAndItalic,
            normal = { textColor = Color.black }
        };

        var shovelRect = _board.GetShovelButtonRect();

        var scaleX = Screen.width / 800f;
        var scaleY = Screen.height / 600f;
        
        var x = shovelRect.x * scaleX + ((shovelRect.width - 70) * scaleX) * 0.5f;
        var y = shovelRect.y * scaleY + ((shovelRect.height * 1.85f) * scaleY) * 0.5f;
        
        var position = new Rect(x, y, 60, 30);
        
        var keybindText = _shovelKeybind.ToString();
        GUI.Label(position, keybindText, _labelStyle);
    }

    public override void OnUpdate()
    {
        if (!CanPickupShovel()) return;

        if (Input.GetKeyDown(_shovelKeybind)) _board.PickUpTool(ReloadedObjectType.Shovel);
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