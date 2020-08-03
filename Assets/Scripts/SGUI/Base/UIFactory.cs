﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using HC.UI;
using UniRx;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.Base
{
    public static class UIFactory
    {

        public static GameObject CreateBaseRect (GameObject parent, string name)
        {
            GameObject obj = new GameObject (name);
            if (parent != null) obj.transform.SetParent (parent.transform, false);
            var rect = obj.AddComponent<RectTransform> ();
            rect.SetTopLeftAnchor();
            obj.transform.SetLocalPos (0, 0);
            rect.sizeDelta = new Vector2 (100, 100);
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
            return gameObject;
        }

        public static GameObject CreatePanel (GameObject parent, string name)
        {
            var gameObject = CreateBaseRect (parent, name);
            Image image = gameObject.AddComponent<Image> ();
            image.sprite = UGUIResources.Background;
            image.type = Image.Type.Sliced;
            image.color = Utils.GetColor (ColorType.White, 0.2f);
            return gameObject;
        }

        public static GameObject CreateImage (GameObject parent, string name, string resourceImagePath = null)
        {
            var gameObject = CreateBaseRect (parent, name);
            var rect = gameObject.GetComponent<RectTransform> ();
            rect.sizeDelta = new Vector2 (100, 100);
            Image image = gameObject.AddComponent<Image> ();
            if (resourceImagePath == null)
            {
                image.sprite = UGUIResources.Background;
            }
            else
            {
                var source = Resources.Load<Sprite> (resourceImagePath);
                if (!source) Debug.Log ($"Sprite resource {resourceImagePath} is not found");
                else image.sprite = source;
            }
            image.type = Image.Type.Sliced;
            image.color = Color.white;
            return gameObject;
        }

        //public static void SetTopLeftAnchor(RectTransform rectTransform)
        //{
        //    rectTransform.anchorMin = new Vector2(0f, 1f);
        //    rectTransform.anchorMax = new Vector2(0f, 1f);
        //    rectTransform.pivot = new Vector2(0f, 1f);
        //    rectTransform.localPosition = new Vector2(0f, 0f);
        //    rectTransform.offsetMax = new Vector2(0, 0);
        //    rectTransform.offsetMin = new Vector2(0, 0);
        //}

        //public static void SetMiddleCenterAnchor(RectTransform rectTransform)
        //{
        //    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        //    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        //    rectTransform.pivot = new Vector2(0.5f, 0.5f);
        //    rectTransform.offsetMax = new Vector2(0, 0);
        //    rectTransform.offsetMin = new Vector2(0, 0);
        //}

        //public static void SetHorizontalStretchAnchor(RectTransform rectTransform)
        //{
        //    rectTransform.anchorMin = new Vector2(0.0f, 0.25f);
        //    rectTransform.anchorMax = new Vector2(1f, 0.75f);
        //    rectTransform.pivot = new Vector2(0.5f, 0.5f);
        //    rectTransform.offsetMax = new Vector2(0, 0);
        //    rectTransform.offsetMin = new Vector2(0, 0);
        //}

        public static void SetStretchLeftAnchor(RectTransform rectTransform)
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
        }

        //public static void SetMiddleLeftAnchor(RectTransform rectTransform)
        //{
        //    rectTransform.anchorMin = new Vector2(0f, 0.5f);
        //    rectTransform.anchorMax = new Vector2(0f, 0.5f);
        //    rectTransform.pivot = new Vector2(0f, 0.5f);
        //    rectTransform.offsetMax = new Vector2(0, 0);
        //    rectTransform.offsetMin = new Vector2(0, 0);
        //}

        //public static void SetFullStretchAnchor(RectTransform rectTransform)
        //{
        //    rectTransform.anchorMin = new Vector2(0, 0);
        //    rectTransform.anchorMax = new Vector2(1, 1);
        //    rectTransform.pivot = new Vector3(0.5f, 0.5f);
        //    rectTransform.offsetMax = new Vector2(0, 0);
        //    rectTransform.offsetMin = new Vector2(0, 0);
        //}

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
            text.font = UGUIResources.Font;
            text.color = Utils.GetColor (colorType, 1);
            text.alignment = textAnchor;
            var rect = gameObject.GetComponent<RectTransform> ();
            return gameObject;
        }

        public static GameObject CreateTMProText (
            GameObject parent,
            string name = "Text",
            int fontSize = 0,
            ColorType colorType = ColorType.Black,
            TextAlignmentOptions textAlignment = TextAlignmentOptions.TopLeft
        )
        {
            GameObject gameObject = CreateBaseRect (parent, name);
            var text = gameObject.AddComponent<TextMeshProUGUI> ();
            text.fontStyle = FontStyles.Italic | FontStyles.Bold;
            text.color = Utils.GetColor (colorType, 1);
            text.alignment = textAlignment;
            if (fontSize == 0) text.fontSize = Utils.DefaultFontSize;
            return gameObject;
        }

        public static GameObject CreateButton (GameObject parent, string name, string _text, ColorType color)
        {
            var gameObject = CreateBaseRect (parent, name);
            Image image = gameObject.AddComponent<Image> ();
            image.sprite = UGUIResources.Background;
            image.type = Image.Type.Sliced;
            image.color = Utils.GetColor (color, 1);
            Button bt = gameObject.AddComponent<Button> ();
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
            string name
            //int columnSize = 0,
            //float width = 400f,
            //float height = 300f
        )
        {
            var gameObject = CreateBaseRect (parent, name);
            var layout = gameObject.AddComponent<GridLayoutGroup> ();
            //gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (width, height);
            //if (columnSize > 0)
            //{
            //    layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            //    layout.constraintCount = columnSize;
            //    layout.cellSize = new Vector2 (width / columnSize, 100f);
            //}
            return gameObject;
        }

        public static GameObject CreateDropDown (
            GameObject parent,
            string name
        )
        {
            var gameObject = UICreator.CreateDropdown (parent, name);
            return gameObject;
        }

        public static GameObject CreateScrollBar (
            GameObject parent,
            string name
        )
        {
            return UICreator.CreateScrollbar (parent, name);
        }


        public static GameObject CreateVerticalScrollView (
            GameObject parent,
            string name
        )
        {
            return UICreator.CreateScrollView (parent, name, UICreator.LayoutGroupType.Vertical);
        }

        public static GameObject CreateHorizontalScrollView(
            GameObject parent,
            string name
)
        {
            return UICreator.CreateScrollView(parent, name, UICreator.LayoutGroupType.Horizontal);
        }

        public static GameObject CreateVerticalGridLayoutScrollView (
            GameObject parent,
            string name
        )
        {
            var obj = UICreator.CreateScrollView(parent, name, UICreator.LayoutGroupType.VerticalGrid);
            obj.GetComponent<RectTransform>().SetTopLeftAnchor();
            return obj;
        }

        public static GameObject CreateHorizontalGridLayoutScrollView (
            GameObject parent,
            string name
        )
        {
            return UICreator.CreateScrollView (parent, name, UICreator.LayoutGroupType.HorizontalGrid);
        }

        public static GameObject CreateVerticalLayoutView (
            GameObject parent,
            string name,
            TextAnchor textAnchor = TextAnchor.UpperLeft
        )
        {
            var gameObject = CreateBaseRect (parent, name);
            var layout = gameObject.AddComponent<VerticalLayoutGroup> ();
            layout.childControlHeight = false;
            layout.childControlWidth = true;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = true;
            layout.childScaleHeight = false;
            layout.childScaleWidth = false;
            return gameObject;
        }

        public static GameObject CreateHotizontalLayoutView (
            GameObject parent,
            string name
        )
        {
            var gameObject = CreateBaseRect (parent, name);
            var layout = gameObject.AddComponent<HorizontalLayoutGroup> ();
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.childForceExpandHeight = true;
            layout.childForceExpandWidth = false;
            layout.childScaleHeight = false;
            layout.childScaleWidth = false;
            return gameObject;
        }
    }

}