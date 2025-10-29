using BadAPI.events;
using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace BadAPI.patchers;

#nullable disable
internal static class BoardPatcher
{
    public static Board Board { get; private set; }

    public static void Init()
    {
    }

    [HarmonyPatch(typeof(Board))]
    internal static class BoardPatch
    {
        [HarmonyPatch(nameof(Board.InitLevel))]
        internal static void Prefix(Board __instance)
        {
            Board = __instance;
            BoardEvents.InvokeInitPre(__instance);
        }

        [HarmonyPatch(nameof(Board.InitLevel))]
        internal static void Postfix(Board __instance)
        {
            BoardEvents.InvokeInitPost(__instance);
        }

        [HarmonyPatch(nameof(Board.StartLevel))]
        [HarmonyPrefix]
        internal static void StartPre(Board __instance)
        {
            BoardEvents.InvokeStartedPre(__instance);
        }

        [HarmonyPatch(nameof(Board.StartLevel))]
        [HarmonyPostfix]
        internal static void StartPost(Board __instance)
        {
            BoardEvents.InvokeStartedPost(__instance);
        }

        [HarmonyPatch(nameof(Board.DisposeBoard))]
        [HarmonyPrefix]
        internal static void DisposePre(Board __instance)
        {
            Board = null;
            BoardEvents.InvokeDispose(__instance);
        }

        [HarmonyPatch(nameof(Board.AddZombie))]
        [HarmonyPrefix]
        internal static void AddZombiePre(Board __instance, ZombieType theZombieType, int theFromWave,
            bool shakeBrush)
        {
            BoardEvents.InvokeZombieSpawn(__instance, theZombieType, theFromWave, shakeBrush);
        }

        [HarmonyPatch(nameof(Board.AddZombie))]
        [HarmonyPostfix]
        internal static void AddZombiePost(Board __instance, ZombieType theZombieType, int theFromWave,
            bool shakeBrush, Zombie __result)
        {
            BoardEvents.InvokeZombieSpawned(__instance, __result, theZombieType, theFromWave, shakeBrush);
        }

        [HarmonyPatch(nameof(Board.AddPlant))]
        [HarmonyPrefix]
        internal static void AddPlantPre(Board __instance, int theGridX, int theGridY, SeedType theSeedType,
            SeedType theImitaterType)
        {
            BoardEvents.InvokePlantPlanted(__instance, theGridX, theGridY, theSeedType, theImitaterType);
        }

        [HarmonyPatch(nameof(Board.AddCoin))]
        [HarmonyPostfix]
        internal static void AddCoinPost(Board __instance, float theX, float theY, CoinType theCoinType,
            CoinMotion theCoinMotion, Coin __result)
        {
            BoardEvents.InvokeDrop(__instance, __result, theX, theY, theCoinType, theCoinMotion);
        }
        
        [HarmonyPatch(nameof(Board.AddSunMoney))]
        [HarmonyPostfix]
        internal static void AddSunMoneyPost(Board __instance, int theAmount, int playerIndex)
        {
            BoardEvents.InvokeSunCollected(__instance, theAmount, playerIndex);
        }
    }

    // Special cases for AddProjectile since Board has 2 functions
    [HarmonyPatch(typeof(Board), nameof(Board.AddProjectile), new Type[]
    {
        typeof(float), typeof(float), typeof(int), typeof(int), typeof(ProjectileType)
    })]
    internal static class AddProjectile_Float_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(Board __instance,
            float theX, float theY, int aRenderOrder, int theRow, ProjectileType projectileType, Projectile __result)
        {
            // Ensure BoardEvents signature matches these parameters
            BoardEvents.InvokeProjectileAdded(__instance, __result, theX, theY, aRenderOrder, theRow, projectileType);
        }
    }

    [HarmonyPatch(typeof(Board), nameof(Board.AddProjectile), new Type[]
    {
        typeof(int), typeof(int), typeof(int), typeof(int), typeof(ProjectileType)
    })]
    internal static class AddProjectile_Int_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(Board __instance,
            int theX, int theY, int aRenderOrder, int theRow, ProjectileType projectileType, Projectile __result)
        {
            BoardEvents.InvokeProjectileAdded(__instance, __result, theX, theY, aRenderOrder, theRow, projectileType);
        }
    }
}