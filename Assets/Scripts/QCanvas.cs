using Assets.Scripts.Extensions;
using Assets.Scripts.SCanvases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public class QCanvas : SGameObject
    {
        //public GameObject GameObject
        //{
        //    get
        //    {
        //        return gameObject;
        //    }
        //    set
        //    {
        //        gameObject = value;
        //    }
        //}
        //public GameObject gameObject;
        
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        protected Canvas canvas;

        protected List<GameObject> gameObjects = new List<GameObject>();

        public List<SubCanvas> SubCanvases { get; set; } = new List<SubCanvas>();

        public Vector2 Position { get; set; }
        public Vector2 LeftTopPos { get; set; }

        public RectTransform Rect { get; set; }

        //UIFactory2 uIFactory2 = new UIFactory2();

        public QCanvas()
        {
            if (!(this is SubCanvas))
            {
                Debug.Log(this.GetType().Name);
                init();
                if (CanvasStack.Stack.Count == 0)
                {
                    CanvasStack.Push(this);
                }
            }

        }

        private void init()
        {
            gameObject = new GameObject();
            gameObject.name = $"{this.GetType().Name}";
            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = mainCamera;
            gameObject.AddComponent<CanvasScaler>();
            gameObject.AddComponent<GraphicRaycaster>();
        }


        public void GotoNextState(QCanvas nextCanvas, TransitionType type)
        {
            if (type == TransitionType.ClearAndPop)
            {
                CanvasStack.ClearAndPop(nextCanvas);
            }
            else if (type == TransitionType.Overlay)
            {
                CanvasStack.Push(nextCanvas);
            }
            else if (type == TransitionType.Recurrence)
            {
                CanvasStack.PopAndPush(nextCanvas);
            }
        }

    }
}
