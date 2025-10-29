using BadAPI.events;
using MelonLoader;

[assembly: MelonInfo(typeof(BadAPI.BadMod), "BadMod", "0.0.1", "ImVeryBad")]

namespace BadAPI;

public class BadMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        // Board Events
        /*
        BoardEvents.OnInitPre += board => LoggerInstance.Msg($"Called BoardEvents OnBoardInitPre: {board}");
        BoardEvents.OnInitPost += board => LoggerInstance.Msg($"Called BoardEvents OnBoardInitPost: {board}");
        BoardEvents.OnStartedPre += board => LoggerInstance.Msg($"Called BoardEvents OnBoardStartedPre: {board}");
        BoardEvents.OnStartedPost += board => LoggerInstance.Msg($"Called BoardEvents OnBoardStartedPost: {board}");
        BoardEvents.OnDispose += board => LoggerInstance.Msg($"Called BoardEvents OnDispose: {board}");
        //BoardEvents.OnPause += (board, paused) => LoggerInstance.Msg($"Called OnBoardPause: {board} {paused}");
        //BoardEvents.OnResume += (board, paused) => LoggerInstance.Msg($"Called OnBoardResume: {board} {paused}");

        BoardEvents.OnZombieSpawn += (board, zombieType, wave, shakeBush) =>
            LoggerInstance.Msg($"Called BoardEvents OnZombieSpawn with params {board} {zombieType} {wave} {shakeBush}");

        BoardEvents.OnZombieSpawned += (board, zombie, zombieType, wave, shakeBush) =>
            LoggerInstance.Msg(
                $"Called BoardEvents OnZombieSpawned with params {board} {zombieType} {wave} {shakeBush} {zombie}");

        BoardEvents.OnPlantPlanted += (board, gridX, gridY, seedType, imitaterType) =>
            LoggerInstance.Msg(
                $"Called BoardEvents OnPlantPlanted with params {board} {gridX} {gridY} {seedType} {imitaterType}");

        BoardEvents.OnDropAdded += (board, coin, xPos, yPos, coinType, coinMotion) =>
            LoggerInstance.Msg(
                $"Called BoardEvents OnDropAdded with params {board} {coin} {xPos} {yPos} {coinType} {coinMotion}");

        BoardEvents.OnSunCollected += (board, amount, playerId) =>
            LoggerInstance.Msg($"Called BoardEvents OnSunCollected with params {board} {amount} {playerId}");

        BoardEvents.OnProjectileAdded += (board, projectile, xPos, yPos, renderOrder, row, projectileType) =>
            LoggerInstance.Msg(
                $"Called BoardEvents OnProjectileAdded with params {board} {projectile} {xPos} {yPos} {renderOrder} {row} {projectileType}");
                */

        // Plant Events
        /*
        PlantEvents.OnInitPre += (plant, gridX, gridY, seedType, imitaterType, controller) =>
            LoggerInstance.Msg(
                $"Called PlantEvents OnInitPre with params {plant} {gridX} {gridY} {seedType} {imitaterType} {controller}");

        PlantEvents.OnInitPost += (plant, gridX, gridY, seedType, imitaterType, controller) =>
            LoggerInstance.Msg(
                $"Called PlantEvents OnInitPost with params {plant} {gridX} {gridY} {seedType} {imitaterType} {controller}");

        PlantEvents.OnDeath += (plant) => LoggerInstance.Msg($"Called PlantEvents OnDeath with params {plant}");

        PlantEvents.OnShoot += (plant, targetZombie, row, plantWeapon) =>
            LoggerInstance.Msg($"Called PlantEvents OnShoot with params {plant} {targetZombie} {row} {plantWeapon}");

        PlantEvents.OnSpecial += (plant) =>
            LoggerInstance.Msg($"Called PlantEvents OnSpecial with params {plant}");
            */

        // Zombie Events
        ZombieEvents.OnInitPre += (zombie, row, zombieType, variant, parentZombie, fromWave, controller) =>
            LoggerInstance.Msg(
                $"Called ZombieEvents OnInitPre with params {zombie} {row} {zombieType} {variant} {parentZombie} {fromWave} {controller}");

        ZombieEvents.OnInitPost += (zombie, row, zombieType, variant, parentZombie, fromWave, controller) =>
            LoggerInstance.Msg(
                $"Called ZombieEvents OnInitPost with params {zombie} {row} {zombieType} {variant} {parentZombie} {fromWave} {controller}");

        ZombieEvents.OnDie += (zombie) => LoggerInstance.Msg($"Called ZombieEvents OnInitPre with params {zombie}");

        ZombieEvents.OnEatingPlant += (zombie, plant) =>
            LoggerInstance.Msg($"Called ZombieEvents OnEatingZombie with params {zombie} {plant}");

        ZombieEvents.OnEatingZombie += (zombie, targetZombie) =>
            LoggerInstance.Msg($"Called ZombieEvents OnEatingZombie with params {zombie} {targetZombie}");

        ZombieEvents.OnDamage += (zombie, damage, damageFlags) =>
            LoggerInstance.Msg($"Called ZombieEvents OnDamage with params {zombie} {damage} {damageFlags}");
    }
}