using System;
using UnityEngine;
using UnityEngine.UI;

namespace View.Game
{
    public class ResultView : MonoBehaviour
    {
        public event Action OnExitClicked;
        public event Action OnRestartClicked;
        public event Action OnNextClicked;
        
        [SerializeField] private GameObject _resultView;
        [SerializeField] private GameObject _winResult;
        [SerializeField] private GameObject _loseResult;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _loseExitButton;
        [SerializeField] private Button _winExitButton;

        private void Awake()
        {
            _nextButton.onClick.AddListener(OnNextClick);
            _restartButton.onClick.AddListener(OnRestartClick);
            _loseExitButton.onClick.AddListener(OnExitClick);
            _winExitButton.onClick.AddListener(OnExitClick);
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveListener(OnNextClick);
            _restartButton.onClick.RemoveListener(OnRestartClick);
            _loseExitButton.onClick.RemoveListener(OnExitClick);
            _winExitButton.onClick.RemoveListener(OnExitClick);
        }

        public void ShowResult(bool isWin)
        {
            _resultView.SetActive(true);
            
            _winResult.SetActive(isWin);
            _loseResult.SetActive(!isWin);
        }

        public void HideResult() => _resultView.SetActive(false);
        
        private void OnNextClick() => OnNextClicked?.Invoke();
        
        private void OnRestartClick() => OnRestartClicked?.Invoke();
        
        private void OnExitClick() => OnExitClicked?.Invoke();
    }
}