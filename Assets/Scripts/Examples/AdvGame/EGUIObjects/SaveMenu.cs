using System.Linq;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class SaveMenu : EGCanvas
    {
        public SaveMenu(string sceneId, int messageNumber) : base("OptionMenu")
        {
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

            var text = new EGText(menu, "セーブする場所を選択")
                .SetTopCenterAnchor()
                .SetRectSizeByRatio(1, 0.2f)
                .SetLocalPos(0, 0);

            var saveDateImagesCanvas = new EGGridLayoutView(menu, columnCount: 3, rowCount: 2)
                    .SetPaddingAndSpacing(20)
                    .SetBottomCenterAnchor()
                    .SetRectSizeByRatio(1, 0.8f)
                    .SetLocalPos(0, 0)
                as EGGridLayoutView;

            foreach (var (item, index) in GameData.SaveSatas.ToList().Indexed())
            {
                if (item != null)
                {
                    new DataImage(saveDateImagesCanvas, true, index, sceneId, messageNumber, item.DataImagePath);
                }
                else
                {
                    new DataImage(saveDateImagesCanvas, true, index, sceneId, messageNumber);
                }
            }

            var closeButton = new EGButton(menu, "×")
                .SetTopRightAnchor()
                .SetLocalPos(20, -20)
                .SetRectSize(40, 40) as EGButton;
            closeButton.SetOnOnClick(() => { DestroySelf(); });
        }
    }
}