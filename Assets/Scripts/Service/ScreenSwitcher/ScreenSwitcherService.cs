using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using View;

namespace Service.ScreenSwitcher
{
    public class ScreenSwitcherService : IScreenSwitcherService
    {
        private readonly Dictionary<string, IView> _activeScreens = new();

        public void AddView(IView view)
        {
            _activeScreens.TryAdd(view.ScreenId, view);
        }

        public async UniTask ShowScreen(string screenId)
        {
            if (_activeScreens.TryGetValue(screenId, out IView view))
            {
                await view.Show();
            }
        }

        public async UniTask HideScreen(string screenId)
        {
            if (_activeScreens.TryGetValue(screenId, out var view))
            {
                await view.Hide();
            }
        }
    }
}