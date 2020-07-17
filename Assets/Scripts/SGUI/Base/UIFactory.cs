using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using HC.UI;
using SGUI.SGameObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SGUI.Base
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

        // private const float kWidth = 160f;
        // private const float kThickHeight = 30f;
        // private const float kThinHeight = 20f;
        // private static Vector2 s_ThickElementSize = new Vector2 (kWidth, kThickHeight);
        // private static Vector2 s_ThinElementSize = new Vector2 (kWidth, kThinHeight);
        // private static Vector2 s_ImageElementSize = new Vector2 (100f, 100f);
        // private static Color s_DefaultSelectableColor = new Color (1f, 1f, 1f, 1f);
        // private static Color s_PanelColor = new Color (1f, 1f, 1f, 0.392f);
        // private static Color s_TextColor = new Color (50f / 255f, 50f / 255f, 50f / 255f, 1f);

        private const string kUILayerName = "UI";

        private const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        private const string kBackgroundSpritePath = "UI/Skin/Background.psd";
        private const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
        private const string kKnobPath = "UI/Skin/Knob.psd";
        private const string kCheckmarkPath = "UI/Skin/Checkmark.psd";
        private const string kDropdownArrowPath = "UI/Skin/DropdownArrow.psd";
        private const string kMaskPath = "UI/Skin/UIMask.psd";
        private const string kFontPath = "Arial.ttf";

        public static GameObject CreateBaseRect (GameObject parent, string name)
        {
            GameObject obj = new GameObject (name);
            if (parent != null) obj.transform.SetParent (parent.transform, false);
            var rect = obj.AddComponent<RectTransform> ();
            SetTopLeftAnchor (rect);
            //rect.sizeDelta = new Vector2 (100, 100);
            // rect.localPosition = new Vector2(0, 0);
            return obj;
        }

        public static GameObject CreateCanvas (string name)
        {
            var gameObject = CreateBaseRect (null, name);

            var canvas = gameObject.AddComponent<Canvas> ();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
            gameObject.AddComponent<CanvasScaler> ();
            gameObject.AddComponent<GraphicRaycaster> ();
            var image = gameObject.AddComponent<Image> ();
            image.sprite = GetStandardResources ().background;
            image.type = Image.Type.Sliced;
            image.color = new Color (0, 0, 0, 0f);
            return gameObject;
        }

        public static GameObject CreatePanel (GameObject parent, string name)
        {
            var gameObject = CreateBaseRect (parent, name);
            Image image = gameObject.AddComponent<Image> ();
            image.sprite = GetStandardResources ().background;
            image.type = Image.Type.Sliced;
            image.color = Color.gray;
            //SetFullStretchAnchor(gameObject.GetComponent<RectTransform>());
            return gameObject;
        }

        public static GameObject CreateImage (GameObject parent, string name)
        {
            var gameObject = CreateBaseRect (parent, name);
            var rect = gameObject.GetComponent<RectTransform> ();
            rect.sizeDelta = new Vector2 (100, 100);
            Image image = gameObject.AddComponent<Image> ();
            image.sprite = GetStandardResources ().background;
            image.type = Image.Type.Sliced;
            image.color = Color.white;
            //SetMiddleCenterAnchor (gameObject.GetComponent<RectTransform> ());
            return gameObject;
        }

        public static void SetTopLeftAnchor (RectTransform rectTransform)
        {
            rectTransform.anchorMin = new Vector2 (0f, 1f);
            rectTransform.anchorMax = new Vector2 (0f, 1f);
            rectTransform.pivot = new Vector2 (0f, 1f);
            //rectTransform.position = new Vector2(0f, 0f);
        }

        public static void SetMiddleCenterAnchor (RectTransform rectTransform)
        {
            rectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2 (0.5f, 0.5f);
            rectTransform.pivot = new Vector2 (0.5f, 0.5f);
        }

        public static void SetFullStretchAnchor (RectTransform rectTransform)
        {
            rectTransform.anchorMin = new Vector2 (0, 0);
            rectTransform.anchorMax = new Vector2 (1, 1);
            rectTransform.pivot = new Vector3 (0.5f, 0.5f);
            rectTransform.offsetMax = new Vector2 (0, 0);
            rectTransform.offsetMin = new Vector2 (0, 0);
        }

        public static GameObject CreateText (
            GameObject parent,
            string name,
            string _text = "",
            int fontSize = 0,
            ColorType colorType = ColorType.Black,
            TextAnchor textAnchor = TextAnchor.MiddleCenter
        )
        {
            var gameObject = CreateBaseRect (parent, name);
            Text text = gameObject.AddComponent<Text> ();
            text.text = _text;
            if (fontSize == 0) text.fontSize = Utils.DefaultFontSize;
            else text.fontSize = fontSize;
            text.font = StandardResources.font;
            text.color = Utils.GetColor (colorType, 0.8f);
            text.alignment = textAnchor;

            var rect = gameObject.GetComponent<RectTransform> ();
            SetFullStretchAnchor (rect);
            rect.sizeDelta = new Vector2 (0, 0);

            return gameObject;
        }

        public static GameObject CreateButton (GameObject parent, string name, string _text, ColorType color)
        {
            var gameObject = CreateBaseRect (parent, name);
            Resources resources = GetStandardResources ();

            Image image = gameObject.AddComponent<Image> ();
            image.sprite = resources.standard;
            image.type = Image.Type.Sliced;
            image.color = Utils.GetColor (color, 1);

            Button bt = gameObject.AddComponent<Button> ();

            // var text = CreateText (gameObject, name, _text);
            // var rect = text.GetComponent<RectTransform> ();
            // SetFullStretchAnchor (rect);

            return gameObject;
        }

        public static GameObject CreatePrefab (GameObject parent, string name)
        {
            var pre = UnityEngine.Resources.Load (name) as GameObject;
            var gameObject = GameObject.Instantiate (pre);
            return gameObject;
        }

        public static GameObject CreateGridLayoutView (
            GameObject parent,
            string name,
            int columnSize = 0,
            float width = 400f,
            float height = 300f
        )
        {
            var gameObject = CreateBaseRect (parent, name);
            var layout = gameObject.AddComponent<GridLayoutGroup> ();
            gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (width, height);
            if (columnSize > 0)
            {
                layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                layout.constraintCount = columnSize;
                layout.cellSize = new Vector2 (width / columnSize, 100f);
            }
            return gameObject;
        }

        public static GameObject CreateDropDown (
            GameObject parent,
            string name
        )
        {
            var gameObject = UICreator.CreateDropdown (parent, name);
            var rect = gameObject.GetComponent<RectTransform>();
            SetTopLeftAnchor(rect);
            return gameObject;

        }

        public static GameObject CreateVerticalLayoutView (
            GameObject parent,
            string name,
            int rowSize = 10,
            float widthRatio = 1.0f,
            TextAnchor textAnchor = TextAnchor.UpperLeft
        )
        {
            var gameObject = CreateBaseRect (parent, name);
            var layout = gameObject.AddComponent<VerticalLayoutGroup> ();
            return gameObject;
        }

        public static Resources StandardResources = GetStandardResources ();

        static private Resources GetStandardResources ()
        {
            var s_StandardResources = new Resources ();
#if UNITY_EDITOR
            s_StandardResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite> (kStandardSpritePath);
            s_StandardResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite> (kBackgroundSpritePath);
            s_StandardResources.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite> (kInputFieldBackgroundPath);
            s_StandardResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite> (kKnobPath);
            s_StandardResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite> (kCheckmarkPath);
            s_StandardResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite> (kDropdownArrowPath);
            s_StandardResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite> (kMaskPath);
#else
            s_StandardResources.standard = UnityEngine.Resources.Load<GameObject> ("UISprite").GetComponent<SpriteRenderer> ().sprite;
            s_StandardResources.background = UnityEngine.Resources.Load<GameObject> ("Background").GetComponent<SpriteRenderer> ().sprite;
            s_StandardResources.inputField = UnityEngine.Resources.Load<GameObject> ("InputFieldBackground").GetComponent<SpriteRenderer> ().sprite;
            s_StandardResources.knob = UnityEngine.Resources.Load<GameObject> ("Knob").GetComponent<SpriteRenderer> ().sprite;
            s_StandardResources.checkmark = UnityEngine.Resources.Load<GameObject> ("Checkmark").GetComponent<SpriteRenderer> ().sprite;
            s_StandardResources.dropdown = UnityEngine.Resources.Load<GameObject> ("DropdownArrow").GetComponent<SpriteRenderer> ().sprite;
            s_StandardResources.mask = UnityEngine.Resources.Load<GameObject> ("UIMask").GetComponent<SpriteRenderer> ().sprite;
#endif
            s_StandardResources.font = UnityEngine.Resources.GetBuiltinResource<Font> (kFontPath);

            return s_StandardResources;
        }

    }

}