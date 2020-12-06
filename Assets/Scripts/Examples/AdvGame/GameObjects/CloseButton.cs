using EGUI.Base;
using EGUI.GameObjects;

namespace Assets.Scripts.Examples.AdvGame.GameObjects
{
    public class CloseButton : EGButton
    {
        public CloseButton(EGGameObject parent): base(parent)
        {
            SetAnchorType(AnchorType.TopRight)
                .SetPosition(20,20)
                .SetSize(40, 40);
            TextObject.SetText("×");
            SetOnOnClick(() =>
            {
                parent.DestroySelf();
            });
        }
    }
}