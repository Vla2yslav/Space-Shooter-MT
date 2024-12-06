using System;
using Domain.Game;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.Game
{
    public class GameView : BaseView, IGameView
    {
        public const string SCREEN_ID = "GameScreen";
        
        public event Action<Vector2> OnJoystickMove;
        public event Action OnExitClicked;
        public event Action OnRestartClicked;
        public event Action OnNextClicked;
        public event Action OnShipDamaged;
        public event Action OnTargetHit;

        public override string ScreenId => SCREEN_ID;

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _topExitButton;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private ResultView _resultView;
        
        [SerializeField] private ShipView _ship;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        protected override void Awake()
        {
            base.Awake();
            
            BindView();
        }

        private void OnDestroy() => UnbindView();

        private void BindView()
        {
            _topExitButton.onClick.AddListener(OnExitButtonClicked);
            _resultView.OnNextClicked += OnNextButtonClicked;
            _resultView.OnRestartClicked += OnRestartButtonClicked;
            _resultView.OnExitClicked += OnExitButtonClicked;
            _ship.OnGetDamage += OnShipGetDamage;
            _ship.OnHitTarget += OnShipHitTarget;
        }

        private void UnbindView()
        {
            _topExitButton.onClick.RemoveListener(OnExitButtonClicked);
            _resultView.OnNextClicked -= OnNextButtonClicked;
            _resultView.OnRestartClicked -= OnRestartButtonClicked;
            _resultView.OnExitClicked -= OnExitButtonClicked;
            _ship.OnGetDamage -= OnShipGetDamage;
            _ship.OnHitTarget -= OnShipHitTarget;
        }

        private void Update()
        {
            if (_joystick && _joystick.Direction != Vector2.zero)
            {
                OnJoystickMove?.Invoke(_joystick.Direction);
            }
        }
        
        public void SetLevelData(LevelData levelData)
        {
            _levelText.text = $"Level: {levelData.Number}";
            _ship.SetShootingFrequency(levelData.ShootingFrequency);
            _asteroidSpawner.SetAsteroidSpeed(levelData.AsteroidSpeed);
            _asteroidSpawner.SetAsteroidSpawnFrequency(levelData.SpawnFrequency);
        }

        public void SetHealth(int health) => _healthText.text = $"Health: {health}";

        public void SetScore(int score) => _scoreText.text = $"Score: {score}";

        public void UpdateShipPosition(Vector2 direction) => _ship.UpdateMovement(direction);

        public void ResetGame()
        {
            _resultView.HideResult();
            _ship.ResetShip();
        }

        public void StartGame()
        {
            _canvasGroup.interactable = true;
            _ship.SetEnable(true);
            _asteroidSpawner.SetEnable(true);
        }

        public void StopGame(GameResult gameResult)
        {
            _ship.SetEnable(false);
            _asteroidSpawner.SetEnable(false);
            
            _resultView.ShowResult(gameResult.IsWin);
        }

        private void OnExitButtonClicked() => OnExitClicked?.Invoke();
        
        private void OnNextButtonClicked() => OnNextClicked?.Invoke();

        private void OnRestartButtonClicked() => OnRestartClicked?.Invoke();
        
        private void OnShipGetDamage() => OnShipDamaged?.Invoke();

        private void OnShipHitTarget() => OnTargetHit?.Invoke();
    }
}