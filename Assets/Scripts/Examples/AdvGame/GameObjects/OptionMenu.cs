using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Examples.AdvGame.GameObjects
{
    public class OptionMenu : EGCanvas
    {
        public OptionMenu() : base("OptionMenu")
        {
            var blockImage = new EGGameObject(this)
                .SetRelativeSize(1, 1)
                .SetImageColor(Color.clear);
            var menu = new EGGameObject(this);
            menu.SetAnchorType(AnchorType.MiddleCenter)
                .SetPosition(0, 0)
                .SetRelativeSize(0.6f, 0.6f);
            var backGroundImage = new EGGameObject(menu)
                .SetImageColor(Color.gray, 0.2f)
                .SetAnchorType(AnchorType.MiddleCenter)
                .SetPosition(0,0)
                .SetRelativeSize(1, 1);
            var closeButton = new EGButton(menu, "×")
                .SetAnchorType(AnchorType.TopRight)
                .SetPosition(20,-20)
                .SetSize(40, 40) as EGButton;
            closeButton.SetOnOnClick(() =>
            {
                DestroySelf();
            });
        }
        
    }
}