using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public static class CanvasStack
    {
        //public static List<QCanvas> CanvasQueue { get; set; } = new List<QCanvas>();
        public static Stack<SCanvas> Stack { get; set; } = new Stack<SCanvas>();

        public static void GotoNextState(SCanvas nextCanvas, TransitionType type)
        {
            if (type == TransitionType.ClearAndPop)
            {
                ClearAndPop(nextCanvas);
            }
            else if (type == TransitionType.Overlay)
            {
                Push(nextCanvas);
            }
            else if (type == TransitionType.Recurrence)
            {
                PopAndPush(nextCanvas);
            }
            nextCanvas.GameObject.GetComponent<Canvas>().sortingOrder
                = (Stack.Count - 1) * 10;
        }

        public static void Push(SCanvas qCanvas)
        {
            Stack.Push(qCanvas);
        }

        public static void Pop()
        {
            GameObject.DestroyImmediate(Stack.Pop().GameObject);
        }

        public static void ClearAll()
        {
            foreach(var s in Stack)
            {
                GameObject.Destroy(s.GameObject);
            }
            Stack.Clear();
        }

        public static void ClearAndPop(SCanvas qCanvas)
        {
            foreach (var s in Stack)
            {
                GameObject.Destroy(s.GameObject);
            }
            Stack.Clear();
            //Stack.Push(qCanvas);
        }

        public static void PopAndPush(SCanvas qCanvas)
        {
            var last = Stack.Pop();
            GameObject.Destroy(last.GameObject);
            Stack.Push(qCanvas);
        }

        //public static void Recurse()
        //{

        //}
    }
}
