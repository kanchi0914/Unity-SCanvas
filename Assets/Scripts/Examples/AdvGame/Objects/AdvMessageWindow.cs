using System.Collections.Generic;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class AdvMessageWindow : EgMessageWindow
    {
        public int MessageNumber = 0;
        public List<Section> sendSections = new List<Section>();
        
        public bool IsOptionSelecting = false;

        public AdvMessageWindow(EGGameObject parentCanvas) : base(parentCanvas)
        {
            SetBottomCenterAnchor()
                .SetLocalPosByRatio(0, 0.1f)
                .SetRectSizeByRatio(0.8f, 0.3f);

            this.MessageText.SetLocalPosByRatio(0, -0.1f);

            var menus = new EgHorizontalLayoutView(
                    this,
                    isAutoSizingHeight: true,
                    isAutoSizingWidth: true
                )
                .SetTopRightAnchor()
                .SetLocalPosByRatio(0, 0)
                .SetRectSizeByRatio(0.4f, 0.1f);
            var logText = new EGText(menus, "バックログ", isAutoSizing:true);
            var saveText = new EGText(menus, "セーブ", isAutoSizing:true);
            var loadText = new EGText(menus, "ロード", isAutoSizing:true);
            var optionText = new EGText(menus, "オプション", isAutoSizing:true);
            logText.AddOnClick(() => new LogWindow(SentSections));
            saveText.AddOnClick(() => new SaveMenu("", MessageNumber));
            loadText.AddOnClick(() => new LoadMenu());
            optionText.AddOnClick(() => new OptionMenu());
            menus.ChildrenObjects.ForEach(g =>
            {
                var text = g as EGText;
                text.SetTextConfig(font: GameData.FontsMap["genju"], color: Color.white);
;            });
            AddOnClick(() =>
            {
                MessageNumber++;
            });
            MessageText.SetTextConfig(font: GameData.FontsMap["genju"], color: Color.white, fontSize:28);
            TalkerText.SetTextConfig(font: GameData.FontsMap["genju"], color: Color.white);
            
        }
        
        protected override void OnClicked()
        {
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