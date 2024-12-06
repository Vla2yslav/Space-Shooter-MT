using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.MainMenu
{
    public class MainMenuView : BaseView, IMainMenuView
    {
        public const string SCREEN_ID = "MainMenuScreen";
        
        public event Action OnPlayClicked;
        public event Action OnStatisticClicked;

        public override string ScreenId => SCREEN_ID;

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _statisticButton;

        protected override void Awake()
        {
            base.Awake();
            
            BindView();
        }

        private void OnDestroy() => UnbindView();

        private void BindView()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _statisticButton.onClick.AddListener(OnStatisticButtonClicked);
        }
        
        private void UnbindView()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _statisticButton.onClick.RemoveListener(OnStatisticButtonClicked);
        }

        public void SetLevel(int level) => _levelText.text = $"Level: {level}";

        private void OnPlayButtonClicked() => OnPlayClicked?.Invoke();

        private void OnStatisticButtonClicked() => OnStatisticClicked?.Invoke();
    }
}