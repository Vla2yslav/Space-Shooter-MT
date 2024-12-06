using Service;
using Service.SceneLoader;

namespace Model.Scene
{
    public class SceneModel : ISceneModel
    {
        private readonly ISceneLoaderService _sceneLoaderService;

        public SceneModel(ISceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        public void LoadMainMenuScene()
        {
            _sceneLoaderService.LoadScene(Config.Scenes.MAIN_MENU);
        }

        public void LoadGameScene()
        {
            _sceneLoaderService.LoadScene(Config.Scenes.GAME);
        }
    }
}