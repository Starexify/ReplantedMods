using BadAPI.patchers;
using MelonLoader;

[assembly: MelonInfo(typeof(BadAPI.BadMod), "BadMod", "0.0.1", "ImVeryBad")]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

namespace BadAPI;

public class BadAPICore : MelonMod
{
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