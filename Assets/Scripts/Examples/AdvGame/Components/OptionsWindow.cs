﻿using System;
using System.Security.Cryptography;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Examples.AdvGame.GUIData;

namespace Assets.Scripts.Examples.AdvGame.Objects
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
                    .SetLocalPosByRatio(0, -0.2f)
                    .SetRectSizeByRatio(0.4f, 0.3f)
                as EGVerticalLayoutView;
            options.gameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
        }

        public void AddOption(string text, Action action)
        {
            var button = new EGButton(options, text)
                    .SetRectSize(50, 60)
                    .SetImageColor(Color.gray, 0.5f)
                as EGButton;
            button.SetOnOnClick(() =>
            {
                messageWindow.IsOptionSelecting = false;
                DestroySelf();
                action.Invoke();
            });
            var layoutElement = button.gameObject.AddComponent<LayoutElement>();
            layoutElement.minHeight = 50;
            button.TextObject.SetTextPreset(OptionText);
        }
    }
}