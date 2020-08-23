using System.Globalization;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class GUIData
    {
        public static Font GenjuGothicBold = Resources.Load<Font>("Fonts/GenJyuuGothic-Bold");
        public static EGText.TextPreset DefaultText = new EGText.TextPreset(GenjuGothicBold, color: Color.white);
        public static EGText.TextPreset TopMenuText = new EGText.TextPreset(GenjuGothicBold, color: Color.gray, fontSize:36);
    }
}