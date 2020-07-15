using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SCanvases;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public class SCanvas : SGameObject
    {

        public List<SubCanvas> SubCanvases { get; set; } = new List<SubCanvas> ();

        public SCanvas ()
        {
            InitGameObject();
            if (CanvasStack.Stack.Count == 0)
            {
                CanvasStack.Push (this);
            }
        }

        public override void InitGameObject(params object[] args)
        {
            gameObject = UIFactory.CreateCanvas ();
            gameObject.name = $"{this.GetType().Name}";
        }

        // public void AddSubCanvas (
        //     float posRatioX = 0, float posRatioY = 0, float widthRatio = 1f, float heightRatio = 1f)
        // {
        //     var subCanvas = new SubCanvas("");
        //     subCanvas.gameObject = UIFactory.CreateBaseRect ();
        //     subCanvas.rectTransform = this.GameObject.GetComponent<RectTransform> ();
        //     SubCanvases.Add (subCanvas);
        //     subCanvas.GameObject.transform.SetParent (this.GameObject.transform, false);
        //     subCanvas.SetSize (new Vector2 (widthRatio * RectSizeX, heightRatio * RectSizeY));
        //     subCanvas.GameObject.transform.AddLocalPosX (posRatioX * RectSizeX);
        //     subCanvas.GameObject.transform.AddLocalPosY (-posRatioY * RectSizeY);
        // }

        public void GotoNextState (SCanvas nextCanvas, TransitionType type)
        {
            if (type == TransitionType.ClearAndPop)
            {
                CanvasStack.ClearAndPop (nextCanvas);
            }
            else if (type == TransitionType.Overlay)
            {
                CanvasStack.Push (nextCanvas);
            }
            else if (type == TransitionType.Recurrence)
            {
                CanvasStack.PopAndPush (nextCanvas);
            }
        }

        public Action GetFunction(Action function)
        {
            return new Action(() => function());
        }

        public Action<string> GetFunction(Action<string> function)
        {
            return new Action<string>(function);
        }

    }
}