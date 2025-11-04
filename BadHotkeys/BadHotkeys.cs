using BadAPI.events;
using Il2CppReloaded.Gameplay;
using MelonLoader;
using UnityEngine;
using Input = UnityEngine.Input;

[assembly: MelonInfo(typeof(BadHotkeys.BadHotkeys), "BadHotkeys", "1.0.0", "ImVeryBad")]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

#nullable disable
namespace BadHotkeys;

public class BadHotkeys : MelonMod
{
    private const KeyCode DefaultKeybind = KeyCode.Q;

    private static KeyCode[] SeedPacketKeybinds =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0,
    };

    private const string CategoryName = "PVZShovelHotkey";
    private const string EntryName = "shovelKey";

    private static Board _board;
    private static KeyCode _shovelKeybind;

    public static SeedPacket LastSelectedPacket;

    public override void OnInitializeMelon()
    {
        var category = MelonPreferences.GetCategory(CategoryName) ?? MelonPreferences.CreateCategory(CategoryName);
        var entry = category.GetEntry<KeyCode>(EntryName) ??
                    category.CreateEntry(EntryName, DefaultKeybind, "Shovel Keybind");

        _shovelKeybind = entry.Value;
        category.SaveToFile();

        BoardEvents.OnUpdate += board =>
        {
            HandleShovelKey(board);
            HandleSeedKeys(board);
        };
    }

    public static void HandleShovelKey(Board board)
    {
        if (!CanPickupShovel(board)) return;
        
        if (board.mCursorObject?.mCursorType == CursorType.PlantFromBank)
        {
            if (LastSelectedPacket != null)
            {
                LastSelectedPacket.Deselected(0);
                LastSelectedPacket.Activate();
                LastSelectedPacket = null;
            }
        }

        if (Input.GetKeyDown(_shovelKeybind)) board.PickUpTool(ReloadedObjectType.Shovel);
    }

    public static void HandleSeedKeys(Board board)
    {
        if (board.mSeedBank == null) return;

        var seedBank = board.mSeedBank;
        for (var i = 0; i < seedBank.GetPacketCount(); i++)
        {
            var packet = seedBank.SeedPackets[i];
            if (!Input.GetKeyDown(SeedPacketKeybinds[i])) continue;
            if (!packet.CanPickUp()) continue;

            if (board.mCursorObject?.mCursorType == CursorType.Shovel)
            {
                board.mCursorObject.Die();
            }

            if (LastSelectedPacket != null)
            {
                LastSelectedPacket.Deselected(0);
                LastSelectedPacket.Activate();
            }

            packet.Selected(0, false);
            packet.Activated(0, false);

            LastSelectedPacket = packet;
            break;
        }
    }

    private static bool CanPickupShovel(Board board)
    {
        return board is { ShowShovel: true, mPaused: false } && board.mCursorObject?.mCursorType != CursorType.Shovel;
    }
}