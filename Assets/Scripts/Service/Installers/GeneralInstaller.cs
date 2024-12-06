using Model.Game;
using Model.MainMenu;
using Model.Scene;
using Service.LevelGenerator;
using Service.SaveLoad;
using Service.SceneLoader;
using Service.ScreenSwitcher;
using Service.Statistic;
using Zenject;

namespace Service.Installers
{
    public class GeneralInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindModels();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<ScreenSwitcherService>().AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
            Container.BindInterfacesTo<LevelGeneratorService>().AsSingle();
            Container.BindInterfacesTo<StatisticService>().AsSingle();
            Container.BindInterfacesTo<SceneLoaderService>().AsSingle();
        }

        private void BindModels()
        {
            Container.BindInterfacesTo<MainMenuModel>().AsSingle();
            Container.BindInterfacesTo<SceneModel>().AsSingle();
            Container.BindInterfacesTo<GameModel>().AsSingle();
        }
    }
}