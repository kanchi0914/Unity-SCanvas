using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame;
using Assets.Scripts.Examples.AdvGame.GameObjects;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using static Assets.Scripts.Examples.AdvGame.GUIData;

namespace EGUI.Examples
{
    /// <summary>
    /// メッセージウィンドウのビュークラス
    /// 基本的に画面表示に関する操作のみを行い、状態を持たないように設計する
    /// </summary>
    public class AdvMessageWindow : EGGameObject
    {
        public bool IsOptionSelecting = false;
        public Scenario Scenario;
        private EGText talkerText;
        private EGText messageText;

        public AdvMessageWindow(EGCanvas parentCanvas, Scenario scenario) : base(parentCanvas)
        {
            Scenario = scenario;
            SetAnchorType(AnchorType.BottomCenter, false)
                .SetPosition(0, 20)
                .SetSize(600, 150)
                .SetImageColor(Color.gray, 0.5f);

            var menus = new EgHorizontalLayoutView(
                    this,
                    isAutoSizingHeight: true,
                    isAutoSizingWidth: true
                )
                .SetAnchorType(AnchorType.TopRight)
                .SetSize(300, 20);

            var logText = new EGText(menus, "LogTextLabel")
                .SetText("バックログ")
                .ResizeBestFIt()
                .SetTextPreset(DefaultText);
            var saveText = new EGText(menus, "SaveTextLabel")
                .SetText("セーブ")
                .ResizeBestFIt()
                .SetTextPreset(DefaultText);
            var loadText = new EGText(menus, "LoadTextLabel")
                .SetText("ロード")
                .ResizeBestFIt()
                .SetTextPreset(DefaultText);
            var optionText = new EGText(menus, "ConfigTextLabel")
                .SetText("オプション")
                .ResizeBestFIt()
                .SetTextPreset(DefaultText);

            messageText = new EGText(gameObject, "Test text")
                .SetParagraph(alignment: TextAnchor.UpperLeft)
                .SetText("This it a text!")
                .SetTextPreset(MessageWindowText)
                .SetAnchorType(AnchorType.TopCenter, false)
                .SetPosition(0, -20)
                .SetSize(550, 120)
                .AddOnClick(OnClicked) as EGText;

            var talkerPanelImage = new EGGameObject(this)
                .SetImageColor(Color.gray, 0.5f)
                .SetAnchorType(AnchorType.TopLeft)
                .SetPosition(0, 40)
                .SetSize(200, 40);
            
            talkerText = new EGText(talkerPanelImage.gameObject, "Text")
                    .SetTextPreset(DefaultText)
                    .SetRelativeSize(1, 1)
                as EGText;
            gameObject.GetImageComponent().raycastTarget = true;

            logText.AddOnClick(() =>
            {
                new LogWindowCanvas(Scenario.SentSections);
            });
            saveText.AddOnClick(() => new SaveMenu(Scenario));
            loadText.AddOnClick(() => new LoadMenu());
            optionText.AddOnClick(() => new OptionMenu());

            messageText.SetTextPreset(MessageWindowText);
            talkerText.SetTextPreset(DefaultText);
        }

        public void OnClicked()
        {
            if (!IsOptionSelecting)
            {
                Scenario?.SendSection();
            }
        }

        public void SetTalkerText(string talker)
        {
            talkerText.SetText(talker);
        }

        public void SetMessageText(string message)
        {
            messageText.SetText(message);
        }
    }
}