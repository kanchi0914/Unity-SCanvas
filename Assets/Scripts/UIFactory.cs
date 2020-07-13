using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HC.UI;
using Assets.Scripts.Extensions;
using static Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public static class UIFactory
    {
        public struct Resources
        {
            public Sprite standard;
            public Sprite background;
            public Sprite inputField;
            public Sprite knob;
            public Sprite checkmark;
            public Sprite dropdown;
            public Sprite mask;
            public Font font;
        }

        private const float kWidth = 160f;
        private const float kThickHeight = 30f;
        private const float kThinHeight = 20f;
        private static Vector2 s_ThickElementSize = new Vector2(kWidth, kThickHeight);
        private static Vector2 s_ThinElementSize = new Vector2(kWidth, kThinHeight);
        private static Vector2 s_ImageElementSize = new Vector2(100f, 100f);
        private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);
        private static Color s_PanelColor = new Color(1f, 1f, 1f, 0.392f);
        private static Color s_TextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);

        private const string kUILayerName = "UI";

        private const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        private const string kBackgroundSpritePath = "UI/Skin/Background.psd";
        private const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
        private const string kKnobPath = "UI/Skin/Knob.psd";
        private const string kCheckmarkPath = "UI/Skin/Checkmark.psd";
        private const string kDropdownArrowPath = "UI/Skin/DropdownArrow.psd";
        private const string kMaskPath = "UI/Skin/UIMask.psd";
        private const string kFontPath = "Arial.ttf";
        
        public static GameObject CreateBaseRect()
        {
            GameObject obj = new GameObject();
            var rect = obj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.position = new Vector2(0f, 0f);
            return obj;
        }

        public static GameObject CreatePanel()
        {
            var panelObject = CreateBaseRect();
            Image image = panelObject.AddComponent<Image>();
            image.sprite = GetStandardResources().background;
            image.type = Image.Type.Sliced;
            image.color = Color.gray;
            return panelObject;
        }

        //public static GameObject CreateVerticalLayoutView(List<SButton> sButtons)
        //{
        //    var panel = CreateBaseRect();
        //    var layout = panel.AddComponent<VerticalLayoutGroup>();
        //    layout.childForceExpandHeight = false;
        //    layout.childForceExpandWidth = false;
        //    layout.childControlHeight = false;
        //    layout.childControlWidth = false;
        //    sButtons.ForEach(sb => sb.Button.transform.SetParent(panel.transform, false));
        //    return panel;
        //}

        public static GameObject CreateText(
            GameObject parent, string _text,
            int fontSize = 36,
            ColorType colorType = ColorType.Black
            )
        {
            var textObj = CreateBaseRect();
            Text text = textObj.AddComponent<Text>();
            text.text = _text;
            text.font = StandardResources.font;
            text.fontSize = 36;
            text.color = GetColor(ColorType.Black);
            textObj.transform.SetParent(parent.transform, false);
            return textObj;
        }

        public static GameObject CreateButton(GameObject parent, string _text)
        {
            var buttonObj = CreateBaseRect();
            Resources resources = GetStandardResources();

            Image image = buttonObj.AddComponent<Image>();
            image.sprite = resources.standard;
            image.type = Image.Type.Sliced;
            image.color = s_DefaultSelectableColor;

            Button bt = buttonObj.AddComponent<Button>();
            var text = CreateText(buttonObj, _text);
            var rect = text.gameObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector3(0, 1);
            return buttonObj;
        }

        public static GameObject CreatePrefab(GameObject parent, string name)
        {
            var pre = UnityEngine.Resources.Load(name) as GameObject;
            var gameObject = GameObject.Instantiate(pre);
            return gameObject;
        }

        public static GameObject CreateGridView(List<SButton> sButtons,
            int columnSize = 0,
            float width = 400f,
            float height = 300f
            )
        {
            var panel = CreateBaseRect();
            var layout = panel.AddComponent<GridLayoutGroup>();
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            sButtons.ForEach(sb => sb.Button.transform.SetParent(panel.transform, false));
            if (columnSize > 0)
            {
                layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                layout.constraintCount = columnSize;
                layout.cellSize = new Vector2(width / columnSize, 100f);
            }
            return panel;
        }

        public delegate string MyDelegate(string id = "");
        public static Func<string, string> func;

        public static Func<Delegate> func2;

        public static string dosome2(string id)
        {
            return "";
        }

        public static void printMessage(string message)
        {
            Debug.Log(message);
        }


        public static Resources StandardResources = GetStandardResources();

        static private Resources GetStandardResources()
        {
            var s_StandardResources = new Resources();
#if UNITY_EDITOR
            s_StandardResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
                s_StandardResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
                s_StandardResources.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
                s_StandardResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>(kKnobPath);
                s_StandardResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
                s_StandardResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>(kDropdownArrowPath);
                s_StandardResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>(kMaskPath);
#else
                s_StandardResources.standard = UnityEngine.Resources.Load<GameObject>("UISprite").GetComponent<SpriteRenderer>().sprite;
                s_StandardResources.background = UnityEngine.Resources.Load<GameObject>("Background").GetComponent<SpriteRenderer>().sprite;
                s_StandardResources.inputField = UnityEngine.Resources.Load<GameObject>("InputFieldBackground").GetComponent<SpriteRenderer>().sprite;
                s_StandardResources.knob = UnityEngine.Resources.Load<GameObject>("Knob").GetComponent<SpriteRenderer>().sprite;
                s_StandardResources.checkmark = UnityEngine.Resources.Load<GameObject>("Checkmark").GetComponent<SpriteRenderer>().sprite;
                s_StandardResources.dropdown = UnityEngine.Resources.Load<GameObject>("DropdownArrow").GetComponent<SpriteRenderer>().sprite;
                s_StandardResources.mask = UnityEngine.Resources.Load<GameObject>("UIMask").GetComponent<SpriteRenderer>().sprite;
#endif
                s_StandardResources.font = UnityEngine.Resources.GetBuiltinResource<Font>(kFontPath);

            return s_StandardResources;
        }

    }




}
