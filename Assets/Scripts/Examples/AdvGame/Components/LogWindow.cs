﻿using System.Collections.Generic;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UIElements;
using static Assets.Scripts.Examples.AdvGame.GUIData;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class LogWindow : EGCanvas
    {
        public LogWindow() : base("LogWindow")
        {
            var sections = CanvasRenderer.Instance.Scenario.SentSections;
            var blockImage = new EGGameObject(this).SetRectSizeByRatio(1, 1);
            blockImage.SetImageColor(Color.clear);
            var window = new EGGameObject(this);
            window.SetImageColor(Color.gray, 0.5f);
                        
            var backGroundImage = new EGGameObject(window);
            backGroundImage.SetImageColor(Color.gray, 0.5f)
                .SetMiddleCenterAnchor()
                .SetLocalPos(0,0)
                .SetRectSizeByRatio(1, 1);
            
            window.SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSizeByRatio(0.6f, 0.6f);

            var text = new EGText(window, "バックログ")
                .SetTextPreset(DefaultText)
                .SetTopCenterAnchor()
                .SetLocalPos(0,0)
                .SetRectSizeByRatio(1f, 0.1f) as EGText;
            
            var scrollRect = new EGVerticalLayoutScrollView(
                    window, isAutoSizingWidth:true)
                .SetTopCenterAnchor()
                .SetRectSizeByRatio(0.9f, 0.8f)
                .SetLocalPosByRatio(0, 0.1f) as EGVerticalLayoutScrollView;
            scrollRect.SetPaddingAndSpacing(10).SetImageColor(Color.clear);
            sections?.ForEach(s =>
            {
                var image = new EGGameObject(scrollRect).SetRectSize(0, 120);
                image.SetImageColor(Color.cyan, 0.5f);
                var logtext = new EGText(image, $"{s.Talker} : {s.Text}")
                    .SetTextPreset(DefaultText)
                    .SetParagraph(verticalOverflow: VerticalWrapMode.Overflow)
                    .SetCharacter(fontSize:20)
                    .SetRectSizeByRatio(1, 1) 
                    .SetFullStretchAnchor() as EGText;
                logtext.SetParagraph(TextAnchor.UpperLeft);
            });

            var button = new CloseButton(window);
            button.AddOnClick(() => this.DestroySelf());
        }
        
    }
}