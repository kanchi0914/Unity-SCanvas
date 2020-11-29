using System;
using System.Linq;
using Assets.Scripts.EGUI.MonoBehaviourScripts;
using Assets.Scripts.Examples.AdvGame.GameObjects;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.Base;
using EGUI.GameObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.EGUI.MonoBehaviourScripts;
using Assets.Scripts.Extensions;
using UnityEngine;
using EGUI.Base;
using EGUI.EGGameObjects.Base;
using UniRx;
using Utils = Assets.Scripts.Examples.AdvGame.Utils;
using UnityEditor;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    public class AdvGameOpening
    {
        public AdvGameOpening()
        {
            var canvas = new EGCanvas("");
            // var image = new EGGameObject(canvas);
            // image.SetSize(800, 600);
            //new AdvMessageWindow(canvas, null);
            Init();
        }

        private void Init()
        {
            var canvas = new EGCanvas("AdvOpeningCanvas");

            var backGroundImage = new EGGameObject(canvas).SetImage(BackgroundImageData.Images["school"]);
            Utils.SetImageAsBackground(backGroundImage);

            var titleText = new EGText(canvas, "たのしいアドベンチャーゲーム")
                    .SetCharacter(font: GUIData.GenjuGothicBold, fontSize: 48)
                    .SetTopCenterAnchor()
                    .SetPosition(0, -50)
                    .SetSize(800, 150)
                as EGText;
            var menus = new EGVerticalLayoutView(canvas, isAutoSizingWidth: true)
                .SetBottomCenterAnchor()
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