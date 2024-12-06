using Service.LevelGenerator;

namespace Model.MainMenu
{
    public class MainMenuModel : IMainMenuModel
    {
        private readonly ILevelGeneratorService _levelGeneratorService;

        public MainMenuModel(ILevelGeneratorService levelGeneratorService)
        {
            _levelGeneratorService = levelGeneratorService;
        }

        public int GetLevelNumber()
        {
            return _levelGeneratorService.GetCurrentLevel().Number;
        }
    }
}