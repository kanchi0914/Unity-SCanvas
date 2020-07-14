using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.SCanvases
{
    public class MultiCanvas : SCanvas
    {

        //private GameObject myButton = Resources.Load("MyButton") as GameObject;

        // set constructor
        public MultiCanvas()
        {
            //GameObject myButton = Resources.Load("MyButton") as GameObject;
            //UnityEngine.GameObject.Instantiate(myButton);
            var list1 = new List<SButton>()
            {
                new SButton(gameObject, "aaa", func("aaaa")),
                new SButton(gameObject, "bbb", func("bbbb")),
                new SButton(gameObject, "ccc", func("cccc"))
            };

            //var list4 = new List<SButton>()
            //{
            //    new SButton(gameObject, "aaa", func("aaaa")),
            //    new SButton(gameObject, "bbb", func("bbbb")),
            //    new SButton(gameObject, "ccc", func("cccc")),
            //    new SButton(gameObject, "ddd", func("cccc")),
            //    new SButton(gameObject, "eee", func("cccc")),
            //};

            //var list3 = new List<Transform>()
            //{
            //    UIFactory.CreateText(this.GameObject, "11111").transform,
            //    UIFactory.CreateText(this.GameObject, "22222").transform,
            //    UIFactory.CreateText(this.GameObject, "33333").transform
            //};
            //var pre = new SPrefab(gameObject, "MyButton");
            //var list2 = new List<SPrefab>()
            //{
            //    pre
            //};
            


            var sub1 = new SubCanvas(this, 0.1f, 0.1f, 0.8f, 0.8f);
           
            var sub2 = new SubCanvas(sub1, 0, 0, 1f, 0.2f);
            sub2.SetText("sugoi");
            var sub3 = new SubCanvas(sub1, 0, 0.2f, 1f, 0.6f);
            var sub3_1 = new SubCanvas(sub3, 0, 0, 0.5f, 1);
            var sub3_2 = new SubCanvas(sub3, 0.5f, 0, 0.5f, 1);
            sub3_1.SetVerticalListItems(list1);
            //sub3_2.SetGridButtons(list4, 2, 4);
            var sub4 = new SubCanvas(sub1, 0, 0.8f, 1f, 0.2f);

        }

        public Action func(string str)
        {
            return new Action(() => { Debug.Log(str); });
        }

    }

}
