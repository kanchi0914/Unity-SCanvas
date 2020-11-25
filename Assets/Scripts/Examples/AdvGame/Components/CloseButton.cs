using EGUI.GameObjects;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class CloseButton : EGButton
    {
        /// <summary>
        /// 閉じるボタン
        /// 親オブジェクトの右上に配置され、クリックで親オブジェクトを破棄
        /// </summary>
        /// <param name="parent"></param>
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