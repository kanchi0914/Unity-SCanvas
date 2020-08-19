using EGUI.GameObjects;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Utils
    {
        public static void SetBackgroundImage(EGImage image, string imageFilePath)
        {
            image.SetImageByFilePath(imageFilePath);
            image.ImageComponent.SetNativeSize();
            image.SetMiddleCenterAnchor().SetLocalPos(0, 0);
            var asfitter = image.GameObject.AddComponent<AspectRatioFitter>();
            asfitter.aspectRatio = image.RectSize.x / image.RectSize.y;
            asfitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        }

    }
}