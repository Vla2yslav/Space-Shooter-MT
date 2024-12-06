using Presenter.MainMenu;
using Presenter.Statistic;
using View.MainMenu;
using View.Statistic;
using Zenject;

namespace Service.Installers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMainMenuView>().To<MainMenuView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle().NonLazy();
            
            Container.Bind<IStatisticView>().To<StatisticView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<StatisticPresenter>().AsSingle().NonLazy();
        }
    }
}