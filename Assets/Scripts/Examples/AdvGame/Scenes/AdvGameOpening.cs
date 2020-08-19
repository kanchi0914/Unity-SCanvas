using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    public class AdvGameOpening
    {
        private EGCanvas canvas;

        public AdvGameOpening()
        {
            canvas = new EGCanvas();
            GameData.SaveSatas[0] = new SaveData("", 3, "");
            GameData.SaveSatas[1] = new SaveData("", 3, "");
            
            var backGroundImage = new EGImage(canvas);
            Utils.SetBackgroundImage(backGroundImage, "Images/school");

            var titleText = new EGText(canvas, "たのしいアドベンチャーゲーム", fontSize: 48)
                .SetMiddleCenterAnchor()
                .SetLocalPosByRatio(0, 0.25f)
                .SetRectSizeByRatio(0.8f, 0.2f) as EGText;
            titleText.SetFontByFilePath("Fonts/GenJyuuGothic-Bold");
            var menus = new EgVerticalLayoutView(canvas, isAutoSizingWidth: true)
                .SetMiddleCenterAnchor()
                .SetLocalPosByRatio(0, -0.25f)
                .SetRectSizeByRatio(0.40f, 0.4f);
            var newGameText = new EGText(menus, "ニューゲーム");
            var loadGameText = new EGText(menus, "コンティニュー");
            var optionGameText = new EGText(menus, "オプション");
            newGameText.AddOnClick(() =>
            {
                CanvasStack.ClearAll();
                new Scenario0_Morning().Load("intro",0);
            });
            loadGameText.AddOnClick(() => new LoadMenu());
            optionGameText.AddOnClick(() => new OptionMenu());
            menus.ChildrenObjects.ForEach(g =>
            {
                var text = g as EGText;
                text.SetFontByFilePath("Fonts/GenJyuuGothic-Bold");
                text.SetTextConfig(color: Color.gray, fontSize: 36);
                text.SetPointerEnteredTextColor(Color.white);
            });
        }
    }
}