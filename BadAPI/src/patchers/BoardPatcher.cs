using BadAPI.events;
using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace BadAPI.patchers;

#nullable disable
public static class BoardPatcher
{
    public static Board Board { get; private set; }

    public static void Initialize()
    {
    }

    [HarmonyPatch(typeof(Board), "StartLevel")]
    private static class BoardStartLevelPatch
    {
        public static void Prefix(Board __instance)
        {
            Board = __instance;
            BoardEvents.InvokeBoardStarted(__instance);
        }
    }
}