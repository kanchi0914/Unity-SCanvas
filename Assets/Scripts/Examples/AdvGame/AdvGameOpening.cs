﻿using System;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
 using UnityEngine.Networking;
 using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    public class AdvGameOpening
    {
        private EGCanvas canvas;
        
        public AdvGameOpening()
        {
            Init();
        }

        private void Init()
        {
            canvas = new EGCanvas("");

            var backGroundImage = new EGGameObject(canvas);
            Utils.SetBackgroundImage(backGroundImage, "Images/school");

            var titleText = new EGText(canvas, "たのしいアドベンチャーゲーム")
                    .SetCharacter(font: GUIData.GenjuGothicBold, fontSize: 48)
                    .SetMiddleCenterAnchor()
                    .SetRectSizeByRatio(0.8f, 0.2f)
                    .SetLocalPosByRatio(0, -0.25f)
                as EGText;
            var menus = new EGVerticalLayoutView(canvas, isAutoSizingWidth: true)
                .SetMiddleCenterAnchor()
                .SetRectSizeByRatio(0.40f, 0.4f)
                .SetLocalPosByRatio(0, 0.25f);
            var newGameText = new EGText(menus, "ニューゲーム").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            var loadGameText = new EGText(menus, "コンティニュー").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            var optionGameText = new EGText(menus, "オプション").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            newGameText.AddOnClick(() =>
            {
                CanvasStack.ClearAll();
                new Scenario1_School();
            });
            loadGameText.AddOnClick(() => new LoadMenu());
            optionGameText.AddOnClick(() => new OptionMenu());
        }
    }
}