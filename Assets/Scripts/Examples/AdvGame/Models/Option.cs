using System;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Option
    {
        public string Id;
        public string Text;
        public Action SelectedAction;

        public Option(string text, Action selectedAction)
        {
            Text = text;
            SelectedAction = selectedAction;
        }

        public void Select()
        {
            GameData.SelectedOptions.Add(Id);
            SelectedAction.Invoke();
        }
    }
}