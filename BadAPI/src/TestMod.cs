using BadAPI.events;
using MelonLoader;

[assembly: MelonInfo(typeof(BadAPI.BadMod), "BadMod", "0.0.1", "ImVeryBad")]

namespace BadAPI;

public class BadMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        BoardEvents.OnBoardStartedPre += board => LoggerInstance.Msg("Called OnBoardStartedPre");
        BoardEvents.OnBoardStartedPost += board => LoggerInstance.Msg("Called OnBoardStartedPost");
    }
}