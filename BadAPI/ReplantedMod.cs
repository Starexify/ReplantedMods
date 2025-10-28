using BadAPI.events;
using BadAPI.patchers;
using Il2CppReloaded.Gameplay;
using MelonLoader;

[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

#nullable disable
namespace BadAPI;

public abstract class Module : MelonMod
{
    protected static Board Board => BoardPatcher.Board;

    public override void OnInitializeMelon()
    {
        BoardEvents.OnBoardStarted += OnBoardStarted;
    }

    public void OnBoardStarted(Board board)
    {
    }
}