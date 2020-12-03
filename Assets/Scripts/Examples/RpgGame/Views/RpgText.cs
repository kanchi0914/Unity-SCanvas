using EGUI.GameObjects;
using UnityEngine;

namespace Examples.RpgGame.Views
{
    public class RpgText : EGText
    {
        public static Font PixelMplus = Resources.Load<Font>("Fonts/PixelMplus10-Regular");
        public static EGText.TextPreset DefaultText = new EGText.TextPreset(PixelMplus, color: Color.white,
            fontSize: 20, alignment: TextAnchor.MiddleCenter, resizeTextForBestFit:false);
        
        public RpgText(EGGameObject parent) : base(parent)
        {
            SetTextPreset(DefaultText);
        }
    }
}