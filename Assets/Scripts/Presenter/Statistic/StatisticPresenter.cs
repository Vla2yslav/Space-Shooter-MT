using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Domain.Game;
using Service.ScreenSwitcher;
using Service.Statistic;
using View.MainMenu;
using View.Statistic;

namespace Presenter.Statistic
{
    public class StatisticPresenter : IPresenter
    {
        private readonly IStatisticView _view;
        private readonly IStatisticService _statisticService;
        private readonly IScreenSwitcherService _screenSwitcherService;

        public StatisticPresenter(
            IStatisticView view, 
            IStatisticService statisticService, 
            IScreenSwitcherService screenSwitcherService)
        {
            _view = view;
            _statisticService = statisticService;
            _screenSwitcherService = screenSwitcherService;
        }

        public void Initialize() => BindView();

        public void Dispose() => UnbindView();

        private void BindView()
        {
            _view.OnStartView += LoadStatistic;
            _view.OnExitClicked += OnExitClicked;
        }

        private void UnbindView()
        {
            _view.OnStartView -= LoadStatistic;
            _view.OnExitClicked -= OnExitClicked;
        }

        private void LoadStatistic()
        {
            List<GameResult> statistic = _statisticService.GetAllStatistic();
            statistic.Reverse();
            
            _view.SetAllStatistic(statistic);
        }

        private async void OnExitClicked() =>
            await (_screenSwitcherService.HideScreen(StatisticView.SCREEN_ID),
                _screenSwitcherService.ShowScreen(MainMenuView.SCREEN_ID));
    }
}