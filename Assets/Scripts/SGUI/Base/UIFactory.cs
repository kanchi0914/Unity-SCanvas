// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Assets.Scripts.Extensions;
// using Assets.Scripts.SGUI.Base;
// using HC.UI;
// using UniRx;
// using TMPro;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace EGUI.Base
// {
//     public static class UIFactory
//     {
//
//         public static GameObject CreateBaseRect (GameObject parent, string name)
//         {
//             GameObject obj = new GameObject (name);
//             if (parent != null) obj.transform.SetParent (parent.transform, false);
//             var rect = obj.AddComponent<RectTransform> ();
//             rect.SetTopLeftAnchor();
//             obj.transform.SetLocalPos (0, 0);
//             rect.sizeDelta = new Vector2 (100, 100);
//             return obj;
//         }
//
//         public static GameObject CreateCanvas (string name)
//         {
//             var gameObject = CreateBaseRect (null, name);
//             var canvas = gameObject.AddComponent<Canvas> ();
//             canvas.renderMode = RenderMode.ScreenSpaceCamera;
//             var mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
//             canvas.worldCamera = mainCamera;
//             gameObject.AddComponent<CanvasScaler> ();
//             gameObject.AddComponent<GraphicRaycaster> ();
//             return gameObject;
//         }
//
//         // public static GameObject CreateTMProText (
//         //     GameObject parent,
//         //     string name = "Text",
//         //     int fontSize = 0,
//         //     ColorType colorType = ColorType.Black,
//         //     TextAlignmentOptions textAlignment = TextAlignmentOptions.TopLeft
//         // )
//         // {
//         //     GameObject gameObject = CreateBaseRect (parent, name);
//         //     var text = gameObject.AddComponent<TextMeshProUGUI> ();
//         //     text.fontStyle = FontStyles.Italic | FontStyles.Bold;
//         //     text.color = Utils.GetColor (colorType, 1);
//         //     text.alignment = textAlignment;
//         //     if (fontSize == 0) text.fontSize = Utils.DefaultFontSize;
//         //     return gameObject;
//         // }
//
//     }
//
// }