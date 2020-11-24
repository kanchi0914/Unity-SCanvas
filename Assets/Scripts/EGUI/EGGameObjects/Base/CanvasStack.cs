using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.GameObjects;
using UnityEngine;

namespace EGUI.Base
{
    public static class CanvasStack
    {
        public static Stack<(string name, EGCanvas egCanvas)> Stack { get; set; } = new Stack<(string, EGCanvas)>();
        
        public static void Push (EGCanvas canvas)
        {
            Stack.Push((canvas.gameObject.name, canvas));
            canvas.gameObject.GetComponent<Canvas> ().sortingOrder = Stack.Count * 1;
        }

        public static void Pop ()
        {
            Stack.Pop().egCanvas.gameObject.DestroySelf();
            // GameObject.DestroyImmediate(Stack.Pop().egCanvas.GameObject);
        }

        public static void ClearAll ()
        {
            foreach (var s in Stack)
            {
                s.egCanvas.gameObject.DestroySelf();
            }
            Stack.Clear ();
        }

    }
}