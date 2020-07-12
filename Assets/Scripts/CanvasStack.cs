using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class CanvasStack
    {
        //public static List<QCanvas> CanvasQueue { get; set; } = new List<QCanvas>();
        public static Stack<QCanvas> Stack { get; set; } = new Stack<QCanvas>();

        public static void Push(QCanvas qCanvas)
        {
            Stack.Push(qCanvas);
        }

        public static void ClearAll()
        {
            foreach(var s in Stack)
            {
                GameObject.Destroy(s.GameObject);
            }
            Stack.Clear();
        }

        public static void ClearAndPop(QCanvas qCanvas)
        {
            foreach (var s in Stack)
            {
                GameObject.Destroy(s.GameObject);
            }
            Stack.Clear();
            //Stack.Push(qCanvas);
        }

        public static void PopAndPush(QCanvas qCanvas)
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
