using System.Collections.Generic;
using Domain.Game;

namespace Service.Statistic
{
    public interface IStatisticService
    {
        void AddGameResult(GameResult gameResult);
        List<GameResult> GetAllStatistic();
    }
}