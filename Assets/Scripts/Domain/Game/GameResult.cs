namespace Domain.Game
{
    public class GameResult
    {
        public readonly int LevelNumber;
        public readonly int Score;
        public readonly bool IsWin;
        public readonly float Duration;

        public GameResult(int levelNumber, int score, bool isWin, float duration)
        {
            LevelNumber = levelNumber;
            Score = score;
            IsWin = isWin;
            Duration = duration;
        }
    }
}