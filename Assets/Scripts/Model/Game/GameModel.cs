using System;
using Domain.Game;
using Service;
using Service.LevelGenerator;
using Service.Statistic;
using UnityEngine;

namespace Model.Game
{
    public class GameModel : IGameModel
    {
        public event Action<SessionData> OnCreateSession;
        public event Action OnStartGame;
        public event Action<GameResult> OnEndGame;
        public event Action<int> OnHealthChange;
        public event Action<int> OnScoreChange; 
        
        private GameState _gameState;
        private LevelData _currentLevel;

        private readonly ILevelGeneratorService _levelGeneratorService;
        private readonly IStatisticService _statisticService;

        public GameModel(ILevelGeneratorService levelGeneratorService, IStatisticService statisticService)
        {
            _levelGeneratorService = levelGeneratorService;
            _statisticService = statisticService;
        }

        public void CreateSession()
        {
            _currentLevel = _levelGeneratorService.GetCurrentLevel();
            
            _gameState = new GameState()
            {
                LevelNumber = _currentLevel.Number,
                Health = Config.Constants.DEFAULT_SHIP_HEALTH,
                Score = 0
            };

            SessionData sessionData = new SessionData
            (
                _gameState.Health,
                _gameState.Score,
                _currentLevel
            );
            
            OnCreateSession?.Invoke(sessionData);
        }

        public void StartGame()
        {
            _gameState.StartGameTime = Time.time;
         
            OnStartGame?.Invoke();
        }

        public void GetDamage()
        {
            _gameState.Health -= Config.Constants.DEFAULT_ASTEROID_DAMAGE;
            OnHealthChange?.Invoke(_gameState.Health);

            if (_gameState.Health <= 0)
            {
                EndGame(false);
            }
        }
        
        public void IncreaseScore()
        {
            _gameState.Score += Config.Constants.DEFAULT_SCORE_PER_KILL;
            OnScoreChange?.Invoke(_gameState.Score);
            
            if (_gameState.Score >= _currentLevel.ScoreToWin)
            {
                _levelGeneratorService.IncreaseCurrentLevel();
                
                EndGame(true);
            }
        }

        private void EndGame(bool isWin = false)
        {
            _gameState.EndGameTime = Time.time;

            GameResult gameResult = new GameResult
            (
                _gameState.LevelNumber,
                _gameState.Score,
                isWin,
                _gameState.EndGameTime - _gameState.StartGameTime
            );
            
            _statisticService.AddGameResult(gameResult);
            
            OnEndGame?.Invoke(gameResult);
        }
    }
}