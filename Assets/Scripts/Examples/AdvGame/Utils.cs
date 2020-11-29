using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Extensions;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Utils
    {
        public static void SetImageAsBackground(EGGameObject image)
        {
            image.gameObject.GetImageComponent().SetNativeSize();
            image.SetMiddleCenterAnchor().SetPosition(0, 0);
            var asfitter = image.gameObject.AddComponent<AspectRatioFitter>();
            asfitter.aspectRatio = image.RectSize.x / image.RectSize.y;
            asfitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
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

        //--------------------------------------------------------------------------------
        // 引数に渡したオブジェクトをディープコピーしたオブジェクトを生成して返す
        // ジェネリックメソッド版
        //--------------------------------------------------------------------------------
        public static T DeepCopy<T>(T target)
        {
            T result;
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();

            try
            {
                b.Serialize(mem, target);
                mem.Position = 0;
                result = (T) b.Deserialize(mem);
            }
            finally
            {
                mem.Close();
            }

            return result;
        }
    }


    public static partial class TupleEnumerable
    {
        public static IEnumerable<(T item, int index)> Indexed<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerable<(T item, int index)> impl()
            {
                var i = 0;
                foreach (var item in source)
                {
                    yield return (item, i);
                    ++i;
                }
            }

            return impl();
        }
    }
}