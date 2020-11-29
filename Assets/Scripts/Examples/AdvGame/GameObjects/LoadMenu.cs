using System;
using System.Linq;

using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using static Assets.Scripts.Examples.AdvGame.Utils;
using UnityEngine.UIElements;

namespace Assets.Scripts.Examples.AdvGame.GameObjects
{
    public class LoadMenu : EGCanvas
    {
        public LoadMenu() : base("LoadMenu")
        {
            GameData.LoadDatas();
            var blockImage = new EGGameObject(this).SetRelativeSize(1, 1);
            blockImage.SetImageColor(Color.clear);
            var menu = new EGGameObject(this);
            menu.SetMiddleCenterAnchor()
                .SetPosition(0, 0)
                .SetRelativeSize(0.8f, 0.6f);
            var backGroundImage = new EGGameObject(menu);

            backGroundImage.SetImageColor(Color.gray, 0.6f)
                .SetMiddleCenterAnchor()
                .SetPosition(0, 0)
                .SetRelativeSize(1, 1);

            var text = new EGText(menu, "ロードするデータを選択")
                .SetTopCenterAnchor()
                .SetRelativeSize(1, 0.2f)
                .SetPosition(0, 0);

            var saveDateImagesCanvas = new EGGridLayoutView(menu, columnCount: 3, rowCount: 2)
                .SetPaddingAndSpacing(20)
                .SetBottomCenterAnchor()
                .SetRelativeSize(1, 0.8f)
                .SetPosition(0, 0);

            foreach (var (item, index) in GameData.SaveDatas.ToList().Indexed())
            {
                new SaveDataImage(saveDateImagesCanvas, false, index, null, null, 0, item);
            }

            var closeButton = new EGButton(menu, "×")
                .SetTopRightAnchor()
                .SetPosition(20, -20)
                .SetSize(40, 40) as EGButton;
            closeButton.SetOnOnClick(() => { DestroySelf(); });
        }
    }
}