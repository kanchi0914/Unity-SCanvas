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