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
        [HarmonyPrefix]
        internal static void Prefix(Board __instance)
        {
            BoardEvents.InvokeInitPre(__instance);
        }

        [HarmonyPatch(nameof(Board.InitLevel))]
        [HarmonyPostfix]
        internal static void Postfix(Board __instance)
        {
            BoardEvents.InvokeInitPost(__instance);
        }

        [HarmonyPatch(nameof(Board.StartLevel))]
        [HarmonyPrefix]
        internal static void StartPrefix(Board __instance)
        {
            BoardEvents.InvokeStartedPre(__instance);
        }

        [HarmonyPatch(nameof(Board.StartLevel))]
        [HarmonyPostfix]
        internal static void StartPostfix(Board __instance)
        {
            BoardEvents.InvokeStartedPost(__instance);
        }      
        
        [HarmonyPatch(nameof(Board.DisposeBoard))]
        [HarmonyPrefix]
        internal static void DisposePostfix(Board __instance)
        {
            BoardEvents.InvokeDispose(__instance);
        }

        [HarmonyPatch(nameof(Board.AddZombie))]
        [HarmonyPrefix]
        internal static void PrefixAddZombie(Board __instance, ZombieType theZombieType, int theFromWave,
            bool shakeBrush)
        {
            BoardEvents.InvokeZombieSpawn(__instance, theZombieType, theFromWave, shakeBrush);
        }

        [HarmonyPatch(nameof(Board.AddZombie))]
        [HarmonyPostfix]
        internal static void PostfixAddZombie(Board __instance, ZombieType theZombieType, int theFromWave,
            bool shakeBrush, Zombie __result)
        {
            BoardEvents.InvokeZombieSpawned(__instance, __result, theZombieType, theFromWave, shakeBrush);
        }

        [HarmonyPatch(nameof(Board.AddPlant))]
        [HarmonyPrefix]
        internal static void PrefixAddPlant(Board __instance, int theGridX, int theGridY, SeedType theSeedType,
            SeedType theImitaterType)
        {
            BoardEvents.InvokePlantPlanted(__instance, theGridX, theGridY, theSeedType, theImitaterType);
        }
    }
}