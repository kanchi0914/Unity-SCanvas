using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine;

namespace EGUI.Base
{
    public class Utils
    {
        private static Camera mainCamera = Camera.main;

        public static Color DefaultColor = new Color(1f, 1f, 1f, 0.392f);
        public static Color DefaultTextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        public static int DefaultFontSize
        {
            get { return (int) (Screen.width * 0.025f); }
        }

        public static void TryCreateEventSystem()
        {
            var eventSystemComponent = UnityEngine.Object.FindObjectOfType<EventSystem>();
            if (eventSystemComponent == null)
            {
                var eventSystem = new GameObject("EventSystem");
                eventSystemComponent = eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
        }

        // https://baba-s.hatenablog.com/entry/2014/09/08/114615
        /// <summary>
        /// 指定された GameObject を複製して返します
        /// </summary>
        public static GameObject Clone(GameObject go)
        {
            var clone = GameObject.Instantiate(go) as GameObject;
            clone.transform.parent = go.transform.parent;
            clone.transform.localPosition = go.transform.localPosition;
            clone.transform.localScale = go.transform.localScale;
            return clone;
        }
        
    }
}