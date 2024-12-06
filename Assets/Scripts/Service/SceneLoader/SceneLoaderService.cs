namespace Service.SceneLoader
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public void LoadScene(string sceneName) => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}