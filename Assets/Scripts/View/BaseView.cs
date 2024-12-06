using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace View
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected Canvas _canvas;

        protected virtual void Awake()
        {
            _canvasGroup ??= GetComponent<CanvasGroup>();
            _canvas ??= GetComponent<Canvas>();
        }

        public abstract string ScreenId { get; }

        public virtual async UniTask Show()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = true;
            _canvas.enabled = true;
            await _canvasGroup.DOFade(1f, 0.3f);
            _canvasGroup.interactable = true;
            
            OnVisible();
        }

        public virtual async UniTask Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            await _canvasGroup.DOFade(0f, 0.3f);

            _canvas.enabled = false;
            
            OnInvisible();
        }
        
        protected virtual void OnVisible() { }

        protected virtual void OnInvisible() { }
    }
}