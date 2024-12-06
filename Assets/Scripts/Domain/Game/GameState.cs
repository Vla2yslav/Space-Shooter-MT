namespace Domain.Game
{
    public class GameState
    {
        public int LevelNumber { get; set; }
        public int Health { get; set; }
        public int Score { get; set; }
        public float StartGameTime { get; set; }
        public float EndGameTime { get; set; }
    }
}