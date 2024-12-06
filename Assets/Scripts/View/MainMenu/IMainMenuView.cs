using System;

namespace View.MainMenu
{
    public interface IMainMenuView : IView
    {
        event Action OnPlayClicked;
        event Action OnStatisticClicked;

        void SetLevel(int level);
    }
}