using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class OptionMenu : EGCanvas
    {
        public OptionMenu() : base("OptionMenu")
        {
            var blockImage = new EGGameObject(this)
                .SetRectSizeByRatio(1, 1)
                .SetImageColor(Color.clear);
            var menu = new EGGameObject(this);
            menu.SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSizeByRatio(0.6f, 0.6f);
            var backGroundImage = new EGGameObject(menu)
                .SetImageColor(Color.gray, 0.2f)
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