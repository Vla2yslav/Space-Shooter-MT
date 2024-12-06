using Domain.Game;
using Service.SaveLoad;
using UnityEngine;

namespace Service.LevelGenerator
{
    public class LevelGeneratorService : ILevelGeneratorService
    {
        private readonly ISaveLoadService _saveLoadService;
        private LevelData _currentLevel;

        public LevelGeneratorService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public LevelData GetCurrentLevel()
        {
            if (_currentLevel != null)
            {
                return _currentLevel;
            }
            
            LevelCredentials currentLevelCredentials;
            
            if (!_saveLoadService.HasKey(Config.Constants.CURRENT_LEVEL_KEY))
            {
                currentLevelCredentials = GenerateLevelCredentials(Config.Constants.START_LEVEL);
                _saveLoadService.Save<LevelCredentials>(Config.Constants.CURRENT_LEVEL_KEY, currentLevelCredentials);
            }
            else
            {
                currentLevelCredentials = _saveLoadService.Load<LevelCredentials>(Config.Constants.CURRENT_LEVEL_KEY);
            }

            currentLevelCredentials ??= GenerateLevelCredentials(Config.Constants.START_LEVEL);
            
            _currentLevel = GetLevelData(currentLevelCredentials);
            
            return _currentLevel;
        }

        public LevelData IncreaseCurrentLevel()
        {
            LevelCredentials nextLevelCredentials = GenerateLevelCredentials(_currentLevel.Number + 1);
            _saveLoadService.Save<LevelCredentials>(Config.Constants.CURRENT_LEVEL_KEY, nextLevelCredentials);

            _currentLevel = GetLevelData(nextLevelCredentials);

            return _currentLevel;
        }

        private LevelCredentials GenerateLevelCredentials(int level) => new(level, Random.Range(1, int.MaxValue));

        private LevelData GetLevelData(LevelCredentials levelCredentials)
        {
            Random.InitState(levelCredentials.Seed);

            return new LevelData(
                levelCredentials.Number, 
                Random.Range(2f, 7f), 
                Random.Range(1f, 3f),
                Random.Range(0.4f, 0.7f), 
                Config.Constants.DEFAULT_SCORE_TO_WIN + Config.Constants.DEFAULT_SCORE_STEP * levelCredentials.Number);
        }
    }
}