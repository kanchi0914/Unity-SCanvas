using System;
using System.Collections.Generic;
using System.Linq;
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
        private static Camera mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
        
        public static Color DefaultColor = new Color(1f, 1f, 1f, 0.392f);
        
        public static Color DefaultTextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        //
        //
        // public static EGGameObject CreateEGGameObjectFromExistingGameObject()
        // {
        //     
        // }
        
        public static int DefaultFontSize
        {
            get
            {
                return (int) (Screen.width * 0.025f);
            }
        }
        
        public static void TryCreateEventSystem()
        {
            var eventSystemComponent = UnityEngine.Object.FindObjectOfType<EventSystem>();
            if (eventSystemComponent == null)
            {
                var eventSystem = new GameObject( "EventSystem");
                eventSystemComponent = eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
        }


    }
}