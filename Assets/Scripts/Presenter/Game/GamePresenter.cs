using System;
using Cysharp.Threading.Tasks;
using Domain.Game;
using Model.Game;
using Model.Scene;
using Service;
using UnityEngine;
using View.Game;

namespace Presenter.Game
{
    public class GamePresenter : IPresenter
    {
        private readonly IGameView _gameView;
        private readonly IGameModel _gameModel;
        private readonly ISceneModel _sceneModel;

        public GamePresenter(IGameView gameView, IGameModel gameModel, ISceneModel sceneModel)
        {
            _gameView = gameView;
            _gameModel = gameModel;
            _sceneModel = sceneModel;
        }

        public void Initialize()
        {
            BindView();
            BindModel();
            
            _gameModel.CreateSession();
        }

        public void Dispose()
        {
            UnbindView();
            UnbindModel();
        }

        private void BindView()
        {
            _gameView.OnJoystickMove += OnJoystickMoved;
            _gameView.OnExitClicked += OnExitClicked;
            _gameView.OnRestartClicked += OnRestartClicked;
            _gameView.OnNextClicked += OnNextClicked;
            _gameView.OnShipDamaged += OnShipDamaged;
            _gameView.OnTargetHit += OnTargetHit;
        }
        
        private void BindModel()
        {
            _gameModel.OnCreateSession += OnCreateSession;
            _gameModel.OnStartGame += OnGameStarted;
            _gameModel.OnHealthChange += OnHealthChanged;
            _gameModel.OnScoreChange += OnScoreChanged;
            _gameModel.OnEndGame += OnGameEnded;
        }
        
        private void UnbindView()
        {
            _gameView.OnJoystickMove -= OnJoystickMoved;
            _gameView.OnExitClicked -= OnExitClicked;
            _gameView.OnRestartClicked -= OnRestartClicked;
            _gameView.OnNextClicked -= OnNextClicked;
            _gameView.OnShipDamaged -= OnShipDamaged;
            _gameView.OnTargetHit -= OnTargetHit;
        }
        
        private void UnbindModel()
        {
            _gameModel.OnCreateSession -= OnCreateSession;
            _gameModel.OnStartGame -= OnGameStarted;
            _gameModel.OnHealthChange -= OnHealthChanged;
            _gameModel.OnScoreChange -= OnScoreChanged;
            _gameModel.OnEndGame -= OnGameEnded;
        }

        private void OnJoystickMoved(Vector2 direction) => _gameView.UpdateShipPosition(direction);

        private void OnExitClicked() => _sceneModel.LoadMainMenuScene();
        
        private void OnRestartClicked() => _gameModel.CreateSession();
        
        private void OnNextClicked() => _gameModel.CreateSession();
        
        private void OnShipDamaged() => _gameModel.GetDamage();

        private void OnTargetHit() => _gameModel.IncreaseScore();

        private void OnCreateSession(SessionData session)
        {
            _gameView.SetLevelData(session.LevelData);
            _gameView.SetHealth(session.Health);
            _gameView.SetScore(session.Score);
            
            _gameView.ResetGame();
            
            StartGameWithDelay();
        }
        
        private void OnGameStarted() => _gameView.StartGame();

        private void OnHealthChanged(int health) => _gameView.SetHealth(health);

        private void OnScoreChanged(int score) => _gameView.SetScore(score);

        private void OnGameEnded(GameResult gameResult) => _gameView.StopGame(gameResult);

        private async void StartGameWithDelay()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Config.Constants.START_GAME_DELAY));
            
            _gameModel.StartGame();
        }
    }
}