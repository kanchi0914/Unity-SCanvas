using System;

namespace Assets.Scripts.Examples.AdvGame
{
    public struct Section
    {
        public string Talker;
        public string Text;
        public Action Action;

        public Section(string talker, string message, Action action = null)
        {
            Talker = talker;
            Text = message;
            Action = action;
        }
    }

}