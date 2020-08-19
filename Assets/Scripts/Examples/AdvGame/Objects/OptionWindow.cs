using System;
using System.Security.Cryptography;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class OptionWindow : EGCanvas
    {
        private EgVerticalLayoutView options;
        private AdvMessageWindow messageWindow;
        public OptionWindow(AdvMessageWindow messageWindow) : base("OptionMenu")
        {
            messageWindow.IsOptionSelecting = true;
            this.messageWindow = messageWindow;
            // var blockImage = new EGImage(this).SetRectSizeByRatio(1, 1);
            // blockImage.SetColor(Color.clear);
            var window = new EGUIObject(this);
            window.SetMiddleCenterAnchor()
                .SetLocalPosByRatio(0, 0.2f)
                .SetRectSizeByRatio(0.4f, 0.3f);
            var backGroundImage = new EGImage(window) as EGImage;
            backGroundImage.SetColor(Color.white, 0.5f)
                .SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSizeByRatio(1, 1);
            options = new EgVerticalLayoutView(window, isAutoSizingWidth:true)
                .SetRectSizeByRatio(1,1) as EgVerticalLayoutView;
            options.SetPadding(10,10,10,10);
            options.SetSpacing(10);
        }

        public void AddOption(string text, Action action)
        {
            var button = new EGButton(null, text).AddOnClick(action)
                .SetRectSize(50, 60) as EGButton;
            button.AddOnClick(() => messageWindow.IsOptionSelecting = false);
            button.AddOnClick(() => this.DestroySelf());
            button.SetColor(ColorType.Gray, 0.5f);
            button.TextObject.SetTextConfig(color: Color.white, font: GameData.FontsMap["genju"], fontSize: 24);
            options.AddItem(button);
        }
    }
}