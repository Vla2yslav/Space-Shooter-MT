using System;
using Domain.Game;
using UnityEngine;

namespace View.Game
{
    public interface IGameView : IView
    {
        event Action<Vector2> OnJoystickMove;
        event Action OnExitClicked;
        event Action OnRestartClicked;
        event Action OnNextClicked;
        event Action OnShipDamaged;
        event Action OnTargetHit;
        
        void SetLevelData(LevelData levelData);
        void SetHealth(int health);
        void SetScore(int score);
        void UpdateShipPosition(Vector2 direction);
        void ResetGame();
        void StartGame();
        void StopGame(GameResult gameResult);
    }
}