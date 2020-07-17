using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using SGUI.SGameObjects;
using UnityEngine;

namespace SGUI.Base
{
    public static class CanvasStack
    {
        public static Stack<SCanvas> Stack { get; set; } = new Stack<SCanvas> ();

        // public static void GotoNextState (SCanvas nextCanvas, TransitionType type)
        // {
        //     if (type == TransitionType.Clear)
        //     {
        //         ClearAndPop (nextCanvas);
        //     }
        //     else if (type == TransitionType.Overlay)
        //     {
        //         Push (nextCanvas);
        //     }
        //     else if (type == TransitionType.Recurrent)
        //     {
        //         PopAndPush (nextCanvas);
        //     }
        //     nextCanvas.GameObject.GetComponent<Canvas> ().sortingOrder = (Stack.Count - 1) * 10;
        // }

        public static void Push (SCanvas canvas)
        {
            Stack.Push (canvas);
            canvas.GameObject.GetComponent<Canvas> ().sortingOrder = Stack.Count * 1;
        }

        public static void Pop ()
        {
            GameObject.DestroyImmediate (Stack.Pop ().GameObject);
        }

        public static void ClearAll ()
        {
            foreach (var s in Stack)
            {
                GameObject.Destroy (s.GameObject);
            }
            Stack.Clear ();
        }

        public static void ClearAndPop (SCanvas qCanvas)
        {
            foreach (var s in Stack)
            {
                GameObject.Destroy (s.GameObject);
            }
            Stack.Clear ();
            //Stack.Push(qCanvas);
        }

        public static void PopAndPush (SCanvas qCanvas)
        {
            var last = Stack.Pop ();
            GameObject.Destroy (last.GameObject);
            Stack.Push (qCanvas);
        }

    }
}