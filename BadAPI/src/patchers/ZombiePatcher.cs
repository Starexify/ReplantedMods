using BadAPI.events;
using HarmonyLib;
using Il2CppReloaded.Gameplay;
using Il2CppSource.Controllers;

namespace BadAPI.patchers;

#nullable disable
internal static class ZombiePatcher
{
    [HarmonyPatch(typeof(Zombie))]
    internal static class ZombiePatch
    {
        [HarmonyPatch(nameof(Zombie.ZombieInitialize))]
        internal static void Prefix(Zombie __instance, int theRow, ZombieType theType, bool theVariant,
            Zombie theParentZombie, int theFromWave, ZombieController controller)
        {
            ZombieEvents.InvokeInitPre(__instance, theRow, theType, theVariant, theParentZombie, theFromWave,
                controller);
        }

        [HarmonyPatch(nameof(Zombie.ZombieInitialize))]
        internal static void Postfix(Zombie __instance, int theRow, ZombieType theType, bool theVariant,
            Zombie theParentZombie, int theFromWave, ZombieController controller)
        {
            ZombieEvents.InvokeInitPost(__instance, theRow, theType, theVariant, theParentZombie, theFromWave,
                controller);
        }

        [HarmonyPatch(nameof(Zombie.DieNoLoot))]
        [HarmonyPrefix]
        internal static void DieNoLootPre(Zombie __instance) => ZombieEvents.InvokeDie(__instance);

        [HarmonyPatch(nameof(Zombie.DieDeserialize))]
        [HarmonyPrefix]
        internal static void DieDesPre(Zombie __instance) => ZombieEvents.InvokeDie(__instance);

        [HarmonyPatch(nameof(Zombie.DieWithLoot))]
        [HarmonyPrefix]
        internal static void DieLootPre(Zombie __instance) => ZombieEvents.InvokeDie(__instance);

        [HarmonyPatch(nameof(Zombie.EatPlant))]
        [HarmonyPrefix]
        internal static void EatPlantPre(Zombie __instance, Plant thePlant)
        {
            ZombieEvents.InvokeEatingPlant(__instance, thePlant);
        }

        [HarmonyPatch(nameof(Zombie.EatZombie))]
        [HarmonyPrefix]
        internal static void EatZombiePre(Zombie __instance, Zombie theZombie)
        {
            ZombieEvents.InvokeEatingZombie(__instance, theZombie);
        }

        [HarmonyPatch(nameof(Zombie.TakeDamage))]
        [HarmonyPrefix]
        internal static void DamagePre(Zombie __instance, int theDamage, DamageFlags theDamageFlags)
        {
            ZombieEvents.InvokeDamage(__instance, theDamage, theDamageFlags);
        }
    }
}