using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class OptionMenu : EGCanvas
    {
        public OptionMenu() : base("OptionMenu")
        {
            var blockImage = new EGImage(this).SetRectSizeByRatio(1, 1);
            blockImage.SetImageColor(Color.clear);
            var menu = new EGUIObject(this);
            menu.SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSizeByRatio(0.6f, 0.6f);
            var backGroundImage = new EGImage(menu) as EGImage;
            backGroundImage.SetColor(Color.gray, 0.2f)
                .SetMiddleCenterAnchor()
                .SetLocalPos(0,0)
                .SetRectSizeByRatio(1, 1);
            var closeButton = new EGButton(menu, "×")
                .SetTopRightAnchor()
                .SetLocalPos(20,-20)
                .SetRectSize(40, 40) as EGButton;
            closeButton.SetOnOnClick(() =>
            {
                DestroySelf();
            });
        }
        
    }
}