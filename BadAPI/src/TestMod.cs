using BadAPI.events;
using MelonLoader;

[assembly: MelonInfo(typeof(BadAPI.BadMod), "BadMod", "0.0.1", "ImVeryBad")]

namespace BadAPI;

public class BadMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        BoardEvents.OnInitPre += board => LoggerInstance.Msg($"Called OnBoardInitPre: {board}");
        BoardEvents.OnInitPost += board => LoggerInstance.Msg($"Called OnBoardInitPost: {board}");
        BoardEvents.OnStartedPre += board => LoggerInstance.Msg($"Called OnBoardStartedPre: {board}");
        BoardEvents.OnStartedPost += board => LoggerInstance.Msg($"Called OnboardStartedPost: {board}");
        BoardEvents.OnDispose += board => LoggerInstance.Msg($"Called OnDispose: {board}");
        //BoardEvents.OnPause += (board, paused) => LoggerInstance.Msg($"Called OnBoardPause: {board} {paused}");
        //BoardEvents.OnResume += (board, paused) => LoggerInstance.Msg($"Called OnBoardResume: {board} {paused}");

        BoardEvents.OnZombieSpawn += (board, zombieType, wave, shakeBush) =>
            LoggerInstance.Msg($"Called OnZombieSpawn with params {board} {zombieType} {wave} {shakeBush}");

        BoardEvents.OnZombieSpawned += (board, zombie, zombieType, wave, shakeBush) =>
            LoggerInstance.Msg($"Called OnZombieSpawned with params {board} {zombieType} {wave} {shakeBush} {zombie}");

        BoardEvents.OnPlantPlanted += (board, gridX, gridY, seedType, imitaterType) =>
            LoggerInstance.Msg($"Called OnPlantPlanted with params {board} {gridX} {gridY} {seedType} {imitaterType}");
    }
}