using Il2CppReloaded.Gameplay;

namespace BadAPI.events;

#nullable disable
public static class BoardEvents
{
    public static event Action<Board> OnBoardStartedPre;
    public static event Action<Board> OnBoardStartedPost;
    
    internal static void InvokeBoardStartedPre(Board board) => OnBoardStartedPre?.Invoke(board);
    internal static void InvokeBoardStartedPost(Board board) => OnBoardStartedPost?.Invoke(board);
}