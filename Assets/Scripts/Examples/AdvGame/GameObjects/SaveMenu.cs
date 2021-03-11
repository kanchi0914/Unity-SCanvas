using System.Linq;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Examples.AdvGame.GameObjects
{
    public class SaveMenu : EGCanvas
    {
        public SaveMenu(Scenario scenario) : base("OptionMenu")
        {
            GameData.LoadDatas();
            var blockImage = new EGGameObject(this).SetRelativeSize(1, 1);
            blockImage.SetImageColor(Color.clear);
            var menu = new EGGameObject(this);
            menu.SetAnchorType(AnchorType.MiddleCenter)
                .SetPosition(0, 0)
                .SetRelativeSize(0.8f, 0.6f);
            var backGroundImage = new EGGameObject(menu);

            backGroundImage.SetImageColor(Color.gray, 0.6f)
                .SetAnchorType(AnchorType.MiddleCenter)
                .SetPosition(0, 0)
                .SetRelativeSize(1, 1);

            var text = new EGText(menu, "セーブする場所を選択")
                .SetAnchorType(AnchorType.TopCenter)
                .SetRelativeSize(1, 0.2f)
                .SetPosition(0, 0);

            var saveDateImagesCanvas = new EGGridLayoutView(menu, columnCount: 3, rowCount: 2)
                    .SetPaddingAndSpacing(20)
                    .SetAnchorType(AnchorType.BottomCenter)
                    .SetRelativeSize(1, 0.8f)
                    .SetPosition(0, 0)
                as EGGridLayoutView;

            foreach (var (item, index) in GameData.SaveDatas.ToList().Indexed())
            {
                new SaveDataImage(saveDateImagesCanvas, true, index, 
                    scenario.GetType().Name, scenario.CurrentScriptId, 
                    scenario.SectionNumber, item);
            }

            var closeButton = new EGButton(menu, "×")
                .SetAnchorType(AnchorType.TopRight)
                .SetPosition(20, -20)
                .SetSize(40, 40) as EGButton;
            closeButton.AddOnClick(() => { DestroySelf(); });
        }
    }
}