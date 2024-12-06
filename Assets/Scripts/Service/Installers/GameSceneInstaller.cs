using Presenter.Game;
using View.Game;
using Zenject;

namespace Service.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameView>().To<GameView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle().NonLazy();
        }
    }
}