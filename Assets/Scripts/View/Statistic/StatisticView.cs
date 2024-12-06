using System;
using System.Collections.Generic;
using Domain.Game;
using UnityEngine;
using UnityEngine.UI;

namespace View.Statistic
{
    public class StatisticView : BaseView, IStatisticView
    {
        public const string SCREEN_ID = "StatisticScreen";

        public event Action OnStartView;
        public event Action OnExitClicked;

        public override string ScreenId => SCREEN_ID;

        [SerializeField] private Button _exitButton;
        [SerializeField] private Transform _itemsContent;
        [SerializeField] private StatItemView _itemPrefab;
        [SerializeField] private GameObject _noDataText;
        
        protected override void Awake()
        {
            base.Awake();
            
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        protected override void OnVisible()
        {
            base.OnVisible();
            
            OnStartView?.Invoke();
        }

        private void OnDestroy() => _exitButton.onClick.RemoveListener(OnExitButtonClicked);

        public void SetAllStatistic(List<GameResult> results)
        {
            _noDataText.SetActive(results.Count == 0);
            
            foreach (GameResult result in results)
            {
                StatItemView item = Instantiate(_itemPrefab, _itemsContent);
                item.Initialize(result.LevelNumber, result.Duration, result.Score, result.IsWin);
            }
        }

        private void OnExitButtonClicked() => OnExitClicked?.Invoke();
    }
}