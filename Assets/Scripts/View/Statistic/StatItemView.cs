using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.Statistic
{
    public class StatItemView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundColor; 
        [SerializeField] private TextMeshProUGUI _levelNumText;
        [SerializeField] private TextMeshProUGUI _durationText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        [SerializeField] private Color _winColor;
        [SerializeField] private Color _loseColor;

        public void Initialize(int level, float duration, int score, bool isWin)
        {
            _backgroundColor.color = isWin ? _winColor : _loseColor;
            _levelNumText.text = $"Level: {level}";
            _durationText.text = $"Time: {FormatTime(duration)}";
            _scoreText.text = $"Score: {score}";
        }
        
        private string FormatTime(float seconds)
        {
            int minutes = (int)((seconds % 3600) / 60);
            int secs = (int)(seconds % 60);
    
            return $"{minutes:00}:{secs:00}";
        }
    }
}