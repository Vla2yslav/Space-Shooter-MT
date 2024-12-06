using System.Collections.Generic;
using Domain.Game;
using Service.SaveLoad;

namespace Service.Statistic
{
    public class StatisticService : IStatisticService
    {
        private readonly ISaveLoadService _saveLoadService;
        
        private List<GameResult> _gameResults = new List<GameResult>();

        public StatisticService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void AddGameResult(GameResult gameResult)
        {
            if (_gameResults is null || _gameResults.Count == 0)
            {
                _gameResults = LoadStatistic();
            }
            
            _gameResults.Add(gameResult);

            if (_gameResults.Count > Config.Constants.MAX_STATS_COUNT)
            {
                _gameResults.RemoveAt(0);
            }

            SaveStatistic();
        }

        public List<GameResult> GetAllStatistic() => LoadStatistic();

        private void SaveStatistic() => _saveLoadService.Save(Config.Constants.GAME_STATS_KEY, _gameResults);

        private List<GameResult> LoadStatistic()
        {
            List<GameResult> gameResults = new List<GameResult>();

            if (_saveLoadService.HasKey(Config.Constants.GAME_STATS_KEY))
            {
                gameResults = _saveLoadService.Load<List<GameResult>>(Config.Constants.GAME_STATS_KEY);
            }
            
            return gameResults;
        }
    }
}