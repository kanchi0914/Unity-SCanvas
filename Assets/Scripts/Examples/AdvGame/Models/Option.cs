using System;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Option
    {
        // [Serializable]
        // public enum ID
        // {
        //     // シナリオ1
        //     cooperate_with_hiroko,
        //     dont_cooperate_with_hiroko,
        //     goto_saito_s_place,
        //     goto_class_2,
        //     goto_pc_room,
        //     ask_about_situation,
        //     ask_about_wallet,
        //     // シナリオ2
        //     ask_girl,
        //     check_seat,
        //     go_other_place,
        //     ask_about_saito,
        //     ask_about_wallet_in_class,
        //     ask_about_seat,
        //     //Scenario3_PCroom
        //     
        // }

        public string Id;
        public string Text;
        public Action SelectedAction;

        public Option(string id, string text, Action selectedAction)
        {
            Id = id;
            Text = text;
            SelectedAction = selectedAction;
        }

        public void Select()
        {
            if (Id != null) GameData.SelectedOptions.Add(Id);
            SelectedAction.Invoke();
        }
    }
}