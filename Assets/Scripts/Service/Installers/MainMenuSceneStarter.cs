using Service.ScreenSwitcher;
using UnityEngine;
using View.MainMenu;
using View.Statistic;
using Zenject;

namespace Service.Installers
{
    public class MainMenuSceneStarter : MonoBehaviour
    {
        [Inject]
        private void InjectViews(
            IScreenSwitcherService screenSwitcherService, 
            IMainMenuView mainMenuView, 
            IStatisticView statisticView)
        {
            screenSwitcherService.AddView(mainMenuView);
            screenSwitcherService.AddView(statisticView);
        }
    }
}