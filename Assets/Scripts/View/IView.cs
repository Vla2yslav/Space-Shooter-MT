using Cysharp.Threading.Tasks;

namespace View
{
    public interface IView
    {
        string ScreenId { get; }
        
        UniTask Show();
        UniTask Hide();
    }
}