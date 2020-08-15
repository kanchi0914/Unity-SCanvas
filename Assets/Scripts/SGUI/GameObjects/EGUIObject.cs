using EGUI.Base;

namespace EGUI.GameObjects
{
    public class EGUIObject : EGGameObject
    {
        public EGUIObject
        (
            EGGameObject parent,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "SSubCanvas"
        ) : base
        (
            parent,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            name,
            () => UIFactory.CreateBaseRect(parent?.GameObject, name)
        )
        {
        }
    }
}