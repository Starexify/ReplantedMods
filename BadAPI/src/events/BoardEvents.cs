using Il2CppReloaded.Gameplay;

namespace BadAPI.events;

#nullable disable
public static class BoardEvents
{
    public static event Action<Board> OnInitPre;
    public static event Action<Board> OnInitPost;
    public static event Action<Board> OnStartedPre;
    public static event Action<Board> OnStartedPost;
    public static event Action<Board> OnUpdate;
    public static event Action<Board, bool> OnPause;
    public static event Action<Board, bool> OnResume;
    public static event Action<Board> OnDispose;
    public static event Action<Board, ZombieType, int, bool> OnZombieSpawn;
    public static event Action<Board, Zombie, ZombieType, int, bool> OnZombieSpawned;
    public static event Action<Board, int, int, SeedType, SeedType> OnPlantPlanted;
    public static event Action<Board, Coin, float, float, CoinType, CoinMotion> OnDropAdded;
    public static event Action<Board, int, int> OnSunCollected;
    public static event Action<Board, Projectile, float, float, int, int, ProjectileType> OnProjectileAdded;

    internal static void InvokeInitPre(Board board) => OnInitPre?.Invoke(board);
    internal static void InvokeInitPost(Board board) => OnInitPost?.Invoke(board);
    internal static void InvokeStartedPre(Board board) => OnStartedPre?.Invoke(board);
    internal static void InvokeStartedPost(Board board) => OnStartedPost?.Invoke(board);
    internal static void InvokeDispose(Board board) => OnDispose?.Invoke(board);
    internal static void InvokeUpdate(Board board) => OnUpdate?.Invoke(board);
    internal static void InvokePause(Board board, bool paused) => OnPause?.Invoke(board, paused);
    internal static void InvokeResume(Board board, bool paused) => OnResume?.Invoke(board, paused);

    internal static void InvokeZombieSpawn(Board board, ZombieType zombieType, int wave, bool shakeBrush) =>
        OnZombieSpawn?.Invoke(board, zombieType, wave, shakeBrush);

    internal static void InvokeZombieSpawned(Board board, Zombie zombie, ZombieType zombieType, int wave,
        bool shakeBrush) => OnZombieSpawned?.Invoke(board, zombie, zombieType, wave, shakeBrush);

    internal static void
        InvokePlantPlanted(Board board, int gridX, int gridY, SeedType seedType, SeedType imitaterType) =>
        OnPlantPlanted?.Invoke(board, gridX, gridY, seedType, imitaterType);

    internal static void InvokeDrop(Board board, Coin coin, float xPos, float yPos, CoinType coinType,
        CoinMotion coinMotion) => OnDropAdded?.Invoke(board, coin, xPos, yPos, coinType, coinMotion);

    internal static void InvokeSunCollected(Board board, int amount, int playerId) =>
        OnSunCollected?.Invoke(board, amount, playerId);

    internal static void InvokeProjectileAdded(Board board, Projectile projectile, float xPos, float yPos,
        int renderOrder, int row, ProjectileType projectileType) =>
        OnProjectileAdded?.Invoke(board, projectile, xPos, yPos, renderOrder, row, projectileType);
}