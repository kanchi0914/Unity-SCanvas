using System.Collections.Generic;
using EGUI.GameObjects;
using UnityEngine;
using static Assets.Scripts.Examples.AdvGame.GUIData;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    /// <summary>
    /// メッセージウィンドウのビュークラス
    /// 基本的に画面表示に関する操作のみを行い、状態を持たないように設計する
    /// </summary>
    public class AdvMessageWindow : EgMessageWindow
    {
        public bool IsOptionSelecting = false;
        public Scenario Scenario;

        public AdvMessageWindow(EGGameObject parentCanvas, Scenario scenario) : base(parentCanvas.gameObject)
        {
            Scenario = scenario;
            var menus = new EgHorizontalLayoutView(
                    this,
                    isAutoSizingHeight: true,
                    isAutoSizingWidth: true
                )
                .SetTopRightAnchor()
                .SetLocalPosByRatio(0, 0)
                .SetRectSizeByRatio(0.4f, 0.1f);
     
            var logText = new EGText(menus, "バックログ", true).SetTextPreset(DefaultText);
            var saveText = new EGText(menus, "セーブ", true).SetTextPreset(DefaultText);
            var loadText = new EGText(menus, "ロード", true).SetTextPreset(DefaultText);
            var optionText = new EGText(menus, "オプション", true).SetTextPreset(DefaultText);
            
            logText.AddOnClick(() => new LogWindow(Scenario.SentSections));
            saveText.AddOnClick(() => new SaveMenu(Scenario));
            loadText.AddOnClick(() => new LoadMenu());
            optionText.AddOnClick(() => new OptionMenu());

            MessageText.SetTextPreset(MessageWindowText);
            TalkerText.SetTextPreset(DefaultText);
        }
        
        protected override void OnClicked()
        {
            if (!IsOptionSelecting)
            {
                Scenario.SendSection();
            }
        }
    }

}