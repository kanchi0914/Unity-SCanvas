using Assets.Scripts.Examples.AdvGame.GameObjects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class AdvGameOpening
    {
        public AdvGameOpening()
        {
            var canvas = new EGCanvas("testca");
            new EGGameObject(canvas).SetSize(400,300).SetImageColor(Color.blue);
            //Init();
        }

        private void Init()
        {
            var canvas = new EGCanvas("AdvOpeningCanvas");

            var backGroundImage = new EGGameObject(canvas).SetImage(BackgroundImageData.Images["school"]);
            Utils.SetImageAsBackground(backGroundImage);

            var titleText = new EGText(canvas, "たのしいアドベンチャーゲーム")
                    .SetCharacter(font: GUIData.GenjuGothicBold, fontSize: 48)
                    .SetAnchorType(AnchorType.TopCenter)
                    .SetPosition(0, -50)
                    .SetSize(800, 150)
                as EGText;
            var menus = new EGVerticalLayoutView(canvas, isAutoSizingWidth: true)
                .SetAnchorType(AnchorType.BottomCenter)
                .SetPosition(0, 50)
                .SetSize(500, 300);
            var newGameText = new EGText(menus, "ニューゲーム").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            var loadGameText = new EGText(menus, "コンティニュー").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            var optionGameText = new EGText(menus, "オプション").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            newGameText.AddOnClick(() =>
            {
                CanvasStack.ClearAll();
                new Scenario0_Morning().LoadScript();
            });
            loadGameText.AddOnClick(() => new LoadMenu());
            optionGameText.AddOnClick(() => new OptionMenu());
        }
    }
}