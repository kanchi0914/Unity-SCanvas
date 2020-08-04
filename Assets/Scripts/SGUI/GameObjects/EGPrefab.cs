using EGUI.Base;

namespace EGUI.GameObjects
{
    public class EGPrefab : EGGameObject
    {
        public EGPrefab(
            EGGameObject parent,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "SPrefab"
        ) : base(parent,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            name,
            () => UIFactory.CreatePrefab(parent.GameObject, name)
        )
        {
        }
    }
}