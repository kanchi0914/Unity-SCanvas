using System;
using System.Security.Cryptography;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.Examples;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Examples.AdvGame.GUIData;

namespace Assets.Scripts.Examples.AdvGame.GameObjects
{
    public class OptionsWindow : EGCanvas
    {
        private EGVerticalLayoutView options;
        private AdvMessageWindow messageWindow;

        public OptionsWindow(AdvMessageWindow messageWindow) : base("OptionMenu")
        {
            this.messageWindow = messageWindow;
            this.messageWindow.IsOptionSelecting = true;
            options = new EGVerticalLayoutView(this, isAutoSizingWidth: true)
                    .SetPaddingAndSpacing(10)
                    .SetImageColor(Color.white, 0.5f)
                    .SetMiddleCenterAnchor()
                    .SetRelativePosition(0, -0.2f)
                    .SetRelativeSize(0.4f, 0.3f)
                as EGVerticalLayoutView;
            options.gameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
        }
        
        public void AddOption(Option option)
        {
            var optionButton = new EGButton(options, option.Text)
                    .SetSize(50, 60)
                    .SetImageColor(Color.gray, 0.5f)
                as EGButton;
            var layoutElement = optionButton.gameObject.AddComponent<LayoutElement>();
            layoutElement.minHeight = 50;
            optionButton.TextObject.SetTextPreset(OptionText);
            if (GameData.SelectedOptions.Contains(option.Id))
            {
                optionButton.SetImageColor(Color.gray, 0.8f);
                optionButton.TextObject.SetTextColor(Color.white, 0.7f);
            }
            optionButton.SetOnOnClick(() =>
            {
                messageWindow.IsOptionSelecting = false;
                DestroySelf();
                option.Select();
            });
        }

        public void AddOption(string text, Action action)
        {
            AddOption(new Option(null, text, action));
        }
    }
}