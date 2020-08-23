using EGUI.GameObjects;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class CloseButton : EGButton
    {
        public CloseButton(EGGameObject parent): base(parent)
        {
            SetTopRightAnchor()
                .SetLocalPos(20,-20)
                .SetRectSize(40, 40);
            TextObject.SetText("×");
            SetOnOnClick(() =>
            {
                parent.DestroySelf();
            });
        }
    }
}