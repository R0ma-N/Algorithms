using System;

namespace Lesson_2
{
    internal class BaseMenuItem
    {
        public string MenuItemText { get; private set; }
        Action MenuAction;

        public BaseMenuItem(string text, Action menuAction)
        {
            MenuItemText = text;
            MenuAction = menuAction;
        }

        /// <summary>
        /// Функционал данного пункта меню
        /// </summary>
        public void DoMenuAction()
        {
            MenuAction();
        }
    }
}