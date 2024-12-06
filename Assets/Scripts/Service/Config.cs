namespace Service
{
    public static class Config
    {
        public static class Constants
        {
            public const int DEFAULT_SHIP_HEALTH = 3;
            public const int DEFAULT_ASTEROID_HEALTH = 2;
            public const int DEFAULT_DAMAGE = 1;
            public const float DEFAULT_SCREEN_BOUND_OFFSET = 1;
            public const int DEFAULT_ASTEROID_DAMAGE = 1;
            public const int START_LEVEL = 1;
            public const string CURRENT_LEVEL_KEY = "CurrentLevel";
            public const int DEFAULT_SCORE_TO_WIN = 100;
            public const int DEFAULT_SCORE_STEP = 10;
            public const int DEFAULT_SCORE_PER_KILL = 10;
            public const string GAME_STATS_KEY = "GameStatistic";
            public const int MAX_STATS_COUNT = 10;
            public const float START_GAME_DELAY = 1f;
        }

        public static class Scenes
        {
            public const string MAIN_MENU = "MainMenuScene";
            public const string GAME = "GameScene";
        }
    }
}