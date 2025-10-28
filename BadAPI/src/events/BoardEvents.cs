using Il2CppReloaded.Gameplay;

namespace BadAPI.events;

#nullable disable
public static class BoardEvents
{
    // Define events
    public static event Action<Board> OnBoardStarted;
 
    // Internal methods to trigger events (called by patchers)
    internal static void InvokeBoardStarted(Board board) => OnBoardStarted?.Invoke(board);
}