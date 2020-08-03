using System.Collections.Generic;
using EGUI.GameObjects;
using UnityEngine;

namespace EGUI.Base
{
    public static class CanvasStack
    {
        public static Stack<EGCanvas> Stack { get; set; } = new Stack<EGCanvas> ();
        
        public static void Push (EGCanvas canvas)
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

        public static void ClearAndPop (EGCanvas qCanvas)
        {
            foreach (var s in Stack)
            {
                GameObject.Destroy (s.GameObject);
            }
            Stack.Clear ();
        }

        public static void PopAndPush (EGCanvas qCanvas)
        {
            var last = Stack.Pop ();
            GameObject.Destroy (last.GameObject);
            Stack.Push (qCanvas);
        }

    }
}