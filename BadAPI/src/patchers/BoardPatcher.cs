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

    [HarmonyPatch(typeof(Board), nameof(Board.StartLevel))]
    [HarmonyPostfix]
    public static void BoardStartPrefix(Board __instance)
    {
        Board = __instance;
        BoardEvents.InvokeBoardStartedPre(__instance);
    }

    [HarmonyPatch(typeof(Board), nameof(Board.StartLevel))]
    [HarmonyPrefix]
    public static void BoardStartPostfix(Board __instance) => BoardEvents.InvokeBoardStartedPost(__instance);
}