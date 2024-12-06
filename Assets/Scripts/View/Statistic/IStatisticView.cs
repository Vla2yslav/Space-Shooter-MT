using System;
using System.Collections.Generic;
using Domain.Game;

namespace View.Statistic
{
    public interface IStatisticView : IView
    {
        event Action OnStartView;
        event Action OnExitClicked;

        void SetAllStatistic(List<GameResult> results);
    }
}