using System.Collections.Generic;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UIElements;
using static Assets.Scripts.Examples.AdvGame.GUIData;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class LogWindow : EGCanvas
    {
        public LogWindow(List<Section> sections) : base("LogWindow")
        {
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
                .SetBottomCenterAnchor()
                .SetRectSizeByRatio(0.9f, 0.8f)
                .SetLocalPosByRatio(0, 0.05f) as EGVerticalLayoutScrollView;
            scrollRect.SetPaddingAndSpacing(10).SetImageColor(Color.clear);
            sections.ForEach(s =>
            {
                var image = new EGGameObject(scrollRect);
                image.SetImageColor(Color.cyan, 0.5f);
                var logtext = new EGText(image, $"{s.Talker} : {s.Text}")
                    .SetTextPreset(DefaultText)
                    .SetCharacter(fontSize:20)
                    .SetRectSizeByRatio(1, 1) 
                    .SetFullStretchAnchor() as EGText;
                logtext.SetParagraph(TextAnchor.UpperLeft);
                // scrollRect.AddItem(image);
            });

            var button = new CloseButton(window);
            button.AddOnClick(() => this.DestroySelf());
        }
        
    }
}