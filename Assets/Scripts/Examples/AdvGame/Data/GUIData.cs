using System.Globalization;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class GUIData
    {
        public static Font GenjuGothicBold = Resources.Load<Font>("Fonts/GenJyuuGothic-Bold");
        public static EGText.TextPreset DefaultText = new EGText.TextPreset(GenjuGothicBold, color: Color.white);
        public static EGText.TextPreset MessageWindowText = new EGText.TextPreset(GenjuGothicBold, color: Color.white, fontSize:28);
        public static EGText.TextPreset OptionText = new EGText.TextPreset(GenjuGothicBold, color: Color.white, 
            resizeTextForBestFit:true, resizeTextMaxSize:28, resizeTextMinSize:10);
        public static EGText.TextPreset TopMenuText = new EGText.TextPreset(GenjuGothicBold, color: Color.gray, fontSize:36);
    }
}