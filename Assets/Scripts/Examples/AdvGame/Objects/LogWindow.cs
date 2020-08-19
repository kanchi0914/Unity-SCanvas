using System.Collections.Generic;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class LogWindow : EGCanvas
    {
        public LogWindow(List<EgMessageWindow.Section> sections) : base("LogWindow")
        {
            var blockImage = new EGImage(this).SetRectSizeByRatio(1, 1);
            blockImage.SetImageColor(Color.clear);
            var window = new EGImage(this);
            window.SetColor(Color.gray, 0.5f);
                        
            var backGroundImage = new EGImage(window);
            backGroundImage.SetColor(Color.gray, 0.5f)
                .SetMiddleCenterAnchor()
                .SetLocalPos(0,0)
                .SetRectSizeByRatio(1, 1);
            
            window.SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSizeByRatio(0.6f, 0.6f);

            var text = new EGText(window, "バックログ")
                .SetTextConfig(font:GameData.FontsMap["genju"], fontSize:24, color:Color.white)
                .SetTopCenterAnchor()
                .SetLocalPos(0,0)
                .SetRectSizeByRatio(1f, 0.1f) as EGText;
            
            var scrollRect = new EGVerticalLayoutScrollView(
                    window, constantItemHeight:100, isAutoSizingWidth:true)
                .SetBottomCenterAnchor()
                .SetRectSizeByRatio(0.9f, 0.8f)
                .SetLocalPosByRatio(0, 0.05f) as EGVerticalLayoutScrollView;
            scrollRect.ContentArea.SetPadding(10, 10, 10, 10);
            scrollRect.ContentArea.SetSpacing(10);
            scrollRect.SetColor(Color.clear);
            sections.ForEach(s =>
            {
                var image = new EGImage(scrollRect);
                image.SetColor(Color.cyan, 0.5f);
                var logtext = new EGText(image, $"{s.Talker} : {s.Text}")
                    .SetTextConfig(font:GameData.FontsMap["genju"], color:Color.white, fontSize:24)
                    .SetRectSizeByRatio(1, 1) 
                    .SetFullStretchAnchor() as EGText;
                logtext.SetTextAlignment(TextAnchor.UpperLeft);
                scrollRect.AddItem(image);
            });

            var button = new CloseButton(window);
            button.AddOnClick(() => this.DestroySelf());
        }
        
    }
}