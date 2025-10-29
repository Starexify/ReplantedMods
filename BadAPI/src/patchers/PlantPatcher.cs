using BadAPI.events;
using HarmonyLib;
using Il2CppReloaded.Gameplay;
using Il2CppSource.Controllers;

namespace BadAPI.patchers;

#nullable disable
internal static class PlantPatcher
{
    [HarmonyPatch(typeof(Plant))]
    internal static class PlantPatch
    {
        [HarmonyPatch(nameof(Plant.PlantInitialize))]
        internal static void Prefix(Plant __instance, int theGridX, int theGridY, SeedType theSeedType,
            SeedType theImitaterType, PlantController controller)
        {
            PlantEvents.InvokeInitPre(__instance, theGridX, theGridY, theSeedType, theImitaterType, controller);
        }

        [HarmonyPatch(nameof(Plant.PlantInitialize))]
        internal static void Postfix(Plant __instance, int theGridX, int theGridY, SeedType theSeedType,
            SeedType theImitaterType, PlantController controller)
        {
            PlantEvents.InvokeInitPost(__instance, theGridX, theGridY, theSeedType, theImitaterType, controller);
        }

        [HarmonyPatch(nameof(Plant.Die))]
        [HarmonyPrefix]
        internal static void DiePre(Plant __instance)
        {
            PlantEvents.InvokeOnDeath(__instance);
        }

        [HarmonyPatch(nameof(Plant.Fire))]
        [HarmonyPrefix]
        internal static void DiePre(Plant __instance, Zombie theTargetZombie, int theRow, PlantWeapon thePlantWeapon)
        {
            PlantEvents.InvokeOnShoot(__instance, theTargetZombie, theRow, thePlantWeapon);
        }

        [HarmonyPatch(nameof(Plant.DoSpecial))]
        [HarmonyPrefix]
        internal static void DoSpecialPre(Plant __instance)
        {
            PlantEvents.InvokeOnSpecial(__instance);
        }
    }
}