using Cysharp.Threading.Tasks;
using Model.MainMenu;
using Model.Scene;
using Service.ScreenSwitcher;
using View.MainMenu;
using View.Statistic;

namespace Presenter.MainMenu
{
    public class MainMenuPresenter : IPresenter
    {
        private readonly IMainMenuView _view;
        private readonly IMainMenuModel _mainMenuModel;
        private readonly ISceneModel _sceneModel;
        private readonly IScreenSwitcherService _screenSwitcherService;

        public MainMenuPresenter(
            IMainMenuView view, 
            IMainMenuModel mainMenuModel, 
            ISceneModel sceneModel, 
            IScreenSwitcherService screenSwitcherService)
        {
            _view = view;
            _mainMenuModel = mainMenuModel;
            _sceneModel = sceneModel;
            _screenSwitcherService = screenSwitcherService;
        }

        public void Initialize()
        {
            BindView();
            LoadLevel();
        }

        public void Dispose() => UnbindView();

        private void BindView()
        {
            _view.OnPlayClicked += OnPlayClicked;
            _view.OnStatisticClicked += OnStatisticClicked;
        }
        
        private void UnbindView()
        {
            _view.OnPlayClicked -= OnPlayClicked;
            _view.OnStatisticClicked -= OnStatisticClicked;
        }

        private void OnPlayClicked() => _sceneModel.LoadGameScene();

        private async void OnStatisticClicked() =>
            await (_screenSwitcherService.HideScreen(MainMenuView.SCREEN_ID),
                _screenSwitcherService.ShowScreen(StatisticView.SCREEN_ID));

        private void LoadLevel() => _view.SetLevel(_mainMenuModel.GetLevelNumber());
    }
}