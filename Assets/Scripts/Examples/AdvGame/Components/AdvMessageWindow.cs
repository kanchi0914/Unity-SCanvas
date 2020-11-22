using System.Collections.Generic;
using EGUI.GameObjects;
using UnityEngine;
using static Assets.Scripts.Examples.AdvGame.GUIData;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class AdvMessageWindow : EgMessageWindow
    {
        public int MessageNumber = 0;
        public List<Section> sendSections = new List<Section>();
        
        public bool IsOptionSelecting = false;

        public AdvMessageWindow(EGGameObject parentCanvas) : base(parentCanvas.gameObject)
        {
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
            
            logText.AddOnClick(() => new LogWindow(SentSections));
            saveText.AddOnClick(() => new SaveMenu("", "", MessageNumber));
            loadText.AddOnClick(() => new LoadMenu());
            optionText.AddOnClick(() => new OptionMenu());
            
            MessageText.SetTextPreset(DefaultText).SetCharacter(fontSize: 28);
            TalkerText.SetTextPreset(DefaultText);
        }
        
        protected override void OnClicked()
        {
            // MessageNumber++;
            if (!IsOptionSelecting)
            {
                if (MessageQueue.Count == 0)
                {
                    OnSentEveryMessage?.Invoke();
                    DestroySelf();
                    return;
                }
                SetNextMessage();
            }
        }
    }

}