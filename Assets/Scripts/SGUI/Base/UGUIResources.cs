using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.SGUI.Base
{
    public static class UGUIResources
    {
        //public struct Resources
        //{
        //    public Sprite standard;
        //    public Sprite background;
        //    public Sprite inputField;
        //    public Sprite knob;
        //    public Sprite checkmark;
        //    public Sprite dropdown;
        //    public Sprite mask;
        //    public Font font;
        //}

        static UGUIResources()
        {
#if UNITY_EDITOR
            UISprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
            Background = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
            InputField = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
            Knob = AssetDatabase.GetBuiltinExtraResource<Sprite>(kKnobPath);
            Checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
            Dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>(kDropdownArrowPath);
            Mask = AssetDatabase.GetBuiltinExtraResource<Sprite>(kMaskPath);
#else
            Standard = UnityEngine.Resources.Load<GameObject> ("UISprite").GetComponent<SpriteRenderer> ().sprite;
            Background = UnityEngine.Resources.Load<GameObject> ("Background").GetComponent<SpriteRenderer> ().sprite;
            InputField = UnityEngine.Resources.Load<GameObject> ("InputFieldBackground").GetComponent<SpriteRenderer> ().sprite;
            Knob = UnityEngine.Resources.Load<GameObject> ("Knob").GetComponent<SpriteRenderer> ().sprite;
            Checkmark = UnityEngine.Resources.Load<GameObject> ("Checkmark").GetComponent<SpriteRenderer> ().sprite;
            Dropdown = UnityEngine.Resources.Load<GameObject> ("DropdownArrow").GetComponent<SpriteRenderer> ().sprite;
            Mask = UnityEngine.Resources.Load<GameObject> ("UIMask").GetComponent<SpriteRenderer> ().sprite;
#endif
            Font = UnityEngine.Resources.GetBuiltinResource<Font>(kFontPath);
        }

        public static Sprite UISprite { get; private set; }
        public static Sprite Background { get; private set; }
        public static Sprite InputField { get; private set; }
        public static Sprite Knob { get; private set; }
        public static Sprite Checkmark { get; private set; }
        public static Sprite Dropdown { get; private set; }
        public static Sprite Mask { get; private set; }
        public static Font Font { get; private set; }

        private const string kUILayerName = "UI";

        private const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        private const string kBackgroundSpritePath = "UI/Skin/Background.psd";
        private const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
        private const string kKnobPath = "UI/Skin/Knob.psd";
        private const string kCheckmarkPath = "UI/Skin/Checkmark.psd";
        private const string kDropdownArrowPath = "UI/Skin/DropdownArrow.psd";
        private const string kMaskPath = "UI/Skin/UIMask.psd";
        private const string kFontPath = "Arial.ttf";

    }
}
