using Cysharp.Threading.Tasks;
using View;

namespace Service.ScreenSwitcher
{
    public interface IScreenSwitcherService
    {
        void AddView(IView view);
        UniTask ShowScreen(string screenId);
        UniTask HideScreen(string screenId);
    }
}