using System;
using System.Linq;

using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using static Assets.Scripts.Examples.AdvGame.Utils;
using UnityEngine.UIElements;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class LoadMenu : EGCanvas
    {
        public LoadMenu() : base("LoadMenu")
        {
            GameData.ReadDatasFromDisc();
            var blockImage = new EGGameObject(this).SetRectSizeByRatio(1, 1);
            blockImage.SetImageColor(Color.clear);
            var menu = new EGGameObject(this);
            menu.SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSizeByRatio(0.8f, 0.6f);
            var backGroundImage = new EGGameObject(menu);

            backGroundImage.SetImageColor(Color.gray, 0.6f)
                .SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSizeByRatio(1, 1);

            var text = new EGText(menu, "ロードするデータを選択")
                .SetTextPreset(GUIData.DefaultText)
                .SetTopCenterAnchor()
                .SetRectSizeByRatio(1, 0.2f)
                .SetLocalPos(0, 0);

            var saveDateImagesCanvas = new EGGridLayoutView(menu, columnCount: 3, rowCount: 2)
                .SetPaddingAndSpacing(20)
                .SetBottomCenterAnchor()
                .SetRectSizeByRatio(1, 0.8f)
                .SetLocalPos(0, 0);

            foreach (var (item, index) in GameData.SaveDatas.ToList().Indexed())
            {
                new SaveDataImage(saveDateImagesCanvas, false, index, null, null, 0, item);
            }

            var closeButton = new EGButton(menu, "×")
                .SetTopRightAnchor()
                .SetLocalPos(20, -20)
                .SetRectSize(40, 40) as EGButton;
            closeButton.SetOnOnClick(() => { DestroySelf(); });
        }
    }
}