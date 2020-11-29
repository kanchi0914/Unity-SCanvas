using System.Collections.Generic;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Assets.Scripts.Examples.AdvGame.GUIData;

namespace Assets.Scripts.Examples.AdvGame.GameObjects
{
    public class LogWindowCanvas : EGCanvas
    {
        public LogWindowCanvas(List<Section> sections) : base("LogWindow")
        {
            // ログ表示中、背後のオブジェクトをクリックできないようにする
            var blockImage = new EGGameObject(this).SetRelativeSize(1, 1).SetImageColor(Color.clear);
            var logWindow = new EGGameObject(this).SetImageColor(Color.gray, 0.5f)
                .SetSize(700, 500);

            var backGroundImage = new EGGameObject(logWindow);
            backGroundImage.SetImageColor(Color.gray, 0.5f)
                .SetRelativeSize(1, 1);

            var headerText = new EGText(logWindow, "バックログ")
                .SetTextPreset(DefaultText)
                .SetTopCenterAnchor()
                .SetRelativeSize(1f, .1f) as EGText;

            var scrollRect = new EGVerticalLayoutScrollView(
                    logWindow, isAutoSizingWidth: true)
                .SetTopCenterAnchor()
                .SetRelativeSize(0.9f, 0.8f)
                .SetRelativePosition(0, 0.1f) as EGVerticalLayoutScrollView;
            scrollRect.SetPaddingAndSpacing(10).SetImageColor(Color.clear);
            sections.ForEach(s =>
            {
                var image = new EGGameObject(scrollRect).SetSize(0, 120);
                image.SetImageColor(Color.cyan, 0.5f);
                var logtext = new EGText(image, $"{s.Talker} : {s.Text}")
                    .SetTextPreset(DefaultText)
                    .SetParagraph(alignment:TextAnchor.UpperLeft, verticalOverflow: VerticalWrapMode.Overflow)
                    .SetCharacter(fontSize: 20)
                    .SetRelativeSize(1, 1)
                    as EGText;
            });
            scrollRect.ScrollRectComponent.verticalNormalizedPosition = 0;
            var button = new CloseButton(logWindow);
            button.AddOnClick(() => DestroySelf());
        }
    }
}