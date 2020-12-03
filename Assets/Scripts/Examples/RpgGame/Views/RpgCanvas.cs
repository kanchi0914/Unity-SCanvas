using Assets.Scripts.Extensions;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.RpgGame.Views
{
    public class RpgCanvas : EGCanvas
    {
        public RpgCanvas(string name = "RpgCanvas") : base(name)
        {
            var scaler = gameObject.GetOrAddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(640, 480);
        }
    }
}