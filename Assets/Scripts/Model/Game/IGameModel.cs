using System;
using Domain.Game;

namespace Model.Game
{
    public interface IGameModel
    {
        event Action<SessionData> OnCreateSession;
        event Action OnStartGame;
        event Action<GameResult> OnEndGame;
        event Action<int> OnHealthChange;
        event Action<int> OnScoreChange;

        void CreateSession();
        void StartGame();
        void GetDamage();
        void IncreaseScore();
    }
}