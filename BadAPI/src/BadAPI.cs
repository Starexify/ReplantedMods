using BadAPI.patchers;
using Il2CppReloaded.Gameplay;
using MelonLoader;

[assembly: MelonInfo(typeof(BadAPI.BadMod), "BadMod", "0.0.1", "ImVeryBad")]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

namespace BadAPI;

public class BadAPI : MelonMod
{
    public static Board Board = BoardPatcher.Board;
    
    private static bool _initialized = false;

    public override void OnInitializeMelon()
    {
        if (_initialized) return;
        _initialized = true;

        LoggerInstance.Msg("BadAPI initializing...");

        BoardPatcher.Initialize();

        LoggerInstance.Msg("BadAPI initialized!");
    }
}