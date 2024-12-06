using Domain.Game;

namespace Service.LevelGenerator
{
    public interface ILevelGeneratorService
    {
        LevelData GetCurrentLevel();
        LevelData IncreaseCurrentLevel();
    }
}