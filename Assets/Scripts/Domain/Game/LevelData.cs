namespace Domain.Game
{
    public class LevelData
    {
        public readonly int Number;
        public readonly float AsteroidSpeed;
        public readonly float SpawnFrequency;
        public readonly float ShootingFrequency;
        public readonly int ScoreToWin;

        public LevelData(int number, float asteroidSpeed, float spawnFrequency, float shootingFrequency, int scoreToWin)
        {
            Number = number;
            AsteroidSpeed = asteroidSpeed;
            SpawnFrequency = spawnFrequency;
            ShootingFrequency = shootingFrequency;
            ScoreToWin = scoreToWin;
        }
    }

    public class LevelCredentials
    {
        public readonly int Number;
        public readonly int Seed;

        public LevelCredentials(int number, int seed)
        {
            Number = number;
            Seed = seed;
        }
    }
}