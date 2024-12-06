namespace Domain.Game
{
    public class SessionData
    {
        public readonly int Health;
        public readonly int Score;
        public readonly LevelData LevelData;

        public SessionData(int health, int score, LevelData levelData)
        {
            Health = health;
            Score = score;
            LevelData = levelData;
        }
    }
}